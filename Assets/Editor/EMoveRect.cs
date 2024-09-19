using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MoveRect))]
[CanEditMultipleObjects]
public class EMoveRect : Editor
{
    // this are serialized variables in YourClass
    SerializedProperty movementAllow, rotationAllow;
    SerializedProperty rect;
    SerializedProperty startPosition, finalPosition, moveSpeed, moveStartedActions, moveEndedActions, FinishedAtStart, FinishedAtEnd;
    SerializedProperty targetRotationValue, rotationSpeed, rotateEndingEvent, PostRotationEventOnlyOnce;

	private void OnEnable()
	{
        movementAllow = serializedObject.FindProperty("movementAllow");
        rotationAllow = serializedObject.FindProperty("rotationAllow");

        rect = serializedObject.FindProperty("rect");

		startPosition = serializedObject.FindProperty("startPosition");
		finalPosition = serializedObject.FindProperty("finalPosition");

        moveSpeed = serializedObject.FindProperty("moveSpeed");
        moveStartedActions = serializedObject.FindProperty("moveStartedActions");
        moveEndedActions = serializedObject.FindProperty("moveEndedActions");
        FinishedAtStart = serializedObject.FindProperty("FinishedAtStart");
        FinishedAtEnd = serializedObject.FindProperty("FinishedAtEnd");

        targetRotationValue = serializedObject.FindProperty("targetRotationValue");
        rotationSpeed = serializedObject.FindProperty("rotationSpeed");
        rotateEndingEvent = serializedObject.FindProperty("rotateEndingEvent");
        PostRotationEventOnlyOnce = serializedObject.FindProperty("PostRotationEventOnlyOnce");
    }

    public override void OnInspectorGUI()
    {
        // add this to render base
        //base.OnInspectorGUI();
        serializedObject.Update();
        GUI.enabled = false;
        EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((MoveRect)target), typeof(MoveRect), false);
        GUI.enabled = true;
        EditorGUILayout.PropertyField(rect);
        EditorGUILayout.PropertyField(movementAllow);
        if (movementAllow.boolValue)
        {
			EditorGUILayout.PropertyField(startPosition);
			EditorGUILayout.PropertyField(finalPosition);
            EditorGUILayout.PropertyField(moveSpeed);
            EditorGUILayout.PropertyField(moveStartedActions);
            EditorGUILayout.PropertyField(moveEndedActions);
            EditorGUILayout.PropertyField(FinishedAtStart);
            EditorGUILayout.PropertyField(FinishedAtEnd);
        }
        EditorGUILayout.PropertyField(rotationAllow);
        if (rotationAllow.boolValue)
        {
            EditorGUILayout.PropertyField(targetRotationValue);
            EditorGUILayout.PropertyField(rotationSpeed);
            EditorGUILayout.PropertyField(rotateEndingEvent);
            EditorGUILayout.PropertyField(PostRotationEventOnlyOnce);
        }
            // must be on the end.
            serializedObject.ApplyModifiedProperties();
    }
}
