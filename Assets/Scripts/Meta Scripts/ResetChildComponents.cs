using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Static class that can be called by any script to reset its children. Children must have components using ComminInterface.
/// </summary>
public static class ResetChildComponents
{
	/// <summary>
	/// Reset children of passed transform using CommonInterfaces. 
	/// </summary>
	public static void ResetChildren(Transform parentTransform)
	{
		CommonInterface[] commonInterfaces = parentTransform.GetComponentsInChildren<CommonInterface>(true);
		Debug.Log(commonInterfaces.Length);
		if (commonInterfaces.Length > 0)
		{
			foreach (CommonInterface CI in commonInterfaces)
			{
				CI.Reset();
			}
		}
	}
}