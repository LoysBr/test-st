using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : MonoBehaviour {

	public GridRenderingManager 	m_gridRenderer;
	public MenuManager 				m_menuManager;
	private GameInstance 			m_gameInstance;

	public int 						m_gridSizeX = 10;
	public int 						m_gridSizeY = 22;

	//public int 					m_numberOfTurnToLevelUp = 2;
	public float 					m_startingStepDuration = 1;
	public float 					m_levelUpStepDurationDiminishing = 0.1f;
	public float 					m_minTimeBetweenMoveDownInputs = 0.5f;
	public float 					m_minimumStepDuration = 0.2f;
	public int 						m_startingPointsForLine = 10;
	public int 						m_startingTotalPointsForTetris = 100;

	private float 					m_downInputDuration = 0;
	private bool 					m_keepPressingDown;
	private int 					m_numberOfDownRequest;

	public GridRenderingManager GetRenderingManager() 
	{ 
		return m_gridRenderer; 
	}

	public void Start()
	{
		m_gridRenderer.InitializeGrid(m_gridSizeX, m_gridSizeY);
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
		m_gameInstance.m_levelUpStepDurationDiminishing = m_levelUpStepDurationDiminishing;
		m_gameInstance.m_minimumStepDuration = m_minimumStepDuration;
		m_gameInstance.m_currentPointsForLine = m_startingPointsForLine;
		m_gameInstance.m_currentTotalPointsForTetris = m_startingTotalPointsForTetris;
		m_gameInstance.m_currentStepDuration = m_startingStepDuration;

		m_gridRenderer.InitializeGrid(m_gridSizeX, m_gridSizeY);

		m_downInputDuration = 0.0f;
		m_keepPressingDown = false;
		m_numberOfDownRequest = 0;

		m_menuManager.GameOver(false);

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
		m_menuManager.GameOver(true);
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
		if(Input.GetKey(KeyCode.DownArrow))
		{
			m_downInputDuration += Time.deltaTime;

			if(m_downInputDuration >= m_minTimeBetweenMoveDownInputs)
			{
				m_keepPressingDown = true;
				int n = (int) Mathf.Ceil(m_downInputDuration / m_minTimeBetweenMoveDownInputs);
				if (n > m_numberOfDownRequest)
				{
					m_numberOfDownRequest = n;
					OnInput_Down();
				}
			}
			else if(Input.GetKeyDown(KeyCode.DownArrow))
				OnInput_Down();
		}
		else
		{	
			m_downInputDuration = 0.0f;
			m_keepPressingDown = false;
			m_numberOfDownRequest = 0;
		}

		if(!m_gameInstance.Update(Time.deltaTime))   //if return false : GameOver
			GameOver();

		m_menuManager.RefreshUIGameValues(m_gameInstance);
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

	public int GetCurrentGameScore()
	{
		if(m_gameInstance != null)
		{
			return m_gameInstance.m_currentScore;
		}
		else
			return 0;
	}
}
