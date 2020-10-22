using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
                isRunning = false;
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
        isRunning = false;
        timeRemaining = initialTime;
    }
    public void StartTimer(float length)
    {
        isRunning = false;
        timeRemaining = length;
    }

    
   
}


public class Orders
{

    public string name;
    public List<ingredient> ingredients;
    public int score;
    public float orderTimer;

    private int ReturnScore()
    {
        int totalScore = 0;
        foreach (var ingred in ingredients)
        {
            totalScore += ingred.GetScoreNumber();
        }
        return totalScore;
    }
    public Orders(string _name, List<ingredient> _ingredients, float timer)
    {
        name = _name;
        ingredients = _ingredients;
        score = ReturnScore();
        orderTimer = timer;
    }


    private void ExportJSON()
    {

    }

    private void LoadJSON()
    {

    }
}
