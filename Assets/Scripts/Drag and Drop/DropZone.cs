using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

/// <summary>
/// Component for object that the user is intended to drag and drop another element onto. May contain
/// it's own data (ex. for populating a field when it receives an object, optional)
/// </summary>
public class DropZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform rectTransform;
	[SerializeField]
	private EditableGameEvent eventPointerEnter, eventPointerExit;
	[SerializeField]
	protected int dzIndex;
	//Optional
	public SODataContainer data;
	// Event to be used if DataContainer is also being used.
	private EditableGameEvent droppedEvent;
	public UnityEvent imageDroppedOnZoneEvent, imagePickedUpEvent;
	public virtual void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
	}

	public int GetIndex()
	{
		return dzIndex;
	}

	public void SetZoneData(SODataContainer zoneData)
	{
		data = zoneData;
	}

	public virtual void CursorEnter()
	{
		eventPointerEnter.ChangeInt(dzIndex);
		eventPointerEnter.Raise();
		
	}

	public virtual void CursorExit()
	{
		eventPointerExit.ChangeInt(dzIndex);
		eventPointerExit.Raise();
		
	}

	public virtual void OnPointerEnter(PointerEventData eventData)
	{
		CursorEnter();
	}

	public virtual void OnPointerExit(PointerEventData eventData)
	{
		CursorExit();
	}

	public virtual void DroppedOnZone()
	{
		if(imageDroppedOnZoneEvent!=null)
		{
			imageDroppedOnZoneEvent.Invoke();

		}
	}

	public virtual void DroppedOnZone(GameObject droppedObject)
	{
		// Overload for passing in what has been dropped on the zone - to be used in more complex interactions.
		DroppedOnZone();
	}

	public virtual void DroppedOnZone(DroppableImage droppedImage)
	{
		// Overload for passing in what has been dropped on the zone - to be used in more complex interactions.
		DroppedOnZone();
	}

	public virtual void ObjectPickedUpFromZone()
	{
		if (imagePickedUpEvent != null)
		{
			imagePickedUpEvent.Invoke();
		}
	}

	public virtual void ObjectPickedUpFromZone(DroppableImage pickedUpImage)
	{
		// If necessary to do specific action for a specific object.
		ObjectPickedUpFromZone();

	}

	/// <summary>
	/// Compare 2 Dropzones by index, less than 0 is first is less than second, 0 is equal, greater than 1 first is greater than second.
	/// </summary>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <returns></returns>
	public static int CompareByIndex(DropZone x, DropZone y)
	{
		return x.GetIndex().CompareTo(y.GetIndex());
	}

	/// <summary>
	/// Raises the "dropped on zone event" with the current zone's data scriptable object.
	/// </summary>
	public void SetEventDataAndRaise()
	{
		droppedEvent.ChangeSO(data);
		droppedEvent.Raise();
	}

}