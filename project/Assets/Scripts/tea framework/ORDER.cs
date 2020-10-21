using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ORDER : MonoBehaviour
{


   

    private string orderName;
    private int orderScore;
    private bool completed;
    [SerializeField] List<npcOrders> Orders;

    [System.Serializable]
    struct npcOrders
    {
        public string title;
        public List<ingredient> ingredients;
        public bool isCompleted;

    }

    private List<Orders> orderList = new List<Orders>();



    public bool IsCompleted()
    {
        return completed;
    }


    private void Start()
    {
        foreach (var PleaseLetItEnd in Orders)
        {
            Orders order = new Orders(PleaseLetItEnd.title, PleaseLetItEnd.ingredients);
            orderList.Add(order);
        }

        foreach (var ord in orderList)
        {
            Debug.Log(ord.name + " | " + ord.score);
            
        }
    }

    private void Update()
    {
        foreach(var order in Orders)
        {
            completed = order.isCompleted;
        }
    }

}



