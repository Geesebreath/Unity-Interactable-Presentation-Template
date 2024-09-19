using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Invokes an event if a passed value falls in or out of a given range. Intended for UI sliders.
/// </summary>
public class RangeCheckAndEvent : MonoBehaviour, CommonInterface
{
	public float minimumValue, maximumValue;
	public UnityEvent inRangeEvent, outOfRangeEvent, resetEvent;

	public void CheckRange(float value)
	{
		if(value >= minimumValue && value <= maximumValue)
		{
			inRangeEvent.Invoke();
		}
		else
		{
			outOfRangeEvent.Invoke();
		}
	}

	public void Reset()
	{
		if(resetEvent==null)
		{
			outOfRangeEvent.Invoke();
		}
		else
		{
			resetEvent.Invoke();
		}
	}
}
