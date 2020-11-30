using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    public NavMeshAgent NPC;
    [SerializeField] private GameObject recieveTexture;
    public GameObject target;
    public Transform spawnLocation;
    public Transform leaveLocation;
    public List<NPC> npcList = new List<NPC>();
    public List<GameObject> toMove = new List<GameObject>();
   
  
    // Start is called before the first frame update
    public void Spawn()
    {
        
        foreach (var npc in npcList)
        {

            GameObject obj = NPC.gameObject;
            GameObject placed = Instantiate(obj) as GameObject;

            placed.AddComponent<scriptableNPC>();
            //placed.AddComponent<NavMeshAgent>();

            scriptableNPC placedNPC = placed.GetComponent<scriptableNPC>();
            placedNPC.npcName = npc.npcName;
            placedNPC.order = npc.orderForNPC;
            placedNPC.diffLevel = npc.diffLevel;
            placed.transform.position = spawnLocation.position;

            toMove.Add(placed);

        }
        
    }


    public void Set(List<NPC> _list)
    {
        NPC.gameObject.transform.position = spawnLocation.position;
        npcList = _list;

    }

    public List<GameObject> GetNPCs()
    {
        return toMove;
    }

    public void SetNPCs(List<GameObject> objects)
    {
        toMove = objects;
    }
 
    private void Update()
    {

        foreach (var npc in toMove)
        {

            if(npc.GetComponent<scriptableNPC>().isDone == true)
            {
                npc.SetActive(false);
            }
            else{
            
                npc.GetComponent<NavMeshAgent>().SetDestination(target.gameObject.transform.position);
                scriptableNPC placedNPC = npc.GetComponent<scriptableNPC>();
                Collider[] colliders = Physics.OverlapSphere( npc.gameObject.transform.position, 1f);
            }
            
           
        }
      
    }
}
