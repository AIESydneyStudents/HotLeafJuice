using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headobj : MonoBehaviour
{
    public Transform anchor;

    GameObject obj;

    public void SetHeadObject(GameObject _obj)
    {
        obj = Instantiate(_obj) as GameObject;


        obj.SetActive(true);
    }

    public void RemoveHeadObject()
    {
        obj.SetActive(false);
    }


    
}
