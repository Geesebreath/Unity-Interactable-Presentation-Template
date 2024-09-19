using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TextTimer))]
[CanEditMultipleObjects]
public class ETextTimer : Editor
{
    SerializedProperty useAsStopwatch;

    SerializedProperty timerText;

    //Shared Properties
    SerializedProperty initialTimerString, timeMultiplier, timerEndingEvent;

    SerializedProperty finalTimerValue;

    private void OnEnable()
    {
        useAsStopwatch = serializedObject.FindProperty("useAsStopwatch");

        timerText = serializedObject.FindProperty("timerText");

        initialTimerString = serializedObject.FindProperty("initialTimerString");
        timeMultiplier = serializedObject.FindProperty("timeMultiplier");
        timerEndingEvent = serializedObject.FindProperty("timerEndingEvent");

        finalTimerValue = serializedObject.FindProperty("finalTimerValue");
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        serializedObject.Update();
        GUI.enabled = false;
        EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((TextTimer)target), typeof(TextTimer), false);
        GUI.enabled = true;
        EditorGUILayout.PropertyField(timerText);

        EditorGUILayout.PropertyField(useAsStopwatch);
        EditorGUILayout.PropertyField(initialTimerString);
        if(useAsStopwatch.boolValue)
        {
            EditorGUILayout.PropertyField(finalTimerValue);
        }
        EditorGUILayout.PropertyField(timeMultiplier);
        EditorGUILayout.PropertyField(timerEndingEvent);

        serializedObject.ApplyModifiedProperties();
    }
}
