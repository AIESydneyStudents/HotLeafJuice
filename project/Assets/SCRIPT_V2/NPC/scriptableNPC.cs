using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptableNPC : MonoBehaviour
{

    [HideInInspector] public string npcName;
    [HideInInspector] public int diffLevel;
    [HideInInspector] public Orders order;
    [HideInInspector] public GameObject headObject;
    [HideInInspector] public bool isDone;

     
    public scriptableNPC(string _name, int _diffLevel, Orders _order, GameObject _head)
    {
        npcName = _name;
        diffLevel = _diffLevel;
        order = _order;
        headObject = _head;
    }


   
}
