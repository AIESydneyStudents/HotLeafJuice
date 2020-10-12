using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class player_interaction : MonoBehaviour
{
    //[SerializeField] protected GameObject[] ob;
    public float radius;
    




    public void DetectObject(NavMeshAgent player)
    {
        Collider[] hitColliders = Physics.OverlapSphere(player.gameObject.transform.position, radius);
        foreach (var col in hitColliders)
        {
            if (col.gameObject.tag == "interaction")
            {
                col.gameObject.SetActive(false);
            }
            
        }
    }


    public void PickUpObject()
    {

    }

    public void DestroyObject()
    {

    }

    public void CreateObject()
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


