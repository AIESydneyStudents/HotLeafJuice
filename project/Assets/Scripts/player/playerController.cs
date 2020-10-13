﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Movement settings
    [Header("Movement Settings")]
    [SerializeField] 
    [Tooltip("Movement controller scriptable object")]
    private player_movement MovementController;
    public float movementSpeed;
    [SerializeField]
    [Tooltip("Enable or Disable click to move")]
    private bool ClickToMove;


    // Camera settings
    [Header("Camera Settings")]
    [SerializeField]
    [Tooltip("Camera controller")]
    private camerafollow cameraController;
    [SerializeField]
    [Tooltip("Camera Assigned to player")]
    Camera playerCamera;

    [SerializeField]
    [Range(0, 10)]
    private float cameraMoveRange;

    [Range(1, 10)]
    public float cameraSpeed;

    // Interaction Settings
    [Header("Interaction Settings")]
    [SerializeField]
    [Tooltip("Interaction Controller Scriptable Object")]
    private player_interaction InteractionController;
    [SerializeField]
    private Transform meshTransform;
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
    [SerializeField]
    [Tooltip("Place Object KeyBinding")]
    private KeyCode PlaceObjectKeybinding;

    // Start is called before the first frame update
    void Start()
    {
        MovementController.playerAgent.speed = movementSpeed;
        ConfigInteractionController();
        cameraController.InitCamera();
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
        if (Input.GetKeyDown(PlaceObjectKeybinding))
        {
            GameObject obToPlace = InteractionController.tealeaves.First();
            obToPlace.transform.position = meshTransform.position;
            
            InteractionController.PlaceObject(obToPlace);
        }
        cameraController.UpdateCamera(playerCamera, meshTransform, cameraMoveRange, cameraSpeed);
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
