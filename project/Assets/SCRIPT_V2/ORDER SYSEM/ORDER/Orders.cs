using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Custom timer class
/// </summary>
public class Timer
{
    public float timeRemaining;
    private float initialTime;
    public bool isRunning;

    /// <summary>
    /// Timer constructor: Length is how long the timer lasts
    /// </summary>
    /// <param name="length"></param>
    public Timer(float length)
    {
        timeRemaining = length;
        isRunning = true;
        initialTime = length;
    }
    /// <summary>
    /// Update the time value
    /// </summary>
    /// <returns>remaining time</returns>
    public float Update()
    {
        if (isRunning)
        {
            if (timeRemaining > 0)
            {
                return timeRemaining -= Time.deltaTime;  
            }
            else
            {
                return timeRemaining = 0;
            }
        }
        return 0;
    }

   
    /// <summary>
    /// Stop timer
    /// </summary>
    public void StopTimer()
    {
        isRunning = false;
    }

    /// <summary>
    /// Start timer
    /// </summary>
    public void StartTimer()
    {
        isRunning = true;
        timeRemaining = initialTime;
    }

    /// <summary>
    /// Start timer with new time
    /// </summary>
    /// <param name="length"></param>
    public void StartTimer(float length)
    {
        isRunning = true;
        timeRemaining = length;
    }
   
}





/// <summary>
/// Custom order object to store infomation that is generated via the order editor tool
/// </summary>
/// 
 [System.Serializable]
public class Orders
{
    /// <summary>
    /// Order class properties
    /// </summary>
    #region 
    public string name;
    public List<ingredient> ingredients;
    public int score;
    public float orderTimer;
    public int diffLevel;
  
    

    #endregion

    /// <summary>
    /// Calculate total order score
    /// </summary>
    /// <returns></returns>
    private int ReturnScore()
    {
        int totalScore = 0;
        foreach (var ingred in ingredients)
        {
            totalScore += ingred.GetScoreNumber();
        }
        return totalScore;
    }

    /// <summary>
    /// Order constructor
    /// </summary>
    /// <param name="_name"></param>
    /// <param name="_ingredients"></param>
    /// <param name="timer"></param>
    /// 
    public Orders(string _name, List<ingredient> _ingredients, float timer, int _difflevel)
    {
        name = _name;
        ingredients = _ingredients;
        score = ReturnScore();
        orderTimer = timer;
        diffLevel = _difflevel;
    }

    /// <summary>
    /// Export JSON file containing order infomation
    /// </summary>
    public void ExportJSON()
    {
        Debug.Log("Invoke Export");
    }

    /// <summary>
    /// Import JSON file containing order infomation
    /// </summary>
    public void LoadJSON()
    {
        Debug.Log("Invoke Import");
    }

}
