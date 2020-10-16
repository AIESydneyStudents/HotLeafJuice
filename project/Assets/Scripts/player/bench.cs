using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bench : MonoBehaviour
{
    public bool isUsed;
    private List<GameObject> objects;
    bench()
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
