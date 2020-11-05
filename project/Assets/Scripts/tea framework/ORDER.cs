using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

/// <summary>
/// Order Editor class
/// </summary>
public class ORDER : MonoBehaviour
{
    
    [Header("Config Order Infomation")]
    public string CONFIG_FILEPATH = @"C:\TeaTurmoil\config\order.json";
    #region Class Variables
    [SerializeField] private Spawner spawner;
    [SerializeField] UnityEngine.UI.Slider timerElement;

   
    [Header("Timer Settings")]
    [SerializeField] float timerTime;
    
    
    private List<Orders> level1 = new List<Orders>();
    private List<Orders> level2 = new List<Orders>();
    private List<Orders> level3 = new List<Orders>();
    private string orderName;
    private int orderScore;
    private bool completed;
    private float time;
    private Timer timer;
 

    [Header("Order Editor")]
    [SerializeField] List<npcOrders> Orders;
    private System.Random rand;
    #endregion


    // bool jsonOptions = true;
    //public bool importJSON;
    public bool exportJSON;

    private bool orderStart = false;
    private string jsonexport = " ";
    //public bool disableEditor;
    

    
    [System.Serializable]
    struct npcOrders
    {

        public string title;
        [Range(1, 3)] public int diffLevel;
        public List<ingredient> ingredients;
        public bool isCompleted;
        public float orderTimer;

    }


    [System.Serializable]
    public struct npcEditor
    {
        public string npc_name;
        [Range(1, 3)] public int difficultyLevel;
        public Mesh headMesh;
    }
    
    
    [Header("NPC Editor")]
    public bool npcSpawnEnabled;

    [Tooltip("Spawn Timer for NPC (Seconds)")]
    public float spawnTimer;

    public List<npcEditor> NPC_Editor;
    
    private List<NPC> MainNPCsList = new List<NPC>();
    private bool runOnce;
    [HideInInspector]
    public List<Orders> orderList = new List<Orders>();
    private int sizeCount = 0;
    private int numOFNPC;
    /// <summary>
    /// Returns the completion state of the given order
    /// </summary>
    /// <returns></returns>
    public bool IsCompleted()
    {
        return completed;
    }

    /// <summary>
    /// Run at the start of runtime
    /// </summary>
    private void Start()
    {
        runOnce = true;
        foreach (var PleaseLetItEnd in Orders)
        {
            Orders order = new Orders(
                PleaseLetItEnd.title,
                PleaseLetItEnd.ingredients,
                PleaseLetItEnd.orderTimer,
                PleaseLetItEnd.diffLevel);

            orderList.Add(order);
        }

        rand = new System.Random();

        foreach (var npc in NPC_Editor)
        {

            NPC newnpc = new NPC(
                npc.npc_name,
                spawner.transform
                );

            newnpc.SetType(
                npc.headMesh, 
                npc.difficultyLevel);

            newnpc.orderForNPC = getOrder(npc.difficultyLevel);

            MainNPCsList.Add(newnpc);



        }

        foreach (var npc in MainNPCsList)
        {
            Debug.Log(npc.orderForNPC.diffLevel + " " + npc.orderForNPC.name + "FOR NPC" + npc.npcName);
        }


        spawner.Set(MainNPCsList); // assign the spawner the new NPCS

        if (exportJSON == true)
        {
            ExportJSON("");
        }


        numOFNPC = MainNPCsList.Count;

        timer = new Timer(timerTime); // create new timer object
    }

    /// <summary>
    /// Display the time from the timer class and overlay that into a slider value
    /// </summary>
    /// <param name="time"></param>
    private void DisplayUpdate(float time)
    {
        timerElement.value = time;
        if (timerElement.value <= 30)
        {

        }
        if (timerElement.value <= 10)
        {
            timerElement.image.color = Color.red;
        }
    }

    public void StartOrder_BUTTON()
    {
        orderStart = true;
    }


    /// <summary>
    /// RUNS THE ORDER LOOP
    /// 
    /// </summary>
    private void Update()
    {
        if (runOnce == true)
        {
            spawner.Spawn();
            runOnce = false;
            return;
        }
        time += 1 * Time.deltaTime;

       // Debug.Log(time);

        if (time >= spawnTimer && sizeCount <= numOFNPC)
        {
            if (npcSpawnEnabled == true)
            {
                spawner.Spawn();
                time = 0;
                sizeCount++;
            }
        }


    }


    public string SendJSON()
    {
        return jsonexport;
    }

    public void ExportJSON(string filename)
    {
        string configFilepath = CONFIG_FILEPATH;

        foreach (var thing in this.orderList)
        {

            jsonexport += JsonUtility.ToJson(thing, true);
        }

        if (File.Exists(configFilepath))
        {
            if (jsonexport.Length < 0)
            {
                using (FileStream fs = File.Create(configFilepath))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(jsonexport);
                    fs.Write(info, 0, jsonexport.Length);
                }
            }
            else
            {
                return;
            }
        }
        else
        {
            try
            {

                if (Directory.Exists(@"C:\TeaTurmoil\config\"))
                {
                    return;
                }
                else
                {
                    DirectoryInfo dir = Directory.CreateDirectory(@"C:\TeaTurmoil\config\");

                }

                using (FileStream fs = File.Create(configFilepath))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(jsonexport);
                    fs.Write(info, 0, jsonexport.Length);
                }
            }
            catch (Exception x)
            {
                Debug.Log(x.ToString());
            }
        }

        Debug.Log(jsonexport.ToString());
    }
    private Orders getOrder(int level)
    {


        foreach (var f in orderList)
        {
            if (f.diffLevel == 1)
            {
                level1.Add(f);
            }
            if (f.diffLevel == 2)
            {
                level2.Add(f);
            }
            if (f.diffLevel == 3)
            {
                level3.Add(f);
            }
        }


        if (level == 1)
        {
            Orders order = level1[rand.Next(level1.Count)];
            return order;
        }
        if (level == 2)
        {
            Orders order = level2[rand.Next(level2.Count)];
            return order;
        }
        if (level == 3)
        {
            Orders order = level3[rand.Next(level3.Count)];
            return order;
        }

        else
        {
            return null;
        }


    }
}



    