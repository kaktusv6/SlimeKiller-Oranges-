using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GuiController : MonoBehaviour
{
	public void ClickExitGame()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}

	public void ClickStartGame()
	{
		SceneManager.LoadScene("Game", LoadSceneMode.Single);
	}

	public void ClickLoadMenu()
	{
		SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
	}
}
