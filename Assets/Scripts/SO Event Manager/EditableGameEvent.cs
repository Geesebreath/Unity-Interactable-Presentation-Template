using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Version of GameEvent that is editable during runtime.
/// </summary>
[CreateAssetMenu]
public class EditableGameEvent : GameEvent
{
	public void ChangeGameObject(GameObject passedGameObject)
	{
		gameObject = passedGameObject;
	}

	public void ChangeInt(int passedInt)
	{
		intValue = passedInt;
	}
	public void ChangeInt(string passedInt)
	{
		int.TryParse(passedInt, out intValue);
	}

	public void ChangeBool(bool passedBool)
	{
		boolValue = passedBool;
	}

	public void ChangeFloat(float passedFloat)
	{
		floatValue = passedFloat;
	}
	public void ChangeFloat(string passedFloat)
	{
		float.TryParse(passedFloat, out floatValue);
	}

	public void ChangeSO(ScriptableObject so)
	{
		scriptableObjects = so;
	}
}

