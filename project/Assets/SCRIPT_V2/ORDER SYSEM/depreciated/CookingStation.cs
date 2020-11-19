using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingStation : MonoBehaviour
{

    public List<Orders> orderCheck;
    public List<ingredient> playerIngredients;
    public TMPro.TextMeshProUGUI textA;

    private List<ingredient> checkList = new List<ingredient>();

  

    public void LoadOrder(List<Orders> _orders)
    {
        orderCheck = _orders;

    }


    public bool CheckOrder()
    {
        foreach(var b in orderCheck)
        {
            if (checkList.Count == b.ingredients.Count)
            {
                return true;
            }
        }
        return false;
    }

    public bool AcceptIngredient(ingredient ingredient)
    {
        foreach(var check in orderCheck)
        {
            foreach(var ingred in check.ingredients)
            {
                if (ingredient.name == ingred.name)
                {
                    checkList.Add(ingredient);
                    return true;
                }
            }
        }

        return false;
    }


}
