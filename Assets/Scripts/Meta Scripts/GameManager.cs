using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Scene Loading and any other necessary sim-wide funcationality
/// </summary>
public class GameManager : MonoBehaviour
{ 
	public void LoadScene(int index)
	{
		SceneManager.LoadScene(index);
	}

	public void LoadScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
}
