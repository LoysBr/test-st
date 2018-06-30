using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

	public GameplayManager gameplayManager;

	void Update () {
		
	}

	public void OnClicked_Pause()
	{
		Debug.Log("Pause button clicked");

		gameplayManager.PauseGame();
	}

	public void OnClicked_EndGame()
	{
		Debug.Log("EndGame button clicked");

		gameplayManager.SaveGame();
		gameplayManager.ClearCurrentGame();
	}
	public void OnClicked_NewGame()
	{
		Debug.Log("NewGame button clicked");

		gameplayManager.SaveGame();
		gameplayManager.ClearCurrentGame();
		gameplayManager.StartNewGame();
	}
	public void OnClicked_Quit()
	{
		Debug.Log("Quit button clicked");
		Quit();
	}
	public void OnClicked_Leaderboard()
	{
		Debug.Log("Leaderboard button clicked");

		//TODO
	}

	public void Quit()
	{
		Application.Quit();
	}
}
