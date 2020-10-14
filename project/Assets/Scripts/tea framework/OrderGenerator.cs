using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ORDER : MonoBehaviour
{
    public string name;
    public List<ingredient> ingredients;
    public int score;
    public bool completed;

    public ORDER(string _name, List<ingredient> _ingredients, int _score)
    {
        name = _name;
        ingredients = _ingredients;
        score = _score;
        completed = false;
    }

    public bool IsCompleted()
    {
        return completed;
    }

}


public class OrderGenerator : MonoBehaviour
{

   
    [System.Serializable] public struct Orders
    {
        public string orderName;
        public List<ingredient> ingredients;
    }

    private List<ORDER> ordersList;
    [SerializeField] List<Orders> orderMenu;



    private void Start()
    {
       
    }

   

    public string returnJson()
    {
        string json = JsonUtility.ToJson(orderMenu);
        return json;
    }

   
}
