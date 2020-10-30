using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class playerController : MonoBehaviour
{
    /// <summary>
    /// Player controller interaction attributes
    /// </summary>
    #region Class variables
    public bool Movement_Disable;
  
    public player_movement MovementController;
    public float movementSpeed;
  
    private bool ClickToMove;

   
    private bool GridMovementEnabled;
   
    private float gridSnap;
    #endregion

    #region Public Fields for Editor Script
    // Camera Controller

    public bool camera_lock;

    public camerafollow cameraController;

    public Camera playerCamera;

    public float cameraMoveRange;

  
    public float cameraSpeed;



    public player_interaction InteractionController;

    public Transform meshTransform;
  
    public float pickupRange;

    public float inventorySizeLimit;

    public KeyCode InteractionKeybinding = KeyCode.E;

    public KeyCode MenuKeybinding = KeyCode.P;

    public KeyCode InventoryKeybinding = KeyCode.Space;

    public KeyCode PlaceObjectKeybinding = KeyCode.Q;


    // Tea Framework

    public TeaController teaController;

    public List<ingredient> ingredients;

    public CookingStation cookingStationController;

    public ORDER ORDER;
    #endregion
   
    /// <summary>
    /// Called once the scene compiles
    /// </summary>
    void Start()
    {
        MovementController.playerAgent.speed = movementSpeed;
        ConfigInteractionController();
        cameraController.InitCamera();       
        StartLevel();
    }
     /// <summary>
     /// Load needed variables
     /// </summary>
    void StartLevel()
    {
        cookingStationController.loadOrder(ORDER.orderList);
    }  

    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update()
    {
        ingredients = teaController.ingredients;

        // Pick up objects
        if (Input.GetKeyDown(InteractionKeybinding) || Input.GetButtonDown("Fire1"))
        {
            InteractionController.inventoryText.text = " ";

            cookingStationController.Ordertext.text = " ";

            InteractionController.PickupObject(
                MovementController.playerAgent,
                teaController.ingredients );
                        
        
        }
        // Show menu
        if (Input.GetKeyDown(MenuKeybinding))
        {
            Debug.Log("Not yet implemented");
        }
        // Place objects down
        if (Input.GetKeyDown(PlaceObjectKeybinding) || Input.GetButtonDown("Fire2"))
        {
            InteractionController.inventoryText.text = " ";
            cookingStationController.Ordertext.text = " ";

            Collider[] hitColliders = Physics.OverlapSphere(

                MovementController.playerAgent.gameObject.transform.position, 
                0.3f);

            foreach (var col in hitColliders)
            {
                
                if (col.gameObject.tag == "bench")
                {

                    bench bench = col.gameObject.GetComponent<bench>();

                    if (bench.isUsed == false)
                    {
                        GameObject obToPlace = ingredients.First().gameObject;
                        GameObject placed = Instantiate(obToPlace) as GameObject;
                        placed.name = ingredients.First().gameObject.name;
                        InteractionController.inventoryText.text = " ";

                        InteractionController.inventorySize--;

                        placed.transform.position = col.transform.position + new Vector3(0, 1f, 0);
                        ingredients.Remove(ingredients.First());
                        placed.gameObject.SetActive(true);
                        bench.isUsed = true;
                        bench.ObjectOnBench(placed);
                        
                    }
                }

                if (col.gameObject.tag == "Cooking Station")
                {
                    

                    if(cookingStationController.AcceptIngredient(ingredients.First()) == true)
                    {
                        InteractionController.inventoryText.text = " ";

                        InteractionController.inventorySize--;
                        ingredients.Remove(ingredients.First());

                    }
                    else
                    {
                        cookingStationController.stateText.text = "Cannot place that ingredient";
                    }
                    
                }

                if (col.gameObject.tag == "bin")
                {
                    disposeObjects bin = col.GetComponent<disposeObjects>();
                    InteractionController.inventoryText.text = " Object ''" + ingredients.First().name + "'' thrown down sink ";
                    InteractionController.inventorySize--;
                    bin.bin(ingredients.First());
                    ingredients.Remove(ingredients.First());

                    


                }
            }

            
        }

        // Update Camera position
        cameraController.UpdateCamera(
            playerCamera, meshTransform,
            cameraMoveRange, cameraSpeed);

        // Update movement
        ConfigMovementType();


    }

    /// <summary>
    /// Update radius
    /// </summary>
    void ConfigInteractionController()
    {
        InteractionController.setRadius(pickupRange);

    }

    /// <summary>
    /// Set movement type
    /// </summary>
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