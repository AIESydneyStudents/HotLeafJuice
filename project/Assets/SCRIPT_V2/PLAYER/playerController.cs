using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using System.Text;
//You're a bitch


public class playerController : MonoBehaviour
{

    #region Serilized Fields for inspector

    [Header("Player Settings")]
    [SerializeField] private player_movement movement;
    [SerializeField] private float movement_speed;
    [SerializeField] private Transform headAnchor;

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
        timer = new Timer(orderTimer);

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
      //  cup.LoadOrder(orderList);
       // CookingStation.LoadOrder(orderList);
        random = new System.Random();

    }

    
    private void Update()
    {
        

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


                    if(cup.CheckOrder() == true)
                    {
                        
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
                        //placed.SetActive(false);
                        //bench.isUsed = false;
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

        //timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);

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

    private void UpdateSpawner()
    {
        timeLeft += 1 * Time.deltaTime;
        if (timeLeft >= SpawnTimer)
        {
            Spawner.Spawn();
            timeLeft = 0;
        }
        if (spawnOnce == true)
        {
            Spawner.Spawn();
            spawnOnce = false;
            return;
        }
        
    }

}