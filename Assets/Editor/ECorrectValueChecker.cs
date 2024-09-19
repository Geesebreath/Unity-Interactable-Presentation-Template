using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CorrectValueChecker))]
[CanEditMultipleObjects]
public class ECorrectValueChecker : Editor
{
    SerializedProperty correctValue;

    SerializedProperty useConfirmationText, allowCorrectRange;

    SerializedProperty confirmationText;

    SerializedProperty accuracyTolerance;


    SerializedProperty exactString, correctString, incorrectString;
    SerializedProperty errorTextColor, regularTextColor;


    SerializedProperty correctEvent;

    SerializedProperty inRangeEvent;

    SerializedProperty incorrectEvent;

    SerializedProperty resetEvent;


    void OnEnable()
    {
        correctValue = serializedObject.FindProperty("correctValue");

        useConfirmationText = serializedObject.FindProperty("useConfirmationText");
        allowCorrectRange = serializedObject.FindProperty("allowCorrectRange");

        confirmationText = serializedObject.FindProperty("confirmationText");

        accuracyTolerance = serializedObject.FindProperty("accuracyTolerance");


        exactString = serializedObject.FindProperty("exactString");
        correctString = serializedObject.FindProperty("correctString");
        incorrectString = serializedObject.FindProperty("incorrectString");
        errorTextColor = serializedObject.FindProperty("errorTextColor");
        regularTextColor = serializedObject.FindProperty("regularTextColor");


        correctEvent = serializedObject.FindProperty("correctEvent");

        inRangeEvent = serializedObject.FindProperty("inRangeEvent");

        incorrectEvent = serializedObject.FindProperty("incorrectEvent");
        resetEvent = serializedObject.FindProperty("resetEvent");
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        serializedObject.Update();
        GUI.enabled = false;
        EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((CorrectValueChecker)target), typeof(CorrectValueChecker), false);
        GUI.enabled = true;
        EditorGUILayout.PropertyField(correctValue);
        EditorGUILayout.PropertyField(useConfirmationText);
        EditorGUILayout.PropertyField(allowCorrectRange);
        if(useConfirmationText.boolValue)
		{
            EditorGUILayout.PropertyField(confirmationText);
            EditorGUILayout.PropertyField(exactString);
            EditorGUILayout.PropertyField(correctString);
            EditorGUILayout.PropertyField(incorrectString);
            EditorGUILayout.PropertyField(regularTextColor);
            EditorGUILayout.PropertyField(errorTextColor);
        }
        if (allowCorrectRange.boolValue)
        {
            EditorGUILayout.PropertyField(accuracyTolerance);
        }

        EditorGUILayout.PropertyField(correctEvent);
        if (allowCorrectRange.boolValue)
        {
            EditorGUILayout.PropertyField(inRangeEvent);
        }
        EditorGUILayout.PropertyField(incorrectEvent);
        EditorGUILayout.PropertyField(resetEvent);
        serializedObject.ApplyModifiedProperties();
    }
}
