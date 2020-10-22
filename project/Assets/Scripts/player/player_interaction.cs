using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class player_interaction : MonoBehaviour
{
    // Class properties
    private float radius;

    [SerializeField] private int limit;

    public int inventorySize;
    List<ingredient> ingredients;
    public List<GameObject> tealeaves;

    private TeaController teacontroller;
    [SerializeField] bool full;
    // Getters and Setters
    public void setRadius(float _radius)
    {
        radius = _radius;
    }
    public float getRadius()
    {
        return radius;
    }



    // Methods and functions
    public void PickupObject(NavMeshAgent player, List<ingredient> ingredients)
    {
        
        GameObject tea;
        Collider[] hitColliders = Physics.OverlapSphere(player.gameObject.transform.position, radius);
        foreach (var col in hitColliders)
        {
            if (col.gameObject.tag == "tea leaves" && inventorySize < limit)
            {
                inventorySize++;
                tea = col.gameObject;
                tealeaves.Add(tea);

                ingredients.Add(tea.GetComponent<ingredient>());


                Debug.Log("Object : " + col.gameObject.name.ToUpper() + " : picked up.");
                col.gameObject.SetActive(false);
            }
            if (col.gameObject.tag == "bench")
            {
                bench bench = col.gameObject.GetComponent<bench>();

                if (bench.isUsed == true)
                {
                    bench.isUsed = false;
                    
                }
            }

        }
    }

    public void ListInventory()
    {
        Debug.Log("LIST OF CURRENT OBJECTS IN LEAF INVENTORY");
        foreach(var leaves in tealeaves)
        {
            Debug.Log("Object : " + leaves.name.ToUpper());           
        }
    }
   

    public void PlaceObject(NavMeshAgent player, GameObject ob)
    {
       
        
        
        
    }
    private void DestroyObject()
    {

    }

}



