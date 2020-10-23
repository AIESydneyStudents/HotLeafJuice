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
    public void CheckOrder()
    {

    }

    private string ListToText(List<string> text)
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
        List<string> orderMessage = new List<string>();
        
        ordersList = orders;
        foreach (var order in ordersList)
        {
            foreach (var ingred in order.ingredients)
            {
                orderMessage.Add(" - " + ingred.name);
            }
        }


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
                    stateText.text = "Correct";
                    Ordertext.text += " \n" + " > " + ingred.name + "\n";
                    return;
                }
                stateText.text = "Incorrect";                              
            }
        }


        Debug.Log(totalScore);

    }



    
}
