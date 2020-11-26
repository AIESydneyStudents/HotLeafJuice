using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ingredientPile : MonoBehaviour
{

    public string Name;
    [SerializeField] private ingredient type;

    


    public ingredient createItem()
    {
        return type;
    }


}
