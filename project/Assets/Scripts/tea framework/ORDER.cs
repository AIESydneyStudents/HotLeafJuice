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
    const string CONFIG_FILEPATH = @"C:\TeaTurmoil\config\order.json";
    #region

    [SerializeField] UnityEngine.UI.Slider slider;
    [SerializeField] float timerTime;
    private string orderName;
    private int orderScore;
    private bool completed;
    private Timer timer;
    [SerializeField] List<npcOrders> Orders;
    #endregion


    // bool jsonOptions = true;
    public bool importJSON;
    public bool exportJSON;

    //public bool disableEditor;


    /// <summary>
    /// Order struct: stores all nessessary info that is gathered in editor
    /// </summary>
    [System.Serializable]
    struct npcOrders
    {
        public string title;
        public List<ingredient> ingredients;
        public bool isCompleted;
        public float orderTimer;

    }
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
            Orders order = new Orders(PleaseLetItEnd.title, PleaseLetItEnd.ingredients, PleaseLetItEnd.orderTimer);
            orderList.Add(order);
        }

        foreach (var ord in orderList)
        {
            Debug.Log(ord + " | " + ord.score);
            
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

    public void ExportJSON(string filename)
    {
        string configFilepath = CONFIG_FILEPATH;
        string jsonexport = "a";
        foreach(var thing in this.orderList)
        {
            jsonexport += JsonUtility.ToJson(thing, true);           
        }

        if (File.Exists(configFilepath))
        {
            
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
                    DirectoryInfo dick = Directory.CreateDirectory(@"C:\TeaTurmoil\config\");

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


}



