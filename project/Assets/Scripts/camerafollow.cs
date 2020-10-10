using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollow : MonoBehaviour
{
    [SerializeField] Transform PlayerTransform;
    [SerializeField] float zOffset;
    [SerializeField] float cameraMoveRange;
    [SerializeField] float cameraMoveSpeed;
    private Vector3 sidescroll;
    private Vector3 old;
    private float cameraSpeed;
    
    // Update is called once per frame


    private void Start()
    {
        cameraSpeed = cameraMoveSpeed;
        sidescroll = transform.position;
    }
    void Update()
    {
        cameraSpeed = cameraMoveSpeed;
        if (PlayerTransform.position.x <= -cameraMoveRange || PlayerTransform.position.x >= cameraMoveRange)
        {
            sidescroll.x = PlayerTransform.position.x;
            sidescroll.y = this.transform.position.y;
            sidescroll.z = this.transform.position.z;
        }
        else
        {
            cameraSpeed = 1f;
            sidescroll.x = 0;
            sidescroll.y = this.transform.position.y;
            sidescroll.z = this.transform.position.z;
        }

        old = this.transform.position;
        transform.position = Vector3.Lerp(old, sidescroll, cameraSpeed * Time.deltaTime);


        Debug.Log(old.x);
    }
}
