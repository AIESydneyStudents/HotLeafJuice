using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cupSpawner : MonoBehaviour
{
    [System.Serializable]
    struct Cups
    {
        public GameObject cup;
    }

    [SerializeField] List<Cups> npcCups;

    private List<GameObject> cups = new List<GameObject>();
    

    private void Start()
    {
        foreach(var cup in npcCups)
        {
            cups.Add(cup.cup);
        }
    }


    int random(int min, int max)
    {
        Random Random = new Random();
        return Random.Range(min,max);
    }


    public GameObject CreateCup()
    {
        //int rand = random(0, 1);
        return cups[0];
    }
    
}
