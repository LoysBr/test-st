using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

	public GameFlowManager m_gameflowManager;

	void Update () {
		
	}

	public void OnClicked_Pause()
	{
		Debug.Log("Pause button clicked");

		m_gameflowManager.PauseGame();
	}

	public void OnClicked_EndGame()
	{
		Debug.Log("EndGame button clicked");

		m_gameflowManager.SaveGame();
		m_gameflowManager.ClearCurrentGame();
	}
	public void OnClicked_NewGame()
	{
		Debug.Log("NewGame button clicked");

		m_gameflowManager.SaveGame();
		m_gameflowManager.ClearCurrentGame();
		m_gameflowManager.StartNewGame();
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
