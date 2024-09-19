using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Placed on the parent game object of the Scene Section. Contains what Overlay panel goes with this section for easier setup.
/// </summary>
public class SceneSection : MonoBehaviour
{
    public GameObject sectionOverlayPanel;
    public bool resetSection = false;

	private void OnEnable()
	{
		if(resetSection)
		{
			ResetChildComponents.ResetChildren(this.transform);
		}
	}

	public void SetActive(bool value)
	{
		gameObject.SetActive(value);
	}

	public void ActivateOverlay(bool value)
	{
		sectionOverlayPanel.SetActive(value);
	}

	public void ToggleOverlay()
	{
		sectionOverlayPanel.SetActive(!sectionOverlayPanel.activeInHierarchy);
	}
}
