using Unity;
using UnityEditor;
using UnityEngine;
using System;


[CustomEditor(typeof(playerController))]
public class playerEditor : Editor
{
    bool movementShow = true;
    bool cameraShow = true;
    bool interactionShow = true;
    bool teaShow = true;
    bool cookingShow = true;
    bool orderShow = true;

    #region 
    public UnityEngine.Object movement;

    public UnityEngine.Object camera;
    public UnityEngine.Object follow;

    public UnityEngine.Object Interaction;
    public UnityEngine.Object mesh;

    public UnityEngine.Object tea;
    public UnityEngine.Object cooking;
    public UnityEngine.Object order;
    #endregion 


    public override void OnInspectorGUI()
    {
        var controller = target as playerController;

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
    }

    public void OnInspectorUpdate()
    {
        this.Repaint();
    }




}