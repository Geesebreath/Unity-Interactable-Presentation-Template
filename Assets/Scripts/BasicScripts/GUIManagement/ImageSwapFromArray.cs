using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Stores an array of sprites and swaps image to sprite when passed an index.
/// </summary>
public class ImageSwapFromArray : MonoBehaviour, CommonInterface
{
    public Image image;
	public Slider optionalSlider;
	public Sprite[] sprites;
    
    [SerializeField]
    private int startingImageNumber = 0, currentIndex;

    void Awake()
    {
        if(image ==null)
		{
            image = GetComponent<Image>();
            if(image == null)
			{
                Debug.LogError("ImageSwapFromArray not given an image and not on an object with an Image component.");
			}
		}
        currentIndex = startingImageNumber;
        ChangeImage(startingImageNumber);
    }

    /// <summary>
	/// Changes image to sprite at passed index.
	/// </summary>
	/// <param name="value"></param>
    public void ChangeImage(int value)
	{
        if(image == null)
		{
            image = GetComponent<Image>();
		}
        if(value>=0 && value <= sprites.Length-1)
		{
            image.sprite = sprites[value];
            currentIndex = value;
		}
        else
		{
            Debug.LogError($"Sprite number {value} not found in Sprites array.");
		}
	}

    /// <summary>
    /// Only use this if calling from a UI slider, limited to Whole Numbers.
    /// </summary>
    /// <param name="sliderValue"></param>
    public void ChangeImageFromSlider(float sliderValue)
	{
        int imageValue = (int)sliderValue;
        if(imageValue< 0 || imageValue >= sprites.Length)
		{
            Debug.LogError($"{transform.parent.gameObject.name} child {gameObject.name} UIslider is calling ImageChange array with a value out of range. - {sliderValue} value vs {sprites.Length} array length");
		}
        else
		{
            ChangeImage(imageValue);
		}
	}

    /// <summary>
	/// Moves to next image in array if true, previous if false. Loops to other end of array if above or below max/min ranges.
	/// </summary>
	/// <param name="next"></param>
    public void ChangeToNextImage(bool next = true)
	{
        if(next)
		{
            currentIndex++;
		}
        else
		{
            currentIndex--;
		}

        if(currentIndex<0)
		{
            currentIndex = sprites.Length - 1;
		}

        if (currentIndex > sprites.Length-1)
		{
            currentIndex = 0;
		}

        ChangeImage(currentIndex);
	}

	public void Reset()
	{
        ChangeImage(startingImageNumber);
        if(optionalSlider != null)
            optionalSlider.value = startingImageNumber;
	}

}
