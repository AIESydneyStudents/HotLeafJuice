using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingStation : MonoBehaviour
{
    
    private List<ingredient> playerInventory;
    private List<Orders> ordersList;

    private List<Orders> cookingList;
    private float totalScore = 0;

    public void Init(List<ingredient> player)
    {

    }

    public void CheckOrder()
    {

    }

    public void loadOrder(List<Orders> orders)
    {
        ordersList = orders;
    }


    public void AcceptIngredient(ingredient ingredient)
    {
        foreach(var check in ordersList)
        {
            foreach (var ingred in check.ingredients)
            {
                if (ingredient == ingred)
                {
                    totalScore += check.score;
                    Debug.Log("Correct");
                }
                
            }
        }


        Debug.Log(totalScore);

    }



    
}
