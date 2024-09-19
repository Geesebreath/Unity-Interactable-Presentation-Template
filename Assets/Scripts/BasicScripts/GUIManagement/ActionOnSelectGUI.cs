using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ActionOnSelectGUI : MonoBehaviour, ISelectHandler
{/// <summary>
/// Triggers a Unity action when the GUI element is selected (Usually by clicking)
/// </summary>
	public UnityEvent selectAction;

	public void OnSelect(BaseEventData eventData)
	{
		Debug.Log("Selected");
		selectAction.Invoke();
	}
}
