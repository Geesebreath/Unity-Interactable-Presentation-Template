using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Set events for different Unity callbacks. Select a bool and the action pops up.
/// </summary>
public class ActionOnCallback : MonoBehaviour, CommonInterface
{
	// Any changes to the properties in this script should be reflected in EActionOnCallback Custum Editor Script.
	[SerializeField,HideInInspector]
	private bool awake;
	[SerializeField,HideInInspector]
	private bool onEnable;
	[SerializeField,HideInInspector]
	private bool start;
	[SerializeField, HideInInspector]
	private bool reset;
	[SerializeField, HideInInspector]
	private bool onDisable;
	[SerializeField, HideInInspector]
	private bool onDestroy;

	[Tooltip("The action to be invoked when one of the following callback functions occur.")]
	[SerializeField,HideInInspector]
	public UnityEvent awakeAction, onEnableAction, onDisableAction, resetAction, startAction, onDestroyAction;

	public void Awake()
	{
		if (awake)
			awakeAction.Invoke();
	}

	public void OnEnable()
	{
		if (onEnable)
			onEnableAction.Invoke();
	}

	public void Start()
	{
		if (start)
			startAction.Invoke();
	}


	public void OnDisable()
	{
		if (onDisable)
			onDisableAction.Invoke();
	}

	public void Reset()
	{
		if (reset)
			resetAction.Invoke();
	}

	public void OnDestroy()
	{
		if (onDestroy)
			onDestroyAction.Invoke();
	}


}


