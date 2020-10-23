using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CookingStation : MonoBehaviour
{
    
    private List<ingredient> playerInventory;
    private List<Orders> ordersList;

    private List<Orders> cookingList;
    private float totalScore = 0;

    public TextMeshProUGUI Ordertext;
    public TextMeshProUGUI stateText;
    

    private  string ListToText(List<string> text)
    {
        string result = " ";
        foreach(var str in text)
        {
            result += str.ToString() + "\n";

        }
        return result;
    }

    public void loadOrder(List<Orders> orders)
    {
       // Do UI stuff to display order here
       
        ordersList = orders;
        
    }



    public bool AcceptIngredient(ingredient ingredient)
    {
        foreach(var check in ordersList)
        {
            foreach (var ingred in check.ingredients)
            {
                if (ingredient == ingred)
                {
                   // Debug.Log("Correct");
                    stateText.text = "Correct";
                    Ordertext.text += " \n" + " > " + ingred.name + "\n";
                    return true;
                }
                
            }
        }
        return false;

       
        
    }



    
}
