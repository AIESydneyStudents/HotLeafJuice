using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    public NavMeshAgent NPC;
    public GameObject target;
    public Transform spawnLocation;
    private List<NPC> npcList = new List<NPC>();
    private List<GameObject> toMove = new List<GameObject>();
    [SerializeField] private TMPro.TextMeshProUGUI text;
  
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

   
    private void Update()
    {
        foreach (var npc in toMove)
        {
            npc.GetComponent<NavMeshAgent>().SetDestination(target.gameObject.transform.position);
            scriptableNPC placedNPC = npc.GetComponent<scriptableNPC>();


            Collider[] colliders = Physics.OverlapSphere(
                npc.gameObject.transform.position,
                1f
                );

            foreach(var col in colliders)
            {
                if (col.gameObject.tag == "counter")
                {
                    
                    text.text = placedNPC.order.name.ToString() + "\n";

                  //  Debug.Log(text.text);
                }
            }


        }
      //  NPC.gameObject.SetActive(false);

    }
}
