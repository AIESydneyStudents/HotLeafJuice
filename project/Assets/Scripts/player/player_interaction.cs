using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class player_interaction : MonoBehaviour
{
    //[SerializeField] protected GameObject[] ob;
    private float radius;
    
    public void setRadius(float _radius)
    {
        radius = _radius;
    }

    public float getRadius()
    {
        return radius;
    }



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


    private void PickUpObject()
    {

    }

    private void DestroyObject()
    {

    }

    public void CreateObject(GameObject gameObject)
    {
        GameObject newObject = Instantiate(gameObject) as GameObject;
        newObject.transform.position = gameObject.transform.position;
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


