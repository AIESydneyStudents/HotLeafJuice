using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bench : MonoBehaviour
{
    public bool isUsed;
    public bool isStove;
    private List<GameObject> objects = new List<GameObject>();
    public bench()
    {
        
        isUsed = false;
    }

    public void ObjectOnBench(GameObject @object)
    {
        objects.Add(@object);
    }

    public void Pickedup(GameObject @object)
    {
        objects.Remove(@object);

    }
}
