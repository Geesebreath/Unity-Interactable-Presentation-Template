using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DroppableImageWithCorrectPlacement : DroppableImage
{
	public UnityEvent correctDZEvent, incorrectDZEvent;

	protected override void SetDZPosition()
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
				if (dropZones[x].gameObject == testingObject)
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
			CheckCorrectDZ();
		}
		else
		{// Reset to starting location
			ResetPosition();
		}

	}

	public void CheckCorrectDZ()
	{
		Debug.Log("Checking...");
		if (currentDropZoneIndex >= 0 && currentDropZoneIndex == soData.GetDZ())
		{
			Debug.Log("Correct DZ!");
			correctDZEvent.Invoke();
		}
		else
		{
			Debug.Log("Incorrect DZ :(");
			incorrectDZEvent.Invoke();
		}
	}
}
