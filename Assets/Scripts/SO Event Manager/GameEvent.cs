using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable Object used by the SO Event system to identify the specific event raised (by object) and
/// allows the event to effectively store arbitrary data to pass to any component that may be listening for
/// that event.
/// </summary>
[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
	/// <summary>
	/// Optional viewable comment in editor to describe what the event is used for. Unused for functionality.
	/// </summary>
	[SerializeField]
	[TextArea(3,20)]
	private string eventComment;

	/// <summary>
	/// Optional bool, if set to true Event will output a Debug.Log when the event is raised.
	/// </summary>
	[SerializeField]
	private bool printToLogOnRaise;

	/// <summary>
	/// Inspector Button to test event functionality. Raises event on click.
	/// </summary>
	[InspectorButton("OnButtonClicked")]
	public bool testButton;

	// Possible Paremeters for data to pass with event. Unneeded if using event just for triggering other functionality.

	[SerializeField]
	protected GameObject gameObject = null;
	[SerializeField]
	protected ScriptableObject scriptableObjects = null;
	[SerializeField]
	protected int intValue = 0;
	[SerializeField]
	protected bool boolValue = false;
	[SerializeField]
	protected float floatValue = 0f;

	/// <summary>
	/// List of all the listeners to this event.
	/// </summary>
	private List<EventListener> listeners = new List<EventListener>();

	// Parameter Accessors
	public GameObject GameObject { get => gameObject; }
	public ScriptableObject ScriptableObject { get => scriptableObjects; }
	public int IntValue { get => intValue; }
	public bool BoolValue { get => boolValue; }
	public float FloatValue { get => floatValue; }

	public void RegisterListener(EventListener listener)
	{ listeners.Add(listener); }

	public void UnregisterListener(EventListener listener)
	{ listeners.Remove(listener); }

	/// <summary>
	/// Raises event in every registered listener.
	/// </summary>
	public void Raise()
	{
		if (printToLogOnRaise)
		{
			Debug.Log($"{this.name} raised");
		}
		// In reverse in case a listener's response is to remove itself from the list.
		for (int i = listeners.Count - 1; i >= 0; i--) 
			listeners[i].OnEventRaised(this);
	}

/// <summary>
/// Debug button to raise event from inspector.
/// </summary>
	private void OnButtonClicked()
	{
		Raise();
	}
}

