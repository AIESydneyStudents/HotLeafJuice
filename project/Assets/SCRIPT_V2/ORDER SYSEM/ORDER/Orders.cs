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

    
    public Timer(float length)
    {
        timeRemaining = length;
        isRunning = true;
        initialTime = length;
    }
   
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

   
   
    public void StopTimer()
    {
        isRunning = false;
    }

    
    public void StartTimer()
    {
        isRunning = true;
        timeRemaining = initialTime;
    }

    
    public void StartTimer(float length)
    {
        isRunning = true;
        timeRemaining = length;
    }
   
}






 [System.Serializable]
public class Orders
{
    
    #region 
    public string name;
    public List<ingredient> ingredients;
    public int score;
    public float orderTimer;
    public int diffLevel;
  
    

    #endregion

   
    private int ReturnScore()
    {
        int totalScore = 0;
        
        return totalScore;
    }

    
    public Orders(string _name, List<ingredient> _ingredients, float timer, int _difflevel)
    {
        name = _name;
        ingredients = _ingredients;
        score = ReturnScore();
        orderTimer = timer;
        diffLevel = _difflevel;
    }

   
}
