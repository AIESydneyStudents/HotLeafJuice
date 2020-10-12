﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Movement settings
    [Header("Movement Settings")]
    [SerializeField] 
    [Tooltip("Movement controller scriptable object")]
    private player_movement MovementController;
    public float movementSpeed;
    public float cameraSpeed;

    [SerializeField]
    [Tooltip("Enable or Disable click to move")]
    private bool ClickToMove;
    // Camera settings
    [Header("Camera Settings")]
    [SerializeField]
    [Tooltip("Camera Assigned to player")]
    Camera playerCamera;


    // Interaction Settings
    [Header("Interaction Settings")]
    [SerializeField]
    [Tooltip("Interaction Controller Scriptable Object")]
    private player_interaction InteractionController;
    [Tooltip("The range at which an object can be interacted with")]
    [Range(0f,5f)] 
    public float pickupRange;
    [SerializeField]
    [Tooltip("Keybinding for interacting with objects")]
    private KeyCode InteractionKeybinding;
    [SerializeField]
    [Tooltip("Keybinding to open menu")]
    private KeyCode MenuKeybinding;
    [SerializeField]
    [Tooltip("Inventory Keybinding")]
    private KeyCode InventoryKeybinding;

    // Start is called before the first frame update
    void Start()
    {
        MovementController.playerAgent.speed = movementSpeed;
        ConfigInteractionController();
    }


    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(InteractionKeybinding))
        {
            InteractionController.PickupObject(MovementController.playerAgent);
        }
        if (Input.GetKeyDown(MenuKeybinding))
        {
            Debug.Log("Not yet implemented");
        }
        if (Input.GetKeyDown(InventoryKeybinding))
        {
            InteractionController.ListInventory();
        }
        
        ConfigMovementType();

    }

    void ConfigInteractionController()
    {
        InteractionController.setRadius(pickupRange);

    }
    void ConfigMovementType()
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
