using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingStation : MonoBehaviour
{

    public List<Orders> orderCheck;
    public List<ingredient> playerIngredients;

    public TMPro.TextMeshProUGUI textA;

    private void Start()
    {
        
    }

    public void LoadOrder(List<Orders> _orders)
    {
        orderCheck = _orders;

    }


    public bool AcceptIngredient(ingredient ingredient)
    {
        foreach(var check in orderCheck)
        {
            foreach(var ingred in check.ingredients)
            {
                if (ingredient.name == ingred.name)
                {

                    return true;
                }
            }
        }

        return false;
    }


}
