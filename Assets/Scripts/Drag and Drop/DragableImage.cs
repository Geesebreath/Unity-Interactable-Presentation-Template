using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
/// <summary>
/// Simple base class to allow dragging an object. Optional event that fires on starting and dropping drag.
/// </summary>
[System.Serializable]
public class DragableImage : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IEndDragHandler, IBeginDragHandler, IDragHandler, IDropHandler, CommonInterface 
{
	public bool allowDragging = true;
	[SerializeField]
	private protected Canvas canvas;
	// Starting Position is set in inspector.
	//public  RectTransform startingPosition;
	
	[SerializeField]
	private protected Image image;
	[SerializeField]
	private bool limitToDragArea = false;
	[SerializeField]
	private DragArea dragArea;

	protected RectTransform rectTransform;
	public UnityEvent pickedUpEvent, droppedEvent;

	private Vector3 startingPosition;

	protected bool isBeingDragged;
	protected virtual void Awake()
	{
		isBeingDragged = false;
		rectTransform = GetComponent<RectTransform>();
		startingPosition = rectTransform.anchoredPosition;
		if(limitToDragArea)
		{
			if (dragArea == null)
			{
				dragArea = GetComponentInParent<DragArea>();
				if(dragArea == null)
				{
					Debug.LogError("Draggable Object is marked as to be contianed in a Drag Area but cannot find one");
					limitToDragArea = false;
				}
			}
		}

		if(canvas == null)
		{
			canvas = FindObjectOfType<Canvas>();
		}
		if(image == null)
		{
			image = GetComponent<Image>();
		}
	}

	public virtual void DragHandler(BaseEventData data)
	{
		isBeingDragged = true;
		if (!allowDragging)
		{
			// Do nothing
		}
		else
		{ // Do drag things
			PointerEventData pointerData = (PointerEventData)data;
			if (limitToDragArea)
			{
				if (dragArea.isOver())
				{
					Debug.Log("Over Drag Area");
					image.raycastTarget = false;
					Vector2 position = dragArea.GetMousePositionInsideImage(GetPointerPosition(pointerData));
					//Set screen position to position pointer is inside limiting drag area rectangle.
					RectTransformUtility.ScreenPointToLocalPointInRectangle(
					(RectTransform)canvas.transform,
					pointerData.position,
					null,
					out position);
					SetToDragPosition(position);

				}
				else
				{
					Debug.Log("Outside Drag Area");
					EndDragging(data);
				}
			}
			else
			{
				//Turn off raycasting to dragged image.
				image.raycastTarget = false;

				// Find RectTransform position of mouse over canvas
				Vector2 position = GetPointerPosition(pointerData);
				RectTransformUtility.ScreenPointToLocalPointInRectangle(
					(RectTransform)canvas.transform,
					pointerData.position,
					null,
					out position);

				SetToDragPosition(position);
			}
		}
	}

	public virtual void SetToDragPosition(Vector2 position)
	{
		transform.position = canvas.transform.TransformPoint(position);
	}

	public virtual void SetPosition(Vector2 position)
	{
		
		transform.position = position;
	}

	public virtual void EndDragging(BaseEventData data)
	{
		isBeingDragged = false;
		if (allowDragging)
		{
			// turn raycasting on image back ON
			image.raycastTarget = true;
			if (droppedEvent != null)
			{
				droppedEvent.Invoke();
			}
		}
	}

	public virtual void ResetPosition()
	{
		// Occassionally this will be called before Awake if in a scene section that is being Reset the first time.
		// Awake fixes these. If both are null then Awake has not been run. Fixes that.
		if (startingPosition == null || rectTransform == null)
		{
			Awake();
		}
		rectTransform.anchoredPosition = startingPosition;
	}

	protected virtual Vector2 GetPointerPosition(PointerEventData pointerData)
	{
		Vector2 position;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(
			(RectTransform)canvas.transform,
			pointerData.position,
			canvas.worldCamera,
			out position);

		return position;
	}

	public virtual void OnDrag(PointerEventData eventData)
	{
		//Debug.Log("Drag");
		if (allowDragging)
		{
			DragHandler(eventData);
		}
	}

	public virtual void OnPointerDown(PointerEventData eventData)
	{
		//Debug.Log("Pointer Down");
		DragHandler(eventData);
		if (allowDragging)
		{
			pickedUpEvent.Invoke();
			OnBeginDrag(eventData);
		}

	}
	public virtual void OnPointerUp(PointerEventData eventData)
	{
		if (isBeingDragged)
		{
			//Debug.Log("Pointer Up");
			EndDragging(eventData);
			image.raycastTarget = true;
		}
	}

	public virtual void OnDrop(PointerEventData eventData)
	{
		//	Debug.Log("Drop");
		//	EndDragging(eventData);
	}

	public virtual void OnEndDrag(PointerEventData eventData)
	{
		//Debug.Log("End Drag");
		if (allowDragging)
		{
			EndDragging(eventData);
		}
	}

	public virtual void OnBeginDrag(PointerEventData eventData)
	{
		Debug.Log("Begin Drag");
		if(allowDragging)
		{
			pickedUpEvent.Invoke();
		}
	}

	public virtual void Reset()
	{
		Debug.Log("Reset " + transform.name);
		allowDragging = true;
		ResetPosition();
	}
}
