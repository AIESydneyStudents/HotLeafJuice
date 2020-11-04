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



public class NPCEditor : MonoBehaviour
{


    [SerializeField] private Spawner spawner;
    [SerializeField] private int SpawnDelay;
    private List<Orders> orders;
    

    private List<Orders> level1 = null;
    private List<Orders> level2 = null;
    private List<Orders> level3 = null;

    private System.Random rand;

    [HideInInspector]
    public List<NPC> MainNPCsList;


    [System.Serializable]
    public struct npcEditor
    {
        public string npc_name;
        [Range(1,3)] public int difficultyLevel;
        /// <summary>
        ///  Load JSON Script for level here
        /// </summary>
        public Mesh headMesh;
    }

    public List<npcEditor> NPC_Editor;


    public void GetOrders(List<Orders> a)
    {
        orders = a;
    }
    // Start is called before the first frame update
    void Start()
    {

        rand = new System.Random();
        foreach (var npc in NPC_Editor)

        {
            LoadDiffOrder(npc.difficultyLevel);

            NPC newnpc = new NPC(npc.npc_name, spawner.transform);
            newnpc.SetType(npc.headMesh, npc.difficultyLevel);
            newnpc.orderForNPC = getOrder(npc.difficultyLevel);
            MainNPCsList.Add(newnpc);

        }



        foreach (var npc in MainNPCsList)
        {
            Debug.Log(npc.orderForNPC.diffLevel);
        }
    }


    public void LoadDiffOrder(int _diffLevel)
    {
        
        // Split orders into 3 levels
        foreach (var order in orders)
        {
            if (order.diffLevel == 1)
            {
                level1.Add(order);

            }
            if (order.diffLevel == 2)
            {
                level2.Add(order);

            }
            if (order.diffLevel == 3)
            {
                level3.Add(order);

            }
        }
    }

    private Orders getOrder(int level)
    {
        if (level == 1)
        {
            Orders order = level1[rand.Next(level1.Count)];
            return order;
        }
        if (level == 2)
        {
            Orders order = level2[rand.Next(level2.Count)];
            return order;
        }
        if (level == 3)
        {
            Orders order = level3[rand.Next(level3.Count)];
            return order;
        }

        else
        {
            return null;
        }
    }
    


   

    

    

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
