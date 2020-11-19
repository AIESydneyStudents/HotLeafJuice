using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cup : MonoBehaviour
{
    [HideInInspector] private List<ingredient> inCup = new List<ingredient>();

    [HideInInspector] public List<Orders> orderCheck;
    
    [HideInInspector] List<ingredient> playerIngredients;


    public void LoadOrder(List<Orders> orders)
    {
        orderCheck = orders;
    }

    public bool CheckOrder()
    {
        foreach(var b in orderCheck)
        {
            if(inCup.Count == b.ingredients.Count)
            {
                return true;
            }
        }
        return false;
    }

    public bool addIngredient(ingredient ingredient)
    {
        foreach (var check in orderCheck)
        {
            foreach (var ingred in check.ingredients)
            {
                if (ingredient.name == ingred.name)
                {
                    inCup.Add(ingredient);
                    return true;
                }
            }
        }

        return false;
    }

    public List<ingredient> returnCupInventory()
    {
        return inCup;
    }

    public void AddToCup(ingredient ingredient)
    {
        inCup.Add(ingredient);
    }

   

}
