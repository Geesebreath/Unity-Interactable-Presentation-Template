using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullScreenManager : MonoBehaviour
{
	[SerializeField]
	Image buttonImage;

	[SerializeField]
	Sprite fullscreenSprite, smallscreenSprite;

	private void Start()
	{
		if (Screen.fullScreen)
		{
			buttonImage.sprite = smallscreenSprite;
		}
	}
	
	public void ToggleScreen()
	{
		Screen.fullScreen = !Screen.fullScreen;
		Debug.Log("Screen toggled");
	}

	public void ChangeSprite()
	{
		if (buttonImage.sprite== fullscreenSprite)
		{
			buttonImage.sprite = smallscreenSprite;
		}
		else
		{
			buttonImage.sprite = fullscreenSprite;
		}
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Screen.fullScreen = false;
			buttonImage.sprite = fullscreenSprite;
		}
		SetSpriteToCurrent();
	}

	public void SetSpriteToCurrent()
	{
		if(Screen.fullScreen)
		{
			buttonImage.sprite = smallscreenSprite;
		}
		else
		{
			buttonImage.sprite = fullscreenSprite;
		}
	}
}
