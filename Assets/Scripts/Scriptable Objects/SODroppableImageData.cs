using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SODroppableImageData : ScriptableObject
{
	[SerializeField]
	private float floatData;
	[SerializeField]
	private string stringData;
	[SerializeField]
	private bool boolData;
	[SerializeField]
	private int correctDZIndex;

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

	public int GetDZ()
	{
		return correctDZIndex;
	}
}
