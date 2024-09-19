using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MoveTransform))]
[CanEditMultipleObjects]
public class EMoveTransform : Editor
{
    SerializedProperty movementAllow, rotationAllow;
    SerializedProperty tForm;
    //Movement properties
    SerializedProperty startPosition, endPosition, moveSpeed, moveEndedEvent, finishedAtStartEvent, finishedAtEndEvent, dampLeeway;
    //Rotation properties
    SerializedProperty startRotation, rotationSpeeds;

    private void OnEnable()
    {
        movementAllow = serializedObject.FindProperty("movementAllow");
        rotationAllow = serializedObject.FindProperty("rotationAllow");

        tForm = serializedObject.FindProperty("tform");

        startPosition = serializedObject.FindProperty("startPosition");
        endPosition = serializedObject.FindProperty("endPosition");
        moveSpeed = serializedObject.FindProperty("moveSpeed");
        moveEndedEvent = serializedObject.FindProperty("moveEndedEvent");
        finishedAtStartEvent = serializedObject.FindProperty("finishedAtStartEvent");
        finishedAtEndEvent = serializedObject.FindProperty("finishedAtEndEvent");
        dampLeeway = serializedObject.FindProperty("dampLeeway");

        startRotation = serializedObject.FindProperty("startRotation");
        rotationSpeeds = serializedObject.FindProperty("rotationSpeeds");
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        serializedObject.Update();
        GUI.enabled = false;
        EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((MoveTransform)target), typeof(MoveTransform), false);
        GUI.enabled = true;
        EditorGUILayout.PropertyField(tForm);
        EditorGUILayout.PropertyField(movementAllow);
        if (movementAllow.boolValue)
        {
            EditorGUILayout.PropertyField(startPosition);
            EditorGUILayout.PropertyField(endPosition);
            EditorGUILayout.PropertyField(moveSpeed);
            EditorGUILayout.PropertyField(moveEndedEvent);
            EditorGUILayout.PropertyField(finishedAtStartEvent);
            EditorGUILayout.PropertyField(finishedAtEndEvent);
            EditorGUILayout.PropertyField(dampLeeway);
        }
        EditorGUILayout.PropertyField(rotationAllow);
        if (rotationAllow.boolValue)
        {
            EditorGUILayout.PropertyField(startRotation);
            EditorGUILayout.PropertyField(rotationSpeeds);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
