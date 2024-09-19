using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Scrolls and loops an image in a set direction while 'enableScroll' is true.
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class ImageScroller : MonoBehaviour
{
	// Moves image and loops.
	RectTransform rect;
	[Tooltip("When object reaches the End position is moved to the Start position.")]
	public Vector2 startPosition, endPosition;
	public float speed;

	public bool enableScroll = true;
	private void Start()
	{
		rect = GetComponent < RectTransform >();
	}

	private void Update()
	{
		if(enableScroll)
		{
			Vector2 endDirection = (endPosition - rect.anchoredPosition).normalized;
			Vector2 startDirection = (startPosition - rect.anchoredPosition).normalized;

			rect.anchoredPosition += (endDirection * (speed * Time.deltaTime));
			endDirection = (endPosition - rect.anchoredPosition).normalized;

			// if the endposition is in the same direction as the start position, the image has passed the end
			if (endDirection == startDirection || (rect.anchoredPosition.x == endPosition.x && rect.anchoredPosition.y == endPosition.y))
			{
				rect.anchoredPosition = startPosition;
			}
		}
	}

	/// <summary>
	/// Activates/deactivates scrolling.
	/// </summary>
	/// <param name="value"></param>
	public void SetScroll(bool value)
	{
		enableScroll = value;
	}
}
