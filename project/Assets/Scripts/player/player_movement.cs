using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System.Runtime.CompilerServices;

public class player_movement : MonoBehaviour
{
    public NavMeshAgent playerAgent;
    
    private Vector3 forward;
    private float zPos = 0;
    private float xPos = 0;
    private bool rotated = false;
  
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

    public float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
        {
            angle += 360f;
        }
        if (angle > 360f)
        {
            angle -= 360f;
        }
        return Mathf.Clamp(angle, min, max);
    }

    private void Rotate(string direction)
    {
        if (direction == "Right")
        {
            playerAgent.transform.Rotate(0, 90f, 0);
            return;
        }
        if (direction == "Left")
        {
            playerAgent.transform.Rotate(0, -90f, 0);
            return;
        }
    }

    public void ControllerMovement(float dt)
    {



        xPos = Input.GetAxis("Horizontal");
        zPos = Input.GetAxis("Vertical");

         // Moving on the X axis
        if (xPos > 0)
        {
            playerAgent.transform.position += new Vector3(xPos * playerAgent.speed * dt, 0, 0);
        }
        if (xPos < 0)
        {
            playerAgent.transform.position += new Vector3(xPos * playerAgent.speed * dt, 0, 0);
        }

        // Moving on the z axis

        if (zPos > 0)
        {
            playerAgent.transform.position += new Vector3(0, 0, zPos * playerAgent.speed * dt);
        }

        if (zPos < 0)
        {
            playerAgent.transform.position += new Vector3(0, 0, zPos * playerAgent.speed * dt);
        }

        

        zPos = 0;
        xPos = 0;

    }

    private Vector3 SnapToGrid(Transform obj, float gridSnap)
    {
        Vector3 pos = obj.transform.position;
        Vector3 snapHits = new Vector3(Mathf.Round(pos.x / gridSnap) * gridSnap, pos.y, Mathf.Round(pos.z / gridSnap) * gridSnap);
        Vector3 snapTransform = new Vector3(snapHits.x - (obj.transform.localScale.x / 2.0f), pos.y, snapHits.z - (obj.transform.localScale.z / 2.0f));
        pos = snapTransform;
        return pos;
    }

    public void GridMovement(float dt, float gridSnap)
    {
        
      

    }


    public bool isActive()
    {
        return false;
    }

   
}
