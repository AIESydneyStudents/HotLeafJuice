using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disposeObjects : MonoBehaviour
{
    // Start is called before the first frame update
    private List<ingredient> playerInventory;
    private List<ingredient> orders;

    
    public void bin(ingredient objectTo)
    {

        Destroy(objectTo.gameObject);

    }


    



}
