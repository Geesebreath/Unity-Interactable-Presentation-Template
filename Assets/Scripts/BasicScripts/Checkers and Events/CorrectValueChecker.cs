using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Checks a float value and executes event if matches exactly or not. Displaes confirmation/error text if value is not inside optional range.
/// </summary>
public class CorrectValueChecker : MonoBehaviour, CommonInterface
{
    public float correctValue;
	[SerializeField]
	private bool useConfirmationText, allowCorrectRange;
	[SerializeField]
	private Text confirmationText;
	[SerializeField]
	private float accuracyTolerance = 0;

	//Confirmation Text variables if required:
	public string exactString, correctString, incorrectString;
	public Color errorTextColor, regularTextColor;

	//Action for when values align correctly
	public UnityEvent correctEvent;
	// Action for when in range is allowed and is within tolerance
	public UnityEvent inRangeEvent;
    //Action for when value is not correct
    public UnityEvent incorrectEvent;
	//Action for when resetting (if different from incorrectEvent)
	public UnityEvent resetEvent;

	void Awake() 
	{ 
		if(useConfirmationText)
		{
			if (exactString == string.Empty)
			{
				exactString = $"Exactly!";
			}

			if (correctString == string.Empty)
			{
				correctString = $"Correct (+- {accuracyTolerance}). Actual value is {correctValue}.";
			}

			if (incorrectString == string.Empty)
			{
				incorrectString = $"Incorrect. Actual value is {correctValue}.";
			}
		}
	}
	
	public void Reset()
	{
		if(resetEvent == null)
		{
			incorrectEvent.Invoke();
		}
		else
		{
			resetEvent.Invoke();
		}
	}
	
	/// <summary>
	/// Get what the expected value is for this component.
	/// </summary>
	/// <returns></returns>
	public float GetExpectedValue()
	{
		return correctValue;
	}

	/// <summary>
	/// Checks passed value against stored 'correct' value and invokes correct or incorrect value.
	/// </summary>
	/// <param name="value"></param>
	public void CheckValue(string value)
	{
		float floatValue = float.Parse(value);
		CheckValue(floatValue);
	}

	/// <summary>
	/// Checks passed value against stored 'correct' value and invokes correct or incorrect value.
	/// </summary>
	/// <param name="value"></param>
	public void CheckValue(float value)
	{
		//Exact
		if (value == correctValue)
		{
			correctEvent.Invoke();
			if(useConfirmationText)
			{
				confirmationText.color = regularTextColor;
				confirmationText.text = exactString;
			}
		}
		// Check Range
		else if (allowCorrectRange)
		{
			//Within range
			if(value >= correctValue - accuracyTolerance && value <= correctValue + accuracyTolerance)
			{
				inRangeEvent.Invoke();
				if (useConfirmationText)
				{
					confirmationText.color = regularTextColor;
					confirmationText.text = correctString;
				}
			}
			else //Incorrect
			{
				incorrectEvent.Invoke();
				if (useConfirmationText)
				{
					confirmationText.color = errorTextColor;
					confirmationText.text = incorrectString;
				}
			}
		}
		// Incorrect		
		else
		{
            incorrectEvent.Invoke();
			if (useConfirmationText)
			{
				confirmationText.color = errorTextColor;
				confirmationText.text = incorrectString;
			}
		}
	}

	/// <summary>
	/// Get difference between passed value and correct value.
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	public float ReturnDifference(float value)
	{
		return correctValue - value;
	}
}
