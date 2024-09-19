using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SODataContainer : ScriptableObject
{
	[SerializeField]
	private float floatData;
	[SerializeField]
	private string stringData;
	[SerializeField]
	private bool boolData;

	public float GetFloat()
	{
		return floatData;
	}

	public string GetString()
	{
		return stringData;
	}

	public bool GetBool()
	{
		return boolData;
	}

}
