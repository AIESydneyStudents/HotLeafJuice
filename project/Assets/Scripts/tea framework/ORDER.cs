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

    private List<Orders> orderList;



    public bool IsCompleted()
    {
        return completed;
    }


    private void Start()
    {
        List<Orders> temp = null;
        foreach(var PleaseLetItEnd in Orders)
        {
            Orders order = new Orders(PleaseLetItEnd.title, PleaseLetItEnd.ingredients);
            temp.Add(order);
        }

        foreach(var gay in orderList)
        {
            Debug.Log("ORDER NAME: " + gay.name + " | ORDER SCORE: " + gay.score);
        }
        
    }

}



