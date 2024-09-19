using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Actived by UI buttons to move the sections forward and backward in that DLO scene by way of UI panel gameObjects.
/// Also manages small UI tasks related to this like disabling the previous button if on the first step.
/// </summary>
public class SceneSectionManager: MonoBehaviour
{
	// Activated by UI buttons to move forward and backward in that DLO scene by way of UI panel gameObjects.
	// Also manages small UI tasks related to this like disabling the previous button if on the first step.

	// Each Section of the DLO should take place on a UI panel, and put into the sectionPanels array in order,
	// with a matching help overlay in the same index

	//Enable or disable manager, used for easier testing of individual sections without navigation enabled.
	//Uncomment here and in scripts for debugging.

	[SerializeField]
    private SceneSection[] sceneSectionPanels;

	private int currentPanelIndex;

	[SerializeField]
	private GameObject buttonPrev, buttonNext, buttonReturn;

	// Bug with button animators that I can't figure out. If they are set inactive they become permanent squares and lose the
	// rounded edges that we want. Moving them out of the way instead serves same purpose and doesn't break the UI look and feel.

	private Vector2 prevStartingLocation, nextStartingLocation, returnStartingLocation;
	float hidingDistance = 2000f;

	private void Awake()
	{
		prevStartingLocation = buttonPrev.transform.localPosition;
		nextStartingLocation = buttonNext.transform.localPosition;
		returnStartingLocation = buttonReturn.transform.localPosition;
		currentPanelIndex = 0;
		foreach (SceneSection panel in sceneSectionPanels)
		{
			panel.SetActive(false);
		}
		sceneSectionPanels[0].SetActive(true);

	}

	public void SetSection(int sectionIndex)
	{
		if (sectionIndex >= 0 && sectionIndex < sceneSectionPanels.Length)
		{
			sceneSectionPanels[currentPanelIndex].SetActive(false);
			currentPanelIndex = sectionIndex;
			sceneSectionPanels[currentPanelIndex].SetActive(true);
		}
		else
		{
			Debug.LogError($"Section Index {sectionIndex} out of sectionIndex range.");
		}
	}

	public void NextSection()
	{
		//Debug.Log("Next Section");
		//if(NAbled)
		//{
		buttonPrev.SetActive(true);
		buttonPrev.transform.localPosition = prevStartingLocation;
		if (currentPanelIndex < sceneSectionPanels.Length - 1)
		{
			sceneSectionPanels[currentPanelIndex].SetActive(false);
			currentPanelIndex++;
			sceneSectionPanels[currentPanelIndex].SetActive(true);

			buttonReturn.transform.localPosition = new Vector2(buttonReturn.transform.localPosition.x, buttonReturn.transform.localPosition.y - hidingDistance);
		}

		if (currentPanelIndex == sceneSectionPanels.Length - 1)
		{
			//buttonNext.SetActive(false);
			buttonReturn.SetActive(true);

			buttonReturn.transform.localPosition = returnStartingLocation;
			buttonNext.transform.localPosition = new Vector2(buttonNext.transform.localPosition.x, buttonNext.transform.localPosition.y - hidingDistance);
		}
		//}
	}

	public void PreviousSection()
	{
		//if(NAbled)
		//{
		buttonNext.SetActive(true);
		buttonNext.transform.localPosition = nextStartingLocation;
		buttonReturn.transform.localPosition = new Vector2(buttonReturn.transform.localPosition.x, buttonReturn.transform.localPosition.y - hidingDistance);

		if (currentPanelIndex > 0)
		{
			sceneSectionPanels[currentPanelIndex].SetActive(false);
			currentPanelIndex--;
			sceneSectionPanels[currentPanelIndex].SetActive(true);
		}

		if (currentPanelIndex == 0)
		{
			//buttonPrev.SetActive(false);
			buttonPrev.transform.localPosition = new Vector2(buttonPrev.transform.localPosition.x, buttonPrev.transform.localPosition.y - hidingDistance);
		}
		//}
	}

	public void ActivateOverlay()
	{
		sceneSectionPanels[currentPanelIndex].ActivateOverlay(true);
	}

	public void DeactivateOverlay()
	{
		sceneSectionPanels[currentPanelIndex].ActivateOverlay(false);
	}

	public void ToggleOverlay()
	{
		sceneSectionPanels[currentPanelIndex].ToggleOverlay();
	}
}
