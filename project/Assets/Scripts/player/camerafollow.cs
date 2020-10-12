using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollow : MonoBehaviour
{

    private Vector3 sidescroll;
    private Vector3 old;



    public void InitCamera()
    {
        sidescroll = transform.position;
    }

    public void UpdateCamera(Camera camera, Transform PlayerTransform, float cameraMoveRange, float cameraSpeed)
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
            sidescroll.x = 0;
            sidescroll.y = camera.transform.position.y;
            sidescroll.z = camera.transform.position.z;
        }

        old = camera.transform.position;
        camera.transform.position = Vector3.Lerp(old, sidescroll, cameraSpeed * Time.deltaTime);


    }
}
