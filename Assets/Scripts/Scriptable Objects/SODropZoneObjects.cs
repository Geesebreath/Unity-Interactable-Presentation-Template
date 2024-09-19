using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SODropZoneObjects : ScriptableObject
{
	//[SerializeField]
	//protected List<GameObject> gameObjects;
	//public GameObject lastObject;

	protected List<SODroppableImageData> dataList;

	public void ClearList()
	{
		if (dataList == null)
		{
			dataList = new List<SODroppableImageData>();
		}
		dataList.Clear();
	}

	public void AddData(SODroppableImageData data)
	{

		if (!dataList.Contains(data))
		{
			dataList.Add(data);
		}
	}

	public void RemoveData(SODroppableImageData data)
	{
		if (dataList.Contains(data))
		{
			dataList.Remove(data);
		}
	}

	public List<SODroppableImageData> GetList()
	{
		return dataList;
	}

	public SODroppableImageData[] GetArray()
	{
		return dataList.ToArray();
	}
}
