using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
/// <summary>
/// Triggers a UnityEvent after a set period of time.
/// </summary>
public class TimerEvent : MonoBehaviour
{
    [SerializeField]
    private float timerLength;
    public UnityEvent timerEndingEvent;

    private float totalTimePassed;
    private Coroutine coroutine;        
    
    //Start Timer
	public void StartTimer()
	{
        StopAllCoroutines();
        coroutine = StartCoroutine(TimerCoroutine());
	}
    /// <summary>
    /// Start Timer for a passed length of time (seconds)
    /// </summary>
    /// <param name="newLength"></param>
    public void StartTimer(float newLength)
	{
        timerLength = newLength;
        StartTimer();
    }

/// <summary>
/// End timer early
/// </summary>
    public void EndTimer()
	{
        StopAllCoroutines();
    }

    IEnumerator TimerCoroutine()
	{
        // Starts next frame
        totalTimePassed = 0;
        yield return null;
        
        // Keep time and update UI for timer value.
        
        while (totalTimePassed < timerLength)
		{
            totalTimePassed += Time.deltaTime;
            yield return null;
		}
        totalTimePassed = 0;
        timerEndingEvent.Invoke();
	}
}
