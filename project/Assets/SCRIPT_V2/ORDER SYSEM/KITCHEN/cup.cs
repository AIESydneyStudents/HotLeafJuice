using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListManager<T>
{
    private List<T> classList = new List<T>();


    public ListManager()
    {

    }

    ~ListManager()
    {


    }

    public void SetList(List<T> list)
    {
        classList = list;
    }

    public void RemoveFromList(int index)
    {
        classList.RemoveAt(index);
    }

    public List<T> GetList()
    {
        return classList;
    }


}



public class cup : MonoBehaviour
{
    private List<ingredient> inCup = new List<ingredient>();

    
    public ListManager<Orders> orderCheck = new ListManager<Orders>();

    List<Orders> ingredientSprites = new List<Orders>();

    [HideInInspector] List<ingredient> playerIngredients;
    

    public void LoadOrder(List<Orders> orders)
    {
        orderCheck.SetList(orders);

        ingredientSprites = orders;
      
    }

    public Orders returnOrder(int count)
   {       
        var temp = ingredientSprites[count];
      
        return temp;
    }

    public float ReturnScore()
    {
        float result = 0f;
        foreach(var ingred in orderCheck.GetList())
        {
            result += ingred.score;
        }
        return result;
    }
    

    public bool CheckOrder()
    {
        foreach(var b in orderCheck.GetList())
        {
            if(inCup.Count == b.ingredients.Count - 1)
            {
                gameObject.SetActive(false);
                return true;
            }
        }
        return false;
    }

    public bool addIngredient(ingredient ingredient)
    {
        foreach (var check in orderCheck.GetList())
        {
            foreach (var ingred in check.ingredients)
            {
                if (ingredient.name == ingred.name)
                {
                    GameObject @object = ingredient.ingredient_sprite;

                    @object.SetActive(false);

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
