﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class player_interaction : MonoBehaviour
{
    // Class properties
    private float radius;
    public List<GameObject> tealeaves;
    

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
    public void PickupObject(NavMeshAgent player)
    {
        GameObject tea;
        Collider[] hitColliders = Physics.OverlapSphere(player.gameObject.transform.position, radius);
        foreach (var col in hitColliders)
        {
            if (col.gameObject.tag == "tea leaves")
            {
                tea = col.gameObject;

                tealeaves.Add(tea);

                Debug.Log("Object : " + col.gameObject.name.ToUpper() + " : picked up.");
                col.gameObject.SetActive(false);
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
    


    public void PlaceObject(GameObject ob)
    {
        tealeaves.Remove(tealeaves.First());
        GameObject placed = Instantiate(ob) as GameObject;
        placed.transform.position = ob.transform.position;
        placed.gameObject.SetActive(true);
    }
    private void DestroyObject()
    {

    }

}

public class tea_controller : player_interaction
{
    protected void CreateTea()
    {

    }


    protected void DestroyTea()
    {

    }


}

