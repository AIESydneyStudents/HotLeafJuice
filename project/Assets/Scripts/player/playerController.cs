using System.Collections;
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
    

    
    // Start is called before the first frame update
    void Start()
    {
        MovementController.playerAgent.speed = movementSpeed;
        ConfigInteractionController();
    }


    
    // Update is called once per frame
    void Update()
    {

        InteractionController.DetectObject(MovementController.playerAgent);
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
