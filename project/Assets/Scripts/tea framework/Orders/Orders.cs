using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orders
{

    public string name;
    public List<ingredient> ingredients;
    public int score;

    private int ReturnScore()
    {
        int totalScore = 0;
        foreach (var ingred in ingredients)
        {
            totalScore += ingred.GetScoreNumber();
        }
        return totalScore;
    }
    public Orders(string _name, List<ingredient> _ingredients)
    {
        name = _name;
        ingredients = _ingredients;
        score = ReturnScore();
    }




   
}
