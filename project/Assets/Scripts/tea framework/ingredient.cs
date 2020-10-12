using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ingredient : MonoBehaviour
{

    protected string name;
    protected int scoreNumber;

    public void SetName(string _name)
    {
        name = _name;
    }

    public string GetName()
    {
        return name;
    }

    public void SetScoreNumber(int score)
    {
        scoreNumber = score;
    }

    public int GetScoreNumber()
    {
        return scoreNumber;
    }

    

}
