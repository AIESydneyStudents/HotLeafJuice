using System.Collections;
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

    [Header("Grid Movement")]
    [SerializeField]
    private bool GridMovementEnabled;
    [SerializeField]
    private float gridSnap;


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

    // Tea Framework
    [Header("Tea Framework")]
    [SerializeField]
    private TeaController teaController;

    [SerializeField]
    [Tooltip("Player Ingredients")]
    private List<ingredient> ingredients;


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
        ingredients = teaController.ingredients;


        if (Input.GetKeyDown(InteractionKeybinding) || Input.GetButtonDown("Fire1"))
        {
            InteractionController.PickupObject(MovementController.playerAgent, teaController.ingredients);
        }
        if (Input.GetKeyDown(MenuKeybinding))
        {
            Debug.Log("Not yet implemented");
        }
        if (Input.GetKeyDown(InventoryKeybinding))
        {
            InteractionController.ListInventory();
        }
        if (Input.GetKeyDown(PlaceObjectKeybinding) || Input.GetButtonDown("Fire2"))
        {

            Collider[] hitColliders = Physics.OverlapSphere(MovementController.playerAgent.gameObject.transform.position, 0.4f);
            foreach (var col in hitColliders)
            {
                
                if (col.gameObject.tag == "bench")
                {
                    GameObject obToPlace = ingredients.First().gameObject;
                    GameObject placed = Instantiate(obToPlace) as GameObject;

                    placed.transform.position = col.transform.position + new Vector3(0, 1f, 0);
                    ingredients.Remove(ingredients.First());
                    placed.gameObject.SetActive(true);
                }
            }
            
            
            

            
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
