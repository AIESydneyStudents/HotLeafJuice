using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ingredient : MonoBehaviour
{

    public string Objectname;
    protected int scoreNumber;

    public void SetName(string _name)
    {
        Objectname = _name;
    }

    public string GetName()
    {
        return Objectname;
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
