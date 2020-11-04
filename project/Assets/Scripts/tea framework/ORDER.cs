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
    public string CONFIG_FILEPATH = @"C:\TeaTurmoil\config\order.json";
    #region Class Variables

    [SerializeField] UnityEngine.UI.Slider slider;
    [SerializeField] float timerTime;
    [SerializeField] private Spawner spawner;
    private List<Orders> level1 = new List<Orders>();
    private List<Orders> level2 = new List<Orders>();
    private List<Orders> level3 = new List<Orders>();
    private string orderName;
    private int orderScore;
    private bool completed;

    private Timer timer;
    [SerializeField] List<npcOrders> Orders;
    private System.Random rand;
    #endregion


    // bool jsonOptions = true;
    //public bool importJSON;
    public bool exportJSON;

    private bool orderStart = false;
    private string jsonexport = " ";
    //public bool disableEditor;


    /// <summary>
    /// Order struct: stores all nessessary info that is gathered in editor
    /// </summary>
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

    public List<npcEditor> NPC_Editor;
    private List<NPC> MainNPCsList = new List<NPC>();



    [HideInInspector]
    public List<Orders> orderList = new List<Orders>();


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
            newnpc.SetType(npc.headMesh, npc.difficultyLevel);
            newnpc.orderForNPC = getOrder(npc.difficultyLevel);
            MainNPCsList.Add(newnpc);

        }

        foreach (var npc in MainNPCsList)
        {
            Debug.Log(npc.orderForNPC.diffLevel);
        }




        if (exportJSON == true)
        {
            ExportJSON("");
        }

        timer = new Timer(timerTime); // create new timer object
    }

    /// <summary>
    /// Display the time from the timer class and overlay that into a slider value
    /// </summary>
    /// <param name="time"></param>
    private void DisplayUpdate(float time)
    {
        slider.value = time;
        if (slider.value <= 30)
        {

        }
        if (slider.value <= 10)
        {
            slider.image.color = Color.red;
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
        //if (orderStart == true)
        //{
        //    timer.StartTimer(timerTime);

        //    orderStart = false;
        //}
        //timer.Update();
        // Debug.Log(timer.timeRemaining);
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



    