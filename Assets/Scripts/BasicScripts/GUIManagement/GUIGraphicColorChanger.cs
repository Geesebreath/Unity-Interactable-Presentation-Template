using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Set an images base color or shift it over time by setting the start and end colors, and the duration. Trigger shift by calling 'Change Color'.
/// </summary>
[RequireComponent(typeof(Graphic))]
public class GUIGraphicColorChanger : MonoBehaviour, CommonInterface
{
	[SerializeField]
	private Graphic graphic;
	public Color startColor = Color.white, endColor = Color.black;
	private Coroutine routine;
	[SerializeField]
	private float duration;
	
	private void Awake()
	{
		if(graphic == null)
		{
			graphic = GetComponent<Graphic>();
		}
	}

	/// <summary>
	/// Directly set color of image
	/// </summary>
	/// <param name="color"></param>
	public void SetColor(Color color)
	{
		Graphic graphicToUse = graphic ?? GetComponent<Graphic>();
		graphicToUse.color = color;
	}

	/// <summary>
	/// Begin coroutine to shift color over time
	/// </summary>
	public void ChangeColorToFinal()
	{
		if(routine !=null)
		{
			StopCoroutine(routine);
		}
		routine = StartCoroutine(ChangeColorOverTime(endColor));
	}

	public void ChangeColorToStart()
	{
		if (routine != null)
		{
			StopCoroutine(routine);
		}
		routine = StartCoroutine(ChangeColorOverTime(startColor));
	}

	public void ChangeColor(Color color)
	{
		if (routine != null)
		{
			StopCoroutine(routine);
		}
		routine = StartCoroutine(ChangeColorOverTime(color));
	}

	private IEnumerator ChangeColorOverTime()
	{
		float colorIncrementer, passedTime = 0;
		graphic.color = startColor;
		while(passedTime<duration)
		{
			passedTime += Time.deltaTime;
			colorIncrementer = passedTime/duration;
			Debug.Log(colorIncrementer);
			graphic.color = Color.Lerp(startColor, endColor, colorIncrementer);
			yield return null;
		}
		graphic.color = endColor;
	}

	private IEnumerator ChangeColorOverTime(Color color)
	{
		float colorIncrementer, passedTime = 0;
		Color beginningColor = graphic.color;
		while (passedTime < duration)
		{
			passedTime += Time.deltaTime;
			colorIncrementer = passedTime / duration;
			Debug.Log(colorIncrementer);
			graphic.color = Color.Lerp(beginningColor, color, colorIncrementer);
			yield return null;
		}
		graphic.color = color;
	}

	/// <summary>
	/// Stops color change and resets back to starting color.
	/// </summary>
	public void Reset()
	{
		if (graphic == null)
		{
			graphic = GetComponent<Graphic>();
		}

		if (routine != null)
		{
			StopCoroutine(routine);
		}
		graphic.color = startColor;
	}
}
