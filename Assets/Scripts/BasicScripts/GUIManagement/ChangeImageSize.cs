using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Sets a UI image to a specific Height
/// </summary>
public class ChangeImageSize : MonoBehaviour
{
    [SerializeField]
    private Graphic image;
    private RectTransform rect;
    public float maximumHeight, minimumHeight, defaultHeight, maximumWidth, minimumWidth, defaultWidth;

    void Awake()
    {
        if(image == null)
		{
            image = GetComponent<Graphic>();
            rect = GetComponent<RectTransform>();
        }
        else
		{
            rect = image.transform.GetComponent<RectTransform>();
        }

        if(defaultHeight == 0 || defaultWidth ==0)
		{
            defaultHeight = rect.sizeDelta.y;
            defaultWidth = rect.sizeDelta.x;
		}

        SetImageHeight(defaultHeight);
        SetImageWidth(defaultWidth);
    }

    /// <summary>
    /// Directly set the pixel height of the image.
    /// </summary>
    /// <param name="value"></param>
    public void SetImageHeight(float value)
	{
        Debug.Log(value);
        float newHeight = value;
        if(newHeight>maximumHeight)
		{
            newHeight = maximumHeight;
		}
        else if(value<minimumHeight)
		{
            newHeight = minimumHeight;
		}
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, newHeight);
    }

    /// <summary>
    /// Adjust the pixel height of the image by an amount of pixels.
    /// </summary>
    /// <param name="value"></param>
    public bool ChangeImageHeight(float value)
	{
        if(rect.sizeDelta.y+value <=maximumHeight && rect.sizeDelta.y+value>=minimumHeight)
		{
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y + value);
            return true;
        }
        return false;
	}

    public void SetToMaximumHeight()
    {
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, maximumHeight);
    }

    public void SetToMinimumHeight()
    {
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, minimumHeight);
    }

    /// <summary>
    /// Directly set the pixel width of the image.
    /// </summary>
    /// <param name="value"></param>
    public void SetImageWidth(float value)
    {
        Debug.Log(value);
        float newWidth = value;
        if (newWidth > maximumWidth)
        {
            newWidth = maximumWidth;
        }
        else if (value < minimumWidth)
        {
            newWidth = minimumWidth;
        }
        rect.sizeDelta = new Vector2(newWidth, rect.sizeDelta.y);
    }

   
    /// <summary>
    /// Adjust the pixel width of the image by an amount of pixels. Returns true if adjustment is within maximum/minimum range for image.
    /// </summary>
    /// <param name="value"></param>
    public bool ChangeImageWidth(float value)
    {
        if (rect.sizeDelta.x + value <= maximumWidth && rect.sizeDelta.x + value >= minimumWidth)
        {
            rect.sizeDelta = new Vector2(rect.sizeDelta.x + value, rect.sizeDelta.y);
            return true;
        }
        return false;
    }

    public void SetToMaximumWidth()
    {
        rect.sizeDelta = new Vector2(maximumWidth, rect.sizeDelta.y);
    }

    public void SetToMinimumWidth()
    {
        rect.sizeDelta = new Vector2(minimumWidth, rect.sizeDelta.y);
    }

    /// <summary>
	/// Put image back to default size
	/// </summary>
    public void ResetImage()
	{
        SetImageHeight(defaultHeight);
        SetImageWidth(defaultWidth);
	}
}
