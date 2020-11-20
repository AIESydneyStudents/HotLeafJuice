using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System.Runtime.CompilerServices;

public class player_movement : MonoBehaviour
{
    public NavMeshAgent playerAgent;
    
    private float zPos = 0;
    private float xPos = 0;
  
     

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

    public void ControllerMovement(float dt)
    {


        float heading = Mathf.Atan2(-Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        playerAgent.gameObject.transform.rotation = Quaternion.Euler(0f, (-heading * Mathf.Rad2Deg), 0f);




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

   

    public bool isActive()
    {
        return false;
    }

   
}
