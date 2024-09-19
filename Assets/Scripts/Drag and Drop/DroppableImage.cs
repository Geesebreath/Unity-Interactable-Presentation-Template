using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Xml.Linq;
/// <summary>
/// Componenet that allows UI images to be dragged. Used with DropZone for drag and drop interfaces.
/// </summary>
[System.Serializable]
public class DroppableImage : DragableImage
{
	// Scriptable object for data stored inside object (if applicable)
	[SerializeField]
	protected SODroppableImageData soData;

	public DropZone[] dropZones;
	//[SerializeField]
	//private Sprite[] crossSectionSprites, carOnRoadSprites;
	//[SerializeField]
	//private Sprite emptyCrossSectionSprite, emptyCarOnRoadSprite;
	// Which drop zone the image is currently over, or 0 is the 'home' position
	protected int currentDropZoneIndex;

	// Used for if you would like to rotate the image when over drop zone or on drop.
	//private float leftSideAngle, centerAngle, rightSideAngle, defaultAngle;
	//private RotateRectToValue rotateRect;

	public UnityEvent droppedOnZoneEvent;

	// variables for checking if cursor is over drop zone
	private protected GraphicRaycaster raycaster;
	private protected PointerEventData eventData;
	private protected EventSystem eventSystem;
	protected override void Awake()
	{
		base.Awake();
		currentDropZoneIndex = -1;

		Canvas mainCanvas = GetComponentInParent<Canvas>().rootCanvas;
		raycaster = mainCanvas.GetComponent<GraphicRaycaster>();
		eventSystem = UnityEngine.EventSystems.EventSystem.current;
	}

	public override void DragHandler(BaseEventData data)
	{
		base.DragHandler(data);
	}

	public virtual void SetDZIndex(int index)
	{
		if (isBeingDragged)
		{
			SetAbsoluteDZIndex(index);
		}
	}

	protected virtual void SetAbsoluteDZIndex(int index)
	{
		currentDropZoneIndex = index;
	}

	public virtual void LeftDropZone(int index)
	{
		// compare which section you left to current index (to proctect against overlapping drop zones breaking things)
		// Only reset value if you left the expected drop zone, and not just entered another one
		if(index == currentDropZoneIndex)
		{
			OutsideDZBounds();
		}
	}

	protected virtual void OutsideDZBounds()
	{
		currentDropZoneIndex = -1;
	}

	public override void EndDragging(BaseEventData data)
	{
		isBeingDragged = false;
		if(allowDragging)
		{
			image.raycastTarget = true;
			SetDZPosition();
			if (currentDropZoneIndex >= 0)
			{
				// Set Image of drop zone to be what is inteded.
				//SetPanelImages(crossSectionSprites[currentDropZoneIndex],carOnRoadSprites[currentDropZoneIndex]);
				droppedEvent.Invoke();
			}
		}
	}

	public void SetAnchoredPosition(Vector2 position)
	{
		rectTransform.anchoredPosition = position;
	}

	/// <summary>
	/// Set position to drop zone location if applicable, else reset position.
	/// </summary>
	protected virtual void SetDZPosition()
	{
		// Check if over  a drop zone (GraphicsRaycast.Raycast) and then execute positioning if yes.
		eventData = new PointerEventData(eventSystem);
		eventData.position = Input.mousePosition;
		List<RaycastResult> results = new List<RaycastResult>();
		raycaster.Raycast(eventData, results);

		// go through all results and check if over a dropzone
		for (int i = 0; i < results.Count; i++)
		{
			GameObject testingObject = results[i].gameObject;
			//Debug.Log(testingObject.name);
			for (int x = 0; x < dropZones.Length; x++)
			{
				if (dropZones[x].gameObject != null && dropZones[x].gameObject == testingObject)
				{
					currentDropZoneIndex = x;
					x = dropZones.Length;
					i = results.Count;
				}
			}
		}

		if (currentDropZoneIndex >= 0 && currentDropZoneIndex < dropZones.Length)
		{
			rectTransform.anchoredPosition = dropZones[currentDropZoneIndex].rectTransform.anchoredPosition;
			dropZones[currentDropZoneIndex].DroppedOnZone();
		}
		else
		{// Reset to starting location
			ResetPosition();
		}
	}
	public override void OnBeginDrag(PointerEventData eventData)
	{
		if(allowDragging)
		{
			isBeingDragged = true;
			//Debug.Log("Picked up Image");
			//Debug.Log(currentDropZoneIndex);
			if (currentDropZoneIndex != -1)
			{
				dropZones[currentDropZoneIndex].ObjectPickedUpFromZone(this);
			}
		}
		
	}

	/// <summary>
	/// Get stored data object (if available).
	/// </summary>
	/// <returns></returns>
	public virtual SODroppableImageData GetDataSO()
	{
		return soData;
	}

	/// <summary>
	/// Enable or disable dragging of this object with state argument.
	/// </summary>
	/// <param name="state"></param>
	public void SetDraggable(bool state)
	{
		allowDragging = state;
	}

	public override void Reset()
	{
		base.Reset();
	}
}
