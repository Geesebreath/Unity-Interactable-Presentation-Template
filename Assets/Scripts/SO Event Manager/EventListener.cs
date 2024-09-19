using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Uses Scriptable Object events to trigger Unity actions. This component executes events when the SO event is raised.
/// </summary>
public class EventListener : MonoBehaviour
{
	// List for all events given to this component
	public List<LinkedEventResponse> linkedEventResponses = new List<LinkedEventResponse>();

	/// <summary>
	/// If there are events to register, registr to every event in the list
	/// </summary>
	private void OnEnable()
	{
		if(linkedEventResponses.Count>0)
		{
			foreach(LinkedEventResponse lER in linkedEventResponses)
			{
				lER.gameEvent.RegisterListener(this);
			}
		}
	}

	/// <summary>
	/// If there are events to unregister, unregister every event in the list
	/// </summary>
	private void OnDisable()
	{
		if (linkedEventResponses.Count > 0)
		{
			foreach (LinkedEventResponse lER in linkedEventResponses)
			{
				lER.gameEvent.UnregisterListener(this);
			}
		}
	}

	public void OnEventRaised(GameEvent passedEvent)
	{
		for(int i = linkedEventResponses.Count-1; i>=0; i--)
		{
			if(linkedEventResponses[i].gameEvent==passedEvent)
			{
				linkedEventResponses[i].EventRaised();
			}
		}
	}

}

/// <summary>
/// Responses for what to do with the data contained within the event. Add a response here and below if
/// adding a data type to GameEvent.
/// </summary>
[System.Serializable]
public class ResponseGameObject : UnityEvent<GameObject>
{ }

[System.Serializable]
public class ResponseInt : UnityEvent<int>
{ }

[System.Serializable]
public class ResponseFloat : UnityEvent<float>
{ }

[System.Serializable]
public class ResponseBool : UnityEvent<bool>
{ }

[System.Serializable]
public class ResponseScriptableObject : UnityEvent<ScriptableObject>
{ }

[System.Serializable]
public class LinkedEventResponse
{
	public GameEvent gameEvent;
	public UnityEvent response;
	public ResponseGameObject responseGameObject;
	public ResponseScriptableObject responseScriptableObject;
	public ResponseInt responseInt;
	public ResponseFloat responseFloat;
	public ResponseBool responseBool;
	public void EventRaised()
	{
		if (response != null)
		{
			response.Invoke();
		}

		if (responseGameObject != null)
		{
			responseGameObject.Invoke(gameEvent.GameObject);
		}

		if (responseScriptableObject != null)
		{
			responseScriptableObject.Invoke(gameEvent.ScriptableObject);
		}

		if (responseInt != null)
		{
			responseInt.Invoke(gameEvent.IntValue);
		}

		if (responseFloat != null)
		{
			responseFloat.Invoke(gameEvent.FloatValue);
		}

		if(responseBool!=null)
		{
			responseBool.Invoke(gameEvent.BoolValue);
		}
	}
}

