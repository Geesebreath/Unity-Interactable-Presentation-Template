using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ActionOnCallback))]
[CanEditMultipleObjects]
public class EActionOnCallback : Editor
{
        // this are serialized variables in YourClass
        SerializedProperty awake, onEnable, start, reset, onDisable, onDestroy;
        SerializedProperty awakeAction, onEnableAction, onDisableAction, resetAction, startAction, onDestroyAction;

    private void OnEnable()
    {
        awake = serializedObject.FindProperty("awake");
        onEnable = serializedObject.FindProperty("onEnable");
        start = serializedObject.FindProperty("start");
        onDisable = serializedObject.FindProperty("onDisable");
        reset = serializedObject.FindProperty("reset");
        onDestroy = serializedObject.FindProperty("onDestroy");

        awakeAction = serializedObject.FindProperty("awakeAction");
        onEnableAction = serializedObject.FindProperty("onEnableAction");
        onDisableAction = serializedObject.FindProperty("onDisableAction");
        resetAction = serializedObject.FindProperty("resetAction");
        startAction = serializedObject.FindProperty("startAction");
        onDestroyAction = serializedObject.FindProperty("onDestroyAction");
    }

        public override void OnInspectorGUI()
        {
        // add this to render base
        base.OnInspectorGUI();
        serializedObject.Update();
        GUI.enabled = false;
        EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((ActionOnCallback)target), typeof(ActionOnCallback), false);
        GUI.enabled = true;
        EditorGUILayout.PropertyField(awake);
        if (awake.boolValue)
        {
            EditorGUILayout.PropertyField(awakeAction);
        }
        EditorGUILayout.PropertyField(onEnable);
        if (onEnable.boolValue)
        {
            EditorGUILayout.PropertyField(onEnableAction);
        }
        EditorGUILayout.PropertyField(start);
        if (start.boolValue)
        {
            EditorGUILayout.PropertyField(startAction);
        }
        EditorGUILayout.PropertyField(onDisable);
        if (onDisable.boolValue)
        {
            EditorGUILayout.PropertyField(onDisableAction);
        }
        EditorGUILayout.PropertyField(reset);
        if (reset.boolValue)
        {
            EditorGUILayout.PropertyField(resetAction);
        }
        EditorGUILayout.PropertyField(onDestroy);
        if (onDestroy.boolValue)
        {
            EditorGUILayout.PropertyField(onDestroyAction);
        }

        // must be on the end.
        serializedObject.ApplyModifiedProperties();



        }
    
}
