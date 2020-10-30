//using UnityEngine;
//using UnityEditor;
//using System;

//[CustomEditor(typeof(ORDER))]
//public class attributeHide : Editor
//{
//    override public void OnInspectorGUI()
//    {
//        var orders = target as ORDER;


//        orders.jsonOptions = EditorGUILayout.Toggle("Disable JSON Tools", orders.jsonOptions);

//        using (new EditorGUI.DisabledScope(orders.jsonOptions))
//        {
            
//                EditorGUI.indentLevel++;
//                EditorGUILayout.PrefixLabel("Import JSON");
//                orders.importJSON = EditorGUILayout.Toggle(orders.importJSON);

//                EditorGUILayout.PrefixLabel("Export JSON");
//                orders.exportJSON = EditorGUILayout.Toggle(orders.exportJSON);

             
//        }

//        orders.disableEditor = EditorGUILayout.Toggle("Disable Editor Tool", orders.disableEditor);

        


//    }
//}
