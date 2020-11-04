using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class NPC
{


    public string npcName;

    /// <summary>
    /// Difficulty based on level number. 1 = easy, 2 = medium, 3 = hard
    /// </summary>
    public int diffLevel;

    private Vector3 position;
    private Mesh headType;
    public Orders orderForNPC;

    public NPC()
    {
        position = new Vector3(0,0,0);
        npcName = "Default";
        diffLevel = 0;

    }

    public NPC(string _npcName, Transform spawnLocation, int _diffLevel = 0)
    {
        position = spawnLocation.position;
        npcName = _npcName;
        diffLevel = _diffLevel;
    }


    public void SetType(Mesh head, int difficultyLevel)
    {
        headType = head;
        diffLevel = difficultyLevel;
    }

    /// <summary>
    /// Spawn NPC at transform
    /// </summary>
    /// <returns></returns>
    public Vector3 Spawn()
    {
        return position;
    }

}



