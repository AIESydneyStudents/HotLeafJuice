using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollow : MonoBehaviour
{
    // Class properties 
    #region
    private Vector3 sidescroll;
    private Vector3 old;
    [SerializeField] private float defaultPosition;
    [SerializeField] private float zoffset = -7;
    [SerializeField] private Collider cameraStop;
    [SerializeField] private bool xAxis;
    [SerializeField] private bool YAxis;
    #endregion

    // Init the properties of the camera controller
    public void InitCamera()
    {
        sidescroll = transform.position;
    }

    // Update camera position
    public void UpdateCamera(Camera camera, Transform PlayerTransform, float cameraMoveRange, float cameraSpeed)
    {

        if (YAxis == true && xAxis == true)
        {

            if (PlayerTransform.position.x <= -cameraMoveRange || PlayerTransform.position.x >= cameraMoveRange)
            {

                sidescroll.x = PlayerTransform.position.x;
                sidescroll.y = camera.transform.position.y;              
                sidescroll.z = PlayerTransform.transform.position.z + zoffset;
                
            }
            else
            {
                cameraSpeed = 1f;
                sidescroll.x = defaultPosition;
                sidescroll.y = camera.transform.position.y;
                sidescroll.z = camera.transform.position.z;
            }


            if (PlayerTransform.position.x <= cameraStop.gameObject.transform.position.x)
            {
                camera.transform.position = Vector3.Lerp(old,
                    new Vector3(cameraStop.gameObject.transform.position.x,
                    camera.transform.position.y, PlayerTransform.transform.position.z + zoffset),
                    cameraSpeed * Time.deltaTime);
            }

            old = camera.transform.position;
            camera.transform.position = Vector3.Lerp(old, sidescroll, cameraSpeed * Time.deltaTime);

        }

        if (xAxis == true)
        {
            if (PlayerTransform.position.x <= -cameraMoveRange || PlayerTransform.position.x >= cameraMoveRange)
            {

                sidescroll.x = PlayerTransform.position.x;
                sidescroll.y = camera.transform.position.y;
                sidescroll.z = camera.transform.position.z;

            }
            else
            {
                cameraSpeed = 1f;
                sidescroll.x = defaultPosition;
                sidescroll.y = camera.transform.position.y;
                sidescroll.z = camera.transform.position.z;
            }


            if (PlayerTransform.position.x <= cameraStop.gameObject.transform.position.x)
            {
                camera.transform.position = Vector3.Lerp(old,
                    new Vector3(cameraStop.gameObject.transform.position.x,
                    camera.transform.position.y, PlayerTransform.transform.position.z + zoffset),
                    cameraSpeed * Time.deltaTime);
            }

            old = camera.transform.position;
            camera.transform.position = Vector3.Lerp(old, sidescroll, cameraSpeed * Time.deltaTime);
        }
       
        if (YAxis == true)
        {
            if (PlayerTransform.position.x <= -cameraMoveRange || PlayerTransform.position.x >= cameraMoveRange)
            {

                sidescroll.x = camera.transform.position.x;
                sidescroll.y = camera.transform.position.y;
                sidescroll.z = PlayerTransform.transform.position.z + zoffset;

            }
            else
            {
                cameraSpeed = 1f;
                sidescroll.x = defaultPosition;
                sidescroll.y = camera.transform.position.y;
                sidescroll.z = camera.transform.position.z;
            }


            if (PlayerTransform.position.x <= cameraStop.gameObject.transform.position.x)
            {
                camera.transform.position = Vector3.Lerp(old,
                    new Vector3(cameraStop.gameObject.transform.position.x,
                    camera.transform.position.y, PlayerTransform.transform.position.z + zoffset),
                    cameraSpeed * Time.deltaTime);
            }

            old = camera.transform.position;
            camera.transform.position = Vector3.Lerp(old, sidescroll, cameraSpeed * Time.deltaTime);


        }

    }

}
