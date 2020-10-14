using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaController: MonoBehaviour
{

    public List<ingredient> ingredients;

   
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            foreach(var ingred in ingredients)
            {
                Debug.Log(ingred.GetName());
            }
        }
    }
}
