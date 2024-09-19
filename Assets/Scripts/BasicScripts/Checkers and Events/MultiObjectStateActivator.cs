using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Contains a dictionary of game objects that can be set to Active or Inactive. If all objects are set to active, an event triggers. 
/// Used for if a series or collection of actions need to be completed to satisfy a condition.
/// </summary>
public class MultiObjectStateActivator : MonoBehaviour, CommonInterface
{
    public GameObject[] trackingObjects;
    public UnityEvent allObjectsActiveAction, objectInactiveAction;
    // Dictionary to simplify the Inspector side of using this. Also easier to search through list of  a passed object with built in methods.
    private Dictionary<GameObject, bool> trackingDictionary;

    void Start()
    {
        trackingDictionary = new Dictionary<GameObject, bool>();
        foreach(GameObject obj in trackingObjects)
		{
            trackingDictionary.Add(obj, false);
		}
    }

    /// <summary>
	/// Passed object, if in dictionary, is marked as being Active. If all objects are active, correct event is triggered.
	/// </summary>
	/// <param name="obj"></param>
    public void ObjectSetCorrect(GameObject obj)
	{
		if (trackingDictionary.ContainsKey(obj))
		{
			trackingDictionary[obj] = true;
		}

        // Check all objects correct
        bool inPlace = true;
        foreach (KeyValuePair<GameObject, bool> entry in trackingDictionary)
        {
            if (inPlace)
            {
                inPlace = entry.Value;
            }
        }
        if (inPlace)
        {
            allObjectsActiveAction.Invoke();
        }
    }

    /// <summary>
	/// Passed object, in in dictionary, is marked as Inactive. Invokes the ObjectOutOfPlace event if all objects were initially in place.
	/// </summary>
	/// <param name="obj"></param>
    public void ObjectSetIncorrect(GameObject obj)
	{
        if(trackingDictionary == null)
		{
            Start();
		}
        // If all objects were in place, invoke the objectOutOfPlace event. Only necessary to go from 'active' to 'inactive'.
        bool isActive = true;
        foreach(KeyValuePair<GameObject,bool> entry in trackingDictionary)
		{
            if(isActive)
			{
                isActive = entry.Value;
			}
		}
        if(isActive)
		{
            objectInactiveAction.Invoke();
		}

        if (trackingDictionary.ContainsKey(obj))
        {
            trackingDictionary[obj] = false;
        }
    }

	public void Reset()
	{
        Start();
	}
}

