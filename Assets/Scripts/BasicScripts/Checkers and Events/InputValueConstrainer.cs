using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// On text changed, check if value falls outside min/max range. If so, set to min/max.
/// </summary>
[RequireComponent(typeof(InputField))]
public class InputValueConstrainer : MonoBehaviour
{
	// Contains the rules for validation, changes and sets values on Text Input this is placed on
	// TODO: Can put in negative sign in middle of value occassionally
	private InputField inputField;
	[SerializeField]
	private float defaultValue = 0;
	[SerializeField]
	private bool allowNegatives = false; 
	[SerializeField]
	private float minValue = 0, maxValue = 100;
	
	private void Awake()
	{
		inputField = GetComponent<InputField>();
		// Set a delegate on the inputfield  to validate every char entered
		inputField.onValidateInput += delegate (string input, int charIndex, char addedChar) { return ValidateLastChar(addedChar); };
	}

	// Code any characters to remove from the input field here.
	public char ValidateLastChar(char charToValidate)
	{
		if (!char.IsDigit(charToValidate))
		{

			if(charToValidate != '-' && charToValidate != '.')
			{
				charToValidate = '\0';
			}
			//if (charToValidate == '+')
			//{
			//	charToValidate = '\0';
			//}

			if (!allowNegatives)
			{
				if (charToValidate == '-')
				{
					charToValidate = '\0';
				}
			}
		}

		return charToValidate;
	}
	/// <summary>
	/// Compares input to min/max range and adjusts to be within range.
	/// </summary>
	/// <param name="textValue"></param>
	public void ValidateInput(string textValue)
	{
		if(textValue!=null && textValue!=string.Empty && textValue!= "-")
		{
			float value = float.Parse(textValue);
			if (value < minValue)
			{
				inputField.text = minValue.ToString();
			}
			else if (value > maxValue)
			{
				inputField.text = defaultValue.ToString();
			}
		}
		
	}
}
