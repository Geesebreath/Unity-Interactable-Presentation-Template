using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Clear a text component instantly or over time.
/// </summary>
public class ClearText : MonoBehaviour
{
	[SerializeField]
	private float clearTime = 1;

	/// <summary>
	/// Set passed Text object to empty immediately.
	/// </summary>
	/// <param name="txt"></param>
	public void EmptyText(Text txt)
	{
		txt.text = string.Empty;
	}

	/// <summary>
	/// Sets passed text to empty after the amount of time set in component.
	/// </summary>
	/// <param name="txt"></param>
	public void DelayClear(Text txt)
	{
		StartCoroutine(TimerEmptyText(txt));
	}

	/// <summary>
	/// Empties passed Text component after a set period of time.
	/// </summary>
	/// <param name="txt"></param>
	/// <returns></returns>
	IEnumerator TimerEmptyText(Text txt)
	{
		yield return new WaitForSeconds(clearTime);
		txt.text = string.Empty;
	}
}
