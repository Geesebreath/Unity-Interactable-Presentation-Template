using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// For when multiple objects can be placed in a single zone. Requires 
/// rect transform objects for locations to place dropped images in the drop zone.
/// SO events are optional.
/// </summary>
public class MultiObjectDropZone : DropZone
{
	// Array of positions that can store objects inside drop zone.
	public RectTransform[] dropPositions;
	// Bool array for if an object is in that position. Set as seperate array rather than dictionary
	// to allow editing positions inside editor.
	private DroppableImage[] objectDroppedHere;
	// TODO: Set SO events as optional.
	[SerializeField]
	private SODropZoneObjects dropZoneObjectsSO;
	// SO event to send out what object was dropped on it.
	[SerializeField]
	private EditableGameEvent droppedImageSOEvent;

	public override void Awake()
	{
		base.Awake();
		objectDroppedHere = new DroppableImage[dropPositions.Length];
		for (int i = 0; i < objectDroppedHere.Length; i++)
		{
			objectDroppedHere[i] = null;
		}
	}

	/// <summary>
	/// Place dropped image in Drop Zone
	/// </summary>
	/// <param name="droppedImage"></param>
	public override void DroppedOnZone(DroppableImage droppedImage)
	{
		bool freeLocationFound = false;
		// Execute dropped Events
		base.DroppedOnZone();

		// Check locations in dropPositions and place in first empty position.
		for (int i = 0; i < objectDroppedHere.Length && !freeLocationFound; i++)
		{
			// null = empty, place object in position.
			if (objectDroppedHere[i] == null)
			{
				// Event management for checking if image in correct DZ
				droppedImageSOEvent.ChangeGameObject(droppedImage.gameObject);
				droppedImageSOEvent.ChangeInt(dzIndex);
				droppedImageSOEvent.Raise();

				freeLocationFound = true;
				objectDroppedHere[i] = droppedImage;
				droppedImage.SetPosition(dropPositions[i].position);
				i = objectDroppedHere.Length + 1;
				// Add object to scriptable object
				if (droppedImage.GetDataSO() != null)
				{
					dropZoneObjectsSO.AddData(droppedImage.GetDataSO());
				}
			}
		}
		// If no free location in drop zone, raise error.
		if (!freeLocationFound)
		{
			Debug.LogError("No open slot to place dropped image.");
			droppedImage.ResetPosition();
			droppedImage.SetDZIndex(-1);
		}
	}

	/// <summary>
	/// Remove object from inside drop zone.
	/// </summary>
	/// <param name="pickedUpImage"></param>
	public override void ObjectPickedUpFromZone(DroppableImage pickedUpImage)
	{
		for (int i = 0; i < objectDroppedHere.Length; i++)
		{
			if (objectDroppedHere[i] == pickedUpImage)
			{
				// Event management for checking if image in correct DZ
				droppedImageSOEvent.ChangeGameObject(pickedUpImage.gameObject);
				// -1 DZ for being in no DZ
				droppedImageSOEvent.ChangeInt(-1);
				droppedImageSOEvent.Raise();

				objectDroppedHere[i] = null;
				if (pickedUpImage.GetDataSO() != null)
				{
					dropZoneObjectsSO.RemoveData(pickedUpImage.GetDataSO());
				}
				imagePickedUpEvent.Invoke();
			}
		}
	}

	public void Reset()
	{
		dropZoneObjectsSO.ClearList();
		Awake();
	}
}


/// <summary>
/// Way for action to send a game object, used to send the dropped image to the receiving method.
/// </summary>
public class ObjectUnityEvent : UnityEvent<GameObject>
{
	public GameObject gameObject;
}
