using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ORDER : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Slider slider;
    [SerializeField] float timerTime;
    private string orderName;
    private int orderScore;
    private bool completed;
    private Timer timer;
    [SerializeField] List<npcOrders> Orders;
    
    

    [System.Serializable]
    struct npcOrders
    {
        public string title;
        public List<ingredient> ingredients;
        public bool isCompleted;
        public float orderTimer;

    }

    public List<Orders> orderList = new List<Orders>();



    public bool IsCompleted()
    {
        return completed;
    }


    private void Start()
    {
        foreach (var PleaseLetItEnd in Orders)
        {
            Orders order = new Orders(PleaseLetItEnd.title, PleaseLetItEnd.ingredients, PleaseLetItEnd.orderTimer);
            orderList.Add(order);
        }

        foreach (var ord in orderList)
        {
            Debug.Log(ord.name + " | " + ord.score);
            
        }

        timer = new Timer(timerTime);
    }


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

    

}



