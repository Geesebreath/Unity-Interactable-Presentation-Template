using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// When passed a Scriptable Object Event, compares stored values with values in event.
/// Triggers Unity Actions if values match.
/// </summary>
public class SOECheckAndAction : MonoBehaviour
{
	/// <summary>
	/// Optional, used if you want to store a Scriptable Object Event that may be updated elsewhere and 
	/// compare to this stored Event.
	/// </summary>
    [SerializeField]
    private GameEvent SoEvent;

    public GameObject expectedGameObject;
    public int expectedInt;
    public bool expectedBool;
    public float expectedFloat;

    public UnityEvent actionOnMatch;
	public void CompareStoredEventValues()
	{
		CompareEventValues(SoEvent);
	}

    public void CompareEventValues(GameEvent gEvent)
    {
		if (expectedBool == gEvent.BoolValue &&
					expectedFloat == gEvent.FloatValue &&
					expectedGameObject == gEvent.GameObject &&
					expectedInt == gEvent.IntValue)
		{
			ExecuteActions();
		}
		else
		{
			Debug.Log(gEvent + " values do not match");
		}
	}
   
    public void ExecuteActions()
	{
        actionOnMatch.Invoke();
	}
}
