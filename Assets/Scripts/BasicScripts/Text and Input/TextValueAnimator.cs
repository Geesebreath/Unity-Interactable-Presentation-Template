using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Gradually count up/down from a starting value to a target value over time.
/// </summary>
public class TextValueAnimator : MonoBehaviour, CommonInterface
{
	[SerializeField]
	private Text textObject;
	// Value
	public float startingValue, targetValue, valuePerSecond;
	// String to append on value if necessary, ie. " cm." for a cm value would show for example "75 cm." in the text field.
	public string modifierString;
	public string textFormat = string.Empty;
	private Coroutine routine;

	public void Reset()
	{
		StopAllCoroutines();
		if (textObject == null)
		{
			textObject = GetComponent<Text>();
		}
		SetText(startingValue);
	}

	public void AnimateValue()
	{
		routine = StartCoroutine(ValueRoutine(startingValue));
	}

	public void SetText(float value)
	{
		if (modifierString != null)
		{
			textObject.text = value.ToString(textFormat) + modifierString;

		}
		else
		{
			textObject.text = value.ToString(textFormat);
		}
	}

	public IEnumerator ValueRoutine(float value)
	{
		float val = value;
		SetText(val);
		yield return null;

		if (targetValue<startingValue && valuePerSecond >0)
		{
			valuePerSecond *= -1;
		}

		// convert if target value is less than startingvalue
		float modifier = 0.0001f;
		if (targetValue < val)
		{
			modifier = -1;
		}

		while (val * modifier < targetValue * modifier)
		{
			val += (valuePerSecond * Time.deltaTime);
			SetText(val);
			yield return null;
		}
		SetText(targetValue);
	}
}
