using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System.Runtime.CompilerServices;

public class player_movement : MonoBehaviour
{
    public NavMeshAgent playerAgent;
    
    private Vector3 forward;
    private float xPos = 0;
    private float yPos = 0;
  
    public void PointToMove(Camera playerCamera)
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(playerCamera.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                playerAgent.destination = hit.point;
            }
        }
    }  

    public void ControllerMovement(float dt)
    {
        
        yPos = Input.GetAxis("Horizontal");
        xPos = Input.GetAxis("Vertical");
        playerAgent.gameObject.transform.Rotate(0, yPos, 0);
        forward.x += xPos;
        playerAgent.gameObject.transform.position += playerAgent.gameObject.transform.forward * xPos * playerAgent.speed * dt;
        xPos = 0;
        yPos = 0;
        forward.x = 0;

    }

    


   
}
