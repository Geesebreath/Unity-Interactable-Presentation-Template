using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Raises all events that are stored in the event list.
/// </summary>
public class EventRaiser: MonoBehaviour
{
	public List<GameEvent> events;

    public void RaiseEvents()
	{
		if(events.Count>0)
		{
			for (int i = events.Count - 1; i >= 0; i--)
			{
				events[i].Raise();
			}
		}
	}
}
