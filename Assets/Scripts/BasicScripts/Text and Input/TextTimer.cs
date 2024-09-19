using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// Counts up or down (timer or stopwatch), and executes an event on timer end. Displays time in passed Text field. 
/// </summary>
public class TextTimer : MonoBehaviour, CommonInterface
{// Any changes to this class needs to be reflected in ETextTimer.

    // If true, count up indefinately (used by ETextTimer to change Editor view)
    [SerializeField]
    private bool useAsStopwatch;

    [SerializeField]
    private Text timerText;
    [SerializeField]
    private float finalTimerValue;
    [SerializeField]
    private string initialTimerString = "00:00";

    [SerializeField]
    private float timeMultiplier;
    public UnityEvent timerEndingEvent;

    private Coroutine timerCoroutine;
    private bool timerActive;
    private float  totalTimePassed;

    public void Awake()
	{
        if(timerText == null)
		{
            timerText = GetComponent<Text>();
		}
	}

    public virtual void Reset()
	{
        if (timerText == null)
        {
            timerText = GetComponent<Text>();
        }
        ResetTimer();
        if(timeMultiplier == 0)
		{
            timeMultiplier = 1;
		}
    }
    
    public void ResetTimer()
	{
        StopAllCoroutines();
        timerText.text = initialTimerString;
        timerActive = false;
    }

	public void StartTimer()
	{
        StopCoroutine(timerCoroutine);
        timerCoroutine = StartCoroutine(TimerCoroutine());
	}

    public void EndTimer()
	{
        StopCoroutine(timerCoroutine);
        timerText.text= timerText.text = finalTimerValue.ToString("00.00");
        timerEndingEvent.Invoke();
    }

    /// <summary>
    /// Begins Counting up indefinately, must be ended using EndStopwatch()
    /// </summary>
    public void StartStopwatch()
    {
        StopCoroutine(timerCoroutine);
        timerCoroutine = StartCoroutine(TimerCoroutine());
    }

    /// <summary>
    /// Ends Stopwatch timer behaviour and executes event (if any are set).
    /// </summary>
    public void EndStopwatch()
	{
        StopCoroutine(timerCoroutine);
        timerActive = false;
        timerEndingEvent.Invoke();
    }

    IEnumerator TimerCoroutine()
	{
        totalTimePassed = 0;
        // Starts next frame
        yield return null;
        
        // Keep time and update UI for timer value.
        
        while (totalTimePassed < finalTimerValue)
		{
            timerText.text = totalTimePassed.ToString("00.00");
            totalTimePassed += Time.deltaTime*timeMultiplier;
            yield return null;
		}
        totalTimePassed = 0;
        timerText.text = finalTimerValue.ToString("00.00");
        timerEndingEvent.Invoke();
	}

    IEnumerator StopwatchCoroutine()
    {
        // Starts next frame
        timerActive = true;
        totalTimePassed = 0;
        yield return null;

        // Keep time and update UI for timer value.
        while (timerActive)
        {
            timerText.text = totalTimePassed.ToString("00.00");
            totalTimePassed += Time.deltaTime * timeMultiplier;
            yield return null;
        }
    }
}
