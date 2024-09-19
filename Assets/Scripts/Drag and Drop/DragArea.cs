using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// This component limits draggablity of objects inside it to the bounds of the image.
/// </summary>
[RequireComponent(typeof(Image))]
[System.Serializable]
public class DragArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool isOverImage;
	private Image image;

	void Awake()
	{
		image = GetComponent<Image>();
	}

	public bool isOver()
	{
		return isOverImage;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		isOverImage = true;
		
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		isOverImage = false;
		
	}

	public Vector2 GetMousePositionInsideImage(Vector2 mousePos)
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
		if (imagePos.x >= image.rectTransform.rect.width)
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
		//	posInImage.y >= 0 && posInImage.y < imageHeight)
		//{
		//	// The mouse click is inside the image, so calculate a normalised value for the click.
		//	normalisedPos.x = posInImage.x / imageWidth;
		//	normalisedPos.y = posInImage.y / imageHeight;
		//}
		Debug.Log("normalisedPos= " + normalisedPos);
		return normalisedPos;
	}
}
