using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	public GameFlowManager 	m_gameflowManager;

	public Text 			m_scoreText;
	public Text 			m_levelText;
	public Text 			m_linesCountText;

	public void Start()
	{
		InitUIGameValues();
	}

	public void InitUIGameValues()
	{
		m_scoreText.text = "Score: 0";
		m_levelText.text = "Level: 1";
		m_linesCountText.text = "Lines: 0";
	}

	public void RefreshUIGameValues(GameInstance _game)
	{
		m_scoreText.text = "Score: " + _game.m_currentScore.ToString();
		m_levelText.text = "Level: " + _game.m_currentLevel.ToString();
		m_linesCountText.text = "Lines: " + _game.m_currentLines.ToString();
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
