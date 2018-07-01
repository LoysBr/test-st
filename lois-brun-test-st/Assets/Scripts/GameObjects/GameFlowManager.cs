using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : MonoBehaviour {

	public GridRenderingManager 	m_gridRenderer;
	public MenuManager 				m_menuManager;
	private GameInstance 			m_gameInstance;

	public int 						m_gridSizeX = 10;
	public int 						m_gridSizeY = 22;

	public GridRenderingManager GetRenderingManager() 
	{ 
		return m_gridRenderer; 
	}

	public void ClearCurrentGame()
	{
		m_gameInstance = null;

		m_gridRenderer.ClearGrid();

		Debug.Log("ClearCurrentGame()");
	}
	public void StartNewGame()
	{
		m_gameInstance = new GameInstance(m_gridSizeX, m_gridSizeY, m_gridRenderer.Refresh);

		m_gridRenderer.InitializeGrid(m_gridSizeX, m_gridSizeY);

		Debug.Log("StartNewGame ()");
	}
	public void PauseGame()
	{
		if(m_gameInstance != null)
		{
			m_gameInstance.Pause();
		}
	}
	public void SaveGame()
	{
		Debug.Log("Save Game ()");
	}

	public void GameOver()
	{
		m_gameInstance.Pause(true);
		Debug.Log("GameOver ()");
	}

	void Update () {
		if(Input.GetKeyUp(KeyCode.Escape))
			OnInput_Escape();

		if(m_gameInstance == null)
			return;
		if(m_gameInstance.m_gameOver)
			return;
		
		if(Input.GetKeyDown(KeyCode.UpArrow))
			OnInput_Up();
		if(Input.GetKeyDown(KeyCode.LeftArrow))
			OnInput_Left();
		if(Input.GetKeyDown(KeyCode.RightArrow))
			OnInput_Right();
		if(Input.GetKeyDown(KeyCode.DownArrow))
			OnInput_Down();

		if(!m_gameInstance.Update(Time.deltaTime))   //if return false : GameOver
			GameOver();
	}

	public Tetrimino.eTetriminoType GetCellTetriminoType(int _x, int _y)
	{
		if(m_gameInstance == null)
			return Tetrimino.eTetriminoType.BLANK;
		else
		{
			return m_gameInstance.GetGameGrid().GetCellTetriminoType(_x, _y);
		}
	}

	public void OnInput_Up()
	{
		m_gameInstance.TurnTetrimino();
	}
	public void OnInput_Left()
	{
		m_gameInstance.MoveTetriminoLeft();
	}
	public void OnInput_Right()
	{
		m_gameInstance.MoveTetriminoRight();
	}
	public void OnInput_Down()
	{
		m_gameInstance.MoveTetriminoDown();
	}
	public void OnInput_Escape()
	{
		m_menuManager.Quit();
	}
}
