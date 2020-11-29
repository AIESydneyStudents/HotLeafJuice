using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using System.Text;
//You're a bitch


public class playerController : MonoBehaviour
{
    int Acount = 0;
    #region Serilized Fields for inspector

    [Header("Player Settings")]
    [SerializeField] private player_movement movement;
    [SerializeField] private float movement_speed;
    [SerializeField] private Transform headAnchor;
    [SerializeField] private UnityEngine.UI.Image sliderScore;

    [Header("Timer")]
    private Timer timer;

    [SerializeField] private TMPro.TextMeshProUGUI timerText;
    [SerializeField] private float timerLength;

    [Header("Camera")]
    public bool cameraLock;
    [SerializeField] private camerafollow cameraController;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float moveRange;

    [Header("Interaction")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float pickupRange;
    [SerializeField] private List<string> pickupTags = new List<string>();
    [SerializeField] private float inventorySizeLimit;
    [SerializeField] private cupSpawner cupSpawner;
    [SerializeField] private Transform CupTransform;
    private float count = 0;

    [Header("Tea Settings")]
    //[SerializeField] private TeaController teaController;
    [HideInInspector] public List<ingredient> ingredients = new List<ingredient>();
   // [SerializeField] private CookingStation CookingStation;

    [Header("NPC Settings")]
    [SerializeField] private float SpawnTimer;
    [SerializeField] private Spawner Spawner;
    [System.Serializable]
    struct npcEditor
    {
        public string name;
        [Range(1, 3)] public int difficultyLevel;
        public Transform spawnLocation;
        

        public Mesh headMesh;
      
    }
    [SerializeField] List<npcEditor> npceditor = new List<npcEditor>();

    [Header("Orders")]
    [SerializeField] private float orderTimer;
    [SerializeField] private string filePath = @"C:\TeaTurmoil\config\order.json";
    [System.Serializable]
    struct npcOrders
    {
        public string title;
        [Range(1, 3)] public int difficulty;

        public bool warmWater; // 30 - 69
        public bool hotWater; // 70 - 99

        public List<ingredient> ingredients;
        public float timeLimit;
    }
    [SerializeField] List<npcOrders> Orders;

    #endregion


    #region Script Fields

    [HideInInspector] public List<Orders> orderList = new List<Orders>();
    [HideInInspector] public List<NPC> mainNPCList = new List<NPC>();

    private ScoreTracking tracker = new ScoreTracking();
    private bool HoldingKettle = false;
    private float timeLeft = 0;
    private bool spawnOnce = true;
    private System.Random random;

    private List<kettle> kettleInventory = new List<kettle>();

    private List<ingredient> inventory = new List<ingredient>();
    
    // Check status of all boiling kettles
    private List<kettle> kettlePlaced = new List<kettle>();

    GameObject headOBJECT = null;
    bool headObjectActive = false;
    #endregion



    private void Start()
    {

        tracker.SetLimit(Orders.Count);
        //tracker.SetLimit(5);

        timer = new Timer(0);

        movement.playerAgent.speed = movement_speed;
        cameraController.InitCamera();

        foreach(var ord in Orders)
        {
            Orders newOrder = new Orders(ord.title, ord.ingredients, ord.timeLimit, ord.difficulty);
            orderList.Add(newOrder);
        }

        foreach (var npc in npceditor)
        {
            NPC newnpc = new NPC(npc.name, npc.spawnLocation, false);
            newnpc.SetType(npc.headMesh, npc.difficultyLevel);
            
            foreach(var ord in orderList)
            {
                newnpc.orderForNPC = ord;
            }

            mainNPCList.Add(newnpc);
        }

        Spawner.Set(mainNPCList);
        timeLeft = 0;
     
        random = new System.Random();

    }

    
    private void Update()
    {
        //if(timer.timeRemaining <= 0)
        //{
        //    timeLeft = 0;
        //    List<GameObject> npc = Spawner.GetNPCs();
        //    foreach (var n in npc)
        //    {
        //        SpawnTimer = 0;
        //        n.GetComponent<scriptableNPC>().isDone = true;
        //    }
        //}

        cameraController.UpdateCamera(playerCamera, playerTransform, moveRange, cameraSpeed); // Update camera
        movement.ControllerMovement(Time.deltaTime); // Move player

        

        UpdateTimer();
        UpdateSpawner();
        InteractWithGame();

        if (headObjectActive)
        {
            headOBJECT.transform.position = headAnchor.position;
            headOBJECT.transform.rotation = headAnchor.rotation;
        }

        
        if (kettlePlaced.Count >= 1)
        {
            foreach (var kettle in kettlePlaced)
            {
                if (kettle.isBoilingWater == true)
                {
                    Debug.Log(kettle.Water.temp);
                    
                    kettle.HeatWater();

                    // Hot water update
                    if(kettle.isBoiling(10) == true)
                    {
                        kettle.water.hotWater = true;
                        kettle.water.warmWater = false;

                        kettle.isBoilingWater = false;
                        return;
                    }
                    

                }

            }
        }

    }

    private void InteractWithGame()
    {
        tracker.timer_timeRemaining = timer.timeRemaining;
        if (Input.GetButtonDown("Fire1"))
        {
            Collider[] near = Physics.OverlapSphere(movement.playerAgent.transform.position, pickupRange);
            foreach (var collider in near)
            {
                foreach (var tag in pickupTags)
                {
                    if (collider.gameObject.CompareTag(tag))
                    {
                        if (tag == "pile")
                        {
                            PileUse(collider);
                        }
                        if (tag == "tea leaves")
                        {
                            TeaBagPickup(collider);

                        }
                        if (tag == "kettle")
                        {
                            PickupKettle(collider);
                        }
                        
                    }
                }
            }


        }

        if (Input.GetButtonDown("Fire2"))
       {

            Collider[] cupCheck = Physics.OverlapSphere(movement.playerAgent.transform.position, pickupRange);
            foreach(var col in cupCheck)
            {
                if (col.gameObject.CompareTag("cup"))
                {
                
                    cup cup = col.gameObject.GetComponent<cup>();
                    cup.LoadOrder(orderList);
                 //   cup.LoadSprites();

                    // ------------------------------- IF ORDER DONE CORRECTLY -----------------------------------------
                    if(cup.CheckOrder() == true)
                    {
                        List<GameObject> npc = Spawner.GetNPCs();

                        foreach(var n in npc)
                        {
                            n.GetComponent<scriptableNPC>().isDone = true;
                        }

                        Spawner.SetNPCs(npc);
                        foreach (var check in cup.orderCheck.GetList())
                        {
                            foreach (var ingred in check.ingredients)
                            {
                                ingred.ingredient_sprite.gameObject.SetActive(false);
                            }
                        }

                        // Math go brrrrr
                       tracker.AddToScore(1);

                        if (tracker.GetTotalScore() >= tracker.GetLimit())
                        {
                            sliderScore.gameObject.SetActive(true);
                            sliderScore.fillAmount = (Mathf.Clamp(tracker.timer_timeRemaining, 1, 10) - 1) / (10 - 1);
                        }


                        

                        timer.StopTimer();
                        cup.gameObject.SetActive(false);
                    }

                    if(HoldingKettle == true)
                    {
                        kettle kettle = kettleInventory.First();
                        kettle.hotTempSprite.SetActive(false);
                        if (cup.addIngredient(kettle.water) && kettle.Water.amount > 0)
                        {
                            kettle.Water.amount = 0;
                            kettle.warmTempSprite.SetActive(true);
                            
                            kettle.water.hotWater = false;
                        }
                    }

                    try
                    {
                        if (cup.addIngredient(inventory.First()) == true)
                        {
                            count = 0;
                            RemoveHeadObject();
                            inventory.Remove(inventory.First());
                        }
                    }
                    catch
                    {

                    }

                }
                if (col.gameObject.CompareTag("bin")){ UseBin(); }
            }


            
            if (HoldingKettle == false) { 
                bench bench = getClosest();
                if (bench.isUsed == false)
                {

                    count = 0;
                    RemoveHeadObject();
                    GameObject placed = Instantiate(inventory.First().gameObject) as GameObject;

                    placed.name = inventory.First().gameObject.name;
                    placed.transform.position = bench.transform.position + new Vector3(0f, 1f, 0f);
                    inventory.Remove(inventory.First());
                    placed.SetActive(true);
                    bench.isUsed = true;

                    if(bench.isStove == true)
                    {
                      //  placed.SetActive(false);
                       //dw bench.isUsed = false;
                    }

                }
            }
            else 
            { 

                bench bench = getClosest();

                {
                    count = 0;
                    

                    GameObject placed = Instantiate(kettleInventory.First().gameObject) as GameObject;
                    
                    placed.name = kettleInventory.First().gameObject.name;
                    placed.transform.position = bench.transform.position + new Vector3(0f, 1f, 0f);


                    
                    kettleInventory.Remove(kettleInventory.First());

                    placed.SetActive(true);
                    RemoveHeadObject();
                    bench.isUsed = true;

                    if (bench.isStove == false)
                    {
                        placed.gameObject.GetComponent<kettle>().isCooling = true;
                    }

                    if (bench.isStove == true)
                    {
                        placed.gameObject.GetComponent<kettle>().isBoilingWater = true;

                    }
                    HoldingKettle = false;
                    kettlePlaced.Add(placed.GetComponent<kettle>());

                }
                
            }
        }

    }


    #region 
    private bench getClosest()
    {
        bench bench = null;
        float Bestdistance = 2.0f;
        float distance = 0;
        Collider closest = null;
        Collider[] near = Physics.OverlapSphere(movement.playerAgent.transform.position, pickupRange);
        foreach (var collider in near)
        {
            if (collider.CompareTag("bench"))
            {
                distance = Vector3.Distance(movement.playerAgent.transform.position, collider.gameObject.GetComponent<bench>().transform.position);
                if (distance < Bestdistance)
                {
                    Bestdistance = distance;                  
                    bench = collider.gameObject.GetComponent<bench>();
                }
            }
        }
        return bench;
    }

    private void PickupKettle(Collider col)
    {
        if (count >= inventorySizeLimit || kettleInventory.Count >= 1)
        {
            return;
        }

        bench bench = getClosest();
        count += 1;
        kettle pickup = col.gameObject.GetComponent<kettle>();
        if (kettlePlaced.Count >= 1)
        {
            kettlePlaced.Remove(kettlePlaced.First());
        }
        kettleInventory.Add(pickup);
        Debug.Log("Added Kettle To Player Hands");
        Debug.Log("Kettle Has Water || Water Is Cold");
        col.gameObject.SetActive(false);
        SetHeadObject(pickup.gameObject);
        bench.isUsed = false;
        HoldingKettle = true;

    }

    private bool TeaBagPickup(Collider col)
    {
        

        if (count >= inventorySizeLimit)
        {
            Debug.Log("FUNCTION - (void) TeaBagPickip - Inventory limit reached: " + count);
            return false;
        }

        bench bench = getClosest();
        count += 1;
        ingredient pickup = col.gameObject.GetComponent<ingredient>();
        inventory.Add(pickup);
        Debug.Log("Added: " + pickup.Objectname + " to player inventory");
        col.gameObject.SetActive(false);
        SetHeadObject(pickup.gameObject);
        bench.isUsed = false;

        return true;

    }

    private void PileUse(Collider col)
    {
        if (count >= inventorySizeLimit)
        {
            Debug.Log("FUNCTION - (void) PileUse - Inventory limit reached: " + count);
            return;
        }

        count += 1;
        ingredientPile pile = col.gameObject.GetComponent<ingredientPile>();
        Debug.Log("Added: " + pile.Name + " to player inventory");
        
        inventory.Add(pile.createItem());

        SetHeadObject(pile.createItem().gameObject);

        // TO DO: Add headobject script

    }


    private void UpdateTimer()
    {
        timer.Update();
        float minutes = Mathf.FloorToInt(timer.timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timer.timeRemaining % 60);

        timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);

    }

    
    private void SetHeadObject(GameObject _obj)
    {
        headOBJECT = Instantiate(_obj) as GameObject;
        headObjectActive = true;
        headOBJECT.SetActive(true);
    }

    private void RemoveHeadObject()
    {
        headObjectActive = false;

        headOBJECT.SetActive(false);
    }
    #endregion
    private void UpdateSpawner()
    {
        
        timeLeft += 1 * Time.deltaTime;
        if (timeLeft >= SpawnTimer)
        {
            GameObject cup = Instantiate(cupSpawner.CreateCup()) as GameObject;
            cup.transform.position = CupTransform.position;
            cup.SetActive(true);
            cup.GetComponent<cup>().LoadOrder(orderList);

            var check = cup.GetComponent<cup>().returnOrder(Acount);
            Acount += 1;
            foreach (var sprite in check.ingredients)
            {
                sprite.ingredient_sprite.SetActive(true);
            }


            timer.StartTimer(timerLength);
            Spawner.Spawn();
            timeLeft = 0;
        }

        if (spawnOnce == true)
        {
            Spawner.Spawn();
            spawnOnce = false;

            GameObject cup = Instantiate(cupSpawner.CreateCup()) as GameObject;
            cup.SetActive(true);
            cup.transform.position = CupTransform.position;
            cup.GetComponent<cup>().LoadOrder(orderList);

            var check = cup.GetComponent<cup>().returnOrder(Acount);
            Acount += 1;
            foreach (var sprite in check.ingredients)
            {
                sprite.ingredient_sprite.SetActive(true);
            }


            timer.StartTimer(timerLength);


            // spawnOnce = false;
            return;
        }


        
    }

    private void UseBin()
    {
        inventory.Remove(inventory.First());
        count = 0;
        RemoveHeadObject();
    }

}


class ScoreTracking
{
    struct Scoreboard
    {
        public float totalScore;
        public int limit;
        public float scoreLimit;
    }

    float MIN = 1;
    float MAX = 10;

    public float timer_timeRemaining = 0;
    public float normalized_score = 0;

    private Scoreboard ScoreBoard;
    public ScoreTracking() {

        ScoreBoard = new Scoreboard();

    }




    public void NormalizeScore()
    {
        float finalAmount = (Mathf.Clamp(timer_timeRemaining, MIN, MAX) - MIN) / (MAX - MIN);
    }

    public void AddToScore()
    {
        ScoreBoard.totalScore += ScoreBoard.totalScore;
    }

    public float GetLimit()
    {
        return ScoreBoard.limit;
    }

    public void AddToScore(float score)
    {
        ScoreBoard.totalScore += score;
    }

    public float GetTotalScore()
    {
        return ScoreBoard.totalScore;
    }

    public void SetLimit(int limit)
    {
        ScoreBoard.limit = limit;
    }

    public void SetScoreMax(float maxScore)
    {
        ScoreBoard.scoreLimit = maxScore;
    }


}