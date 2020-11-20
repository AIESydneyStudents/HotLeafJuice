using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bin object class 
/// </summary>
public class disposeObjects : MonoBehaviour
{
    
    #region
    // Start is called before the first frame update
    private List<ingredient> playerInventory;
    private List<ingredient> orders;
    #endregion

    /// <summary>
    /// Delete object for BIN object
    /// </summary>
    /// <param name="objectTo"></param>
    public void bin(ingredient objectTo)
    {

        objectTo.gameObject.SetActive(false);

    }


}
