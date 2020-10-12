using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float movementSpeed;
    public float cameraSpeed;

    [SerializeField] Camera playerCamera;
    [SerializeField] private player_movement MovementController;
    [SerializeField] private bool ClickToMove;
    // Start is called before the first frame update
    void Start()
    {
        MovementController.playerAgent.speed = movementSpeed;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ClickToMove == true)
        {
            MovementController.PointToMove(playerCamera);
        }
        if (ClickToMove == false)
        {
            MovementController.ControllerMovement(Time.deltaTime);
        }
        
    }
}
