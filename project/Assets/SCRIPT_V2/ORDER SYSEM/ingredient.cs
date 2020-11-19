using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

/// <summary>
/// Ingredient scriptable object base class
/// </summary>


public class ingredient : MonoBehaviour
{
    /// <summary>
    /// Class properties
    /// </summary>
    #region
    public string Objectname;
    protected int scoreNumber;
    #endregion

    /// <summary>
    /// Set object name
    /// </summary>
    /// <param name="_name"></param>
    public void SetName(string _name)
    {
        Objectname = _name;
    }

    /// <summary>
    /// Return object name
    /// </summary>
    /// <returns></returns>
    public string GetName()
    {
        return Objectname;
    }

    /// <summary>
    /// Set the score number of the object
    /// </summary>
    /// <param name="score"></param>
    public void SetScoreNumber(int score)
    {
        scoreNumber = score;
    }

    /// <summary>
    /// Return object score number
    /// </summary>
    /// <returns></returns>
    public int GetScoreNumber()
    {
        return scoreNumber;
    }

    

}
