using Unity;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Text;
using System;

/// <summary>
/// Target the player controller scriptable object
/// </summary>
[CustomEditor(typeof(playerController))]
public class playerEditor : Editor
{
    /// <summary>
    /// Set the hidden fields variables
    /// </summary>

    #region Show Bools
    bool movementShow = true;
    bool cameraShow = true;
    bool interactionShow = true;
    bool teaShow = true;
    bool JSONOPTIONS = false;
    bool showThing1 = false;

    float option = 0f;

    string filePath = @"C:\TeaTurmoil\config\order.json";

    string text;
    #endregion

    #region Unity Gameobjects
    public UnityEngine.Object movement;

    public UnityEngine.Object camera;
    public UnityEngine.Object follow;

    public UnityEngine.Object Interaction;
    public UnityEngine.Object mesh;

    public UnityEngine.Object tea;
    public UnityEngine.Object cooking;
    public UnityEngine.Object order;
    #endregion 

    /// <summary>
    /// Called to update the info shown on the UI
    /// </summary>
    public override void OnInspectorGUI()
    {
        // Open the script as an object
        var controller = target as playerController;


        showThing1 = EditorGUILayout.BeginToggleGroup("Enable Option", showThing1);
        
        option = EditorGUILayout.FloatField("Thing", option);

        EditorGUILayout.EndToggleGroup();

       

        movementShow = EditorGUILayout.Foldout(movementShow, "Movement Settings");
        if (movementShow)
        {

            EditorGUI.indentLevel++;

            EditorGUILayout.LabelField("Object Controller", EditorStyles.boldLabel);
            movement = EditorGUILayout.ObjectField(movement, typeof(player_movement), true);
            EditorGUILayout.PrefixLabel("Movement Speed");
            controller.movementSpeed = EditorGUILayout.FloatField(controller.movementSpeed);
            EditorGUI.indentLevel--;

        }

        cameraShow = EditorGUILayout.Foldout(cameraShow, "Camera Settings");
        if (cameraShow)
        {
            EditorGUI.indentLevel++;
            
            EditorGUILayout.LabelField("Camera Controller", EditorStyles.boldLabel);

            camera = EditorGUILayout.ObjectField(camera, typeof(camerafollow), true);
            follow = EditorGUILayout.ObjectField(follow, typeof(Camera), true);

            EditorGUILayout.PrefixLabel("Camera Speed");
            controller.cameraSpeed = EditorGUILayout.FloatField(controller.cameraSpeed);

            EditorGUILayout.PrefixLabel("Camera Range");
            controller.cameraMoveRange = EditorGUILayout.Slider(controller.cameraMoveRange, 0, 10);

            EditorGUI.indentLevel--;

        }

        interactionShow = EditorGUILayout.Foldout(interactionShow, "Interaction Settings");
        if (interactionShow)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.LabelField("Interaction Controller", EditorStyles.boldLabel);

            Interaction = EditorGUILayout.ObjectField(Interaction, typeof(player_interaction), true);
            mesh = EditorGUILayout.ObjectField(mesh, typeof(Transform), true);

            EditorGUILayout.PrefixLabel("Pickup Range");
            controller.pickupRange = EditorGUILayout.Slider(controller.pickupRange, 0, 10);

            EditorGUILayout.PrefixLabel("Inventory Size Limit");
            controller.inventorySizeLimit = EditorGUILayout.FloatField(controller.inventorySizeLimit);

            EditorGUI.indentLevel--;
        }

        teaShow = EditorGUILayout.Foldout(teaShow, "Tea Settings");
        if (teaShow)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.LabelField("Tea Controller", EditorStyles.boldLabel);
            tea = EditorGUILayout.ObjectField(tea, typeof(TeaController), true);

            cooking = EditorGUILayout.ObjectField(cooking, typeof(CookingStation), true);

            order = EditorGUILayout.ObjectField(order, typeof(ORDER), true);
            EditorGUI.indentLevel--;
        }




        JSONOPTIONS = EditorGUILayout.Foldout(JSONOPTIONS, "JSON Options");
        if (JSONOPTIONS)
        {
            if (GUILayout.Button("Load JSON"))
            {

                // Load from text file here

                if (File.Exists(filePath))
                {
                    text = File.ReadAllText(filePath);
                }

            }



            EditorGUILayout.PrefixLabel("JSON Preview");


            text = EditorGUILayout.TextField(text, GUILayout.Height(200));
        }
    }


    /// <summary>
    /// Reset the overlay and update the controller
    /// </summary>
    public void OnInspectorUpdate()
    {
        this.Repaint();
    }




}