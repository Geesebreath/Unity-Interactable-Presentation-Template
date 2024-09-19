using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// Inteded for an image overlay when clicking on another image. This compoent is placed on a transparent
/// image that marks the boundaries of the area that you can drag inside of.
/// Only valid while mouse is over  this image. Can be locked to only be on X or Y axis.
/// </summary>
//[RequireComponent(typeof(EventTrigger))]
[System.Serializable]
public class ImageAppearOnClick : DragableImage, IPointerEnterHandler, IPointerExitHandler, CommonInterface
{//TODO: bring in IPointerDragHandler (IDragHandler?i) to see if can remove the dependency on an Event Trigger Component.
	// Then may also need to create a way to optionally fire a Unity Event when a mouse event occurs on this object (like if needing
	// another object to do something on this object's drag).
	private bool isOverImage;
	
	[SerializeField]
	private Image primaryImage;
	[SerializeField]
	private ImageAppearOnClick secondaryImage;
	[SerializeField]
	private bool lockXAxis, lockYAxis;
	[SerializeField]
	private float startingPositionX, startingPositionY;
	
	protected override void Awake()
	{
		base.Awake();
		//startingPositionX = primaryImage.transform.position.x;
		//startingPositionY = primaryImage.transform.position.y;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		isOverImage = true;
		//Debug.Log("enter");
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		isOverImage = false;
		//Debug.Log("Exit");
	}

	public override void DragHandler(BaseEventData data)
	{
		Debug.Log("Is Over Image, Drag Handler active");
		if (isOverImage)
		{
			Debug.Log("isOver");
			primaryImage.enabled = true;
			//primaryImage.raycastTarget = false;
			PointerEventData pointerData = (PointerEventData)data;
			
			// Find RectTransform position of mouse over canvas
			Vector2 position = GetPointerPosition(pointerData);
			//Gets 
			RectTransformUtility.ScreenPointToLocalPointInRectangle(
				(RectTransform)canvas.transform,
				pointerData.position,
				null,
				out position);

			SetPosition(position);
			Vector2 positionPercentage = WidthAndHeightPercentage(image.rectTransform, GetMousePositionInsideImage(image, pointerData.position));
			secondaryImage.SetPositionByPercentage(positionPercentage);
		}
		else
		{
			Debug.Log("NotOver");
			secondaryImage.EndDragging(data);
			EndDragging(data);
		}
	}

	public override void SetPosition(Vector2 position)
	{
		Debug.Log("Setting percentage position:  "+ position);
		primaryImage.transform.position = canvas.transform.TransformPoint(position);
		if (lockXAxis)
		{
			primaryImage.rectTransform.anchoredPosition = new Vector2(startingPositionX, primaryImage.rectTransform.anchoredPosition.y);
		}

		if(lockYAxis)
		{
			primaryImage.rectTransform.anchoredPosition = new Vector2(primaryImage.rectTransform.anchoredPosition.x, startingPositionY);
		}
	}

	/// <summary>
	/// Given percentage of x an y distance from bottom left of image, act as if if it was clicked there.
	/// </summary>
	/// <param name="percentage"></param>
	public void SetPositionByPercentage(Vector2 percentage)
	{
		primaryImage.enabled = true;
		primaryImage.rectTransform.anchoredPosition =
			//This shouldn't be primaryImage, needs to be the boundary image height and width.
				new Vector2(image.rectTransform.rect.width * percentage.x, 
							image.rectTransform.rect.height * percentage.y);
		if (lockXAxis)
		{
			primaryImage.rectTransform.anchoredPosition = new Vector2(startingPositionX, primaryImage.rectTransform.anchoredPosition.y);
		}
		if (lockYAxis)
		{
			primaryImage.rectTransform.anchoredPosition = new Vector2(primaryImage.rectTransform.anchoredPosition.x, startingPositionY);
		}
	}

	public override void EndDragging(BaseEventData data)
	{
		primaryImage.enabled = false;
		base.EndDragging(data);
	}

	/// <summary>
	/// Get Mouse position inside of a passed image. Will return a Vector2 of point inside image, or closest edge.
	/// </summary>
	/// <param name="img"></param>
	/// <param name="mousePos"></param>
	/// <returns></returns>
	protected Vector2 GetMousePositionInsideImage(Image img, Vector2 mousePos)
	{
		Vector2 imagePos;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(image.gameObject.GetComponent<RectTransform>(), mousePos, null, out imagePos);
		//Vector2 imagePos = img.gameObject.GetComponent<RectTransform>().anchoredPosition;

		//(0,0) posInImage is center of image.`	

		//float imageWidth = img.rectTransform.rect.width, imageHeight = img.rectTransform.rect.height;
		Vector2 normalisedPos = imagePos;
		if (imagePos.x <= 0)
		{
			normalisedPos.x = 0;
		}
		if(imagePos.x>=image.rectTransform.rect.width)
		{
			normalisedPos.x = image.rectTransform.rect.width;
		}

		if (imagePos.y <= 0)
		{
			normalisedPos.y = 0;
		}
		if (imagePos.y >= image.rectTransform.rect.height)
		{
			normalisedPos.y = image.rectTransform.rect.height;
		}
		return normalisedPos;
	}

	/// <summary>
	/// Calculates percentage widtho/height f distance from bottom left of a rectTransform a passed postition is.
	/// </summary>
	/// <param name="img"></param>
	/// <param name="position"></param>
	/// <returns></returns>
	protected Vector2 WidthAndHeightPercentage(RectTransform rectTransform, Vector2 position)
	{
		Vector2 percentage = Vector2.zero;
		percentage.x = (position.x / rectTransform.rect.width);
		percentage.y = (position.y / rectTransform.rect.height);
		return percentage;
	}
	protected void SetSecondaryImagePosition()
	{
		ImageAppearOnClick secImageScript = secondaryImage.GetComponent<ImageAppearOnClick>();
	}

	public override void Reset()
	{
		primaryImage.enabled = false;
	}
}
