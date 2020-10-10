using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System.Runtime.CompilerServices;

public class player_movement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent playerAgent;
    [SerializeField] private bool clickToMove;


    private Vector3 forward;
    private float xPos = 0;
    private float yPos = 0;
  
    void PointToMove()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                playerAgent.destination = hit.point;
            }
        }
    }  

    void ControllerMovement()
    {

        yPos = Input.GetAxis("Horizontal");
        xPos = Input.GetAxis("Vertical");
        transform.Rotate(0, yPos, 0);
        forward.x += xPos;
        transform.position += this.transform.forward * xPos * playerAgent.speed * Time.deltaTime;
        xPos = 0;
        yPos = 0;
        forward.x = 0;

    }


    // Update is called once per frame
    void Update()
    {
        switch (clickToMove)
        {
            case true:
                PointToMove();
                break;          
            case false:
                ControllerMovement();
                break;

        }
    }
}
