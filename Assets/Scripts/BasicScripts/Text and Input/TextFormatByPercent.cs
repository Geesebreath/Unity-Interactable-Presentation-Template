using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// // Sets a Text component that has a numerical value to a percentage between a maximum and minimum value.
/// Intended for use with a Slider UI object to display a linear value when the slider value is changed.
/// </summary>
[RequireComponent(typeof(Text))]
public class TextFormatByPercent: MonoBehaviour, CommonInterface
{
    [SerializeField]
    private string prefixString, suffixString;
	[SerializeField]
	private float minValue, maxValue;
	
	// Below mostly for use case of a slider needing to be between certain values before the value is going to be changed.
	// Initially made for the Displacement section of the Volumetrics DLO. Value must be between 0 and 1.
	[Tooltip("Passed values must be between 0 and 1.") ]
	[SerializeField]
	private float minPassedPercent = 0f, maxPassedPercent = 1f;
	[SerializeField]
	private string percentAccuracy = "0.00";

	private float relativePercentDistance;

	private Text text;

	private void Awake()
	{
		relativePercentDistance = maxPassedPercent - minPassedPercent;
		text = GetComponent<Text>();
		text.text = prefixString + minValue.ToString() + suffixString;
	}

	/// <summary>
	/// Pass in a percentage value (0f to 1f)
	/// </summary>
	/// <param name="percent"></param>
	public void SetValuePercentage(float passedPercent)
	{

		if(passedPercent < minPassedPercent)
		{
			SetAbsoluteValue(minValue);
		}
		else if(passedPercent > maxPassedPercent)
		{
			SetAbsoluteValue(maxValue);
		}
		else
		{
			float relativePercent = ((passedPercent - minPassedPercent) / relativePercentDistance);
			float endValue = ((maxValue - minValue) * relativePercent) + minValue;
			text.text = prefixString + endValue.ToString(percentAccuracy) + suffixString;
		}
	}

	public void SetAbsoluteValue(float value)
	{
		text.text = prefixString + value.ToString(percentAccuracy) + suffixString;
	}

	public void Reset()
	{
		if(text == null)
		{
			Awake();
		}
		SetValuePercentage(minPassedPercent);
	}
}
