using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
/// <summary>
/// Triggers a UnityEvent on Mouse events (enter, exit, over, down, up, drag).
public class MouseEvents : MonoBehaviour, CommonInterface
{
    public UnityEvent mouseEnterActions, mouseOverActions, mouseExitActions, mouseDownActions, mouseUpActions, mouseDragActions;

	public void Reset()
	{
		if (mouseExitActions != null)
		{
			mouseExitActions.Invoke();
		}
	}

	public void OnMouseEnter()
	{
		if (mouseEnterActions != null)
		{
			mouseEnterActions.Invoke();
		}
	}

	public void OnMouseOver()
	{
		if (mouseOverActions != null)
		{
			mouseOverActions.Invoke();
		}
	}

	public void OnMouseExit()
	{
		if (mouseExitActions != null)
		{
			mouseExitActions.Invoke();
		}
	}


	public void OnMouseDown()
	{
		if (mouseDownActions != null)
		{
			mouseDownActions.Invoke();
		}
	}

	public void OnMouseUp()
	{
		if (mouseUpActions != null)
		{
			mouseUpActions.Invoke();
		}
	}

	public void OnMouseDrag()
	{
		if (mouseDragActions != null)
		{
			mouseDragActions.Invoke();
		}
	}

	public void DebugAction()
	{
		Debug.Log(gameObject.name + " Action Activated!");
	}
}
