using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameInstance {

	//a Turn is spent each time Tetriminos touches ground
	//a Step is the time between 2 tetrimino "falling step"

	public float 		m_currentStepDuration = 1f;
	private float 		m_currentStepElapsedTime;
	private int 		m_turnsCount;

	public int 			m_numberOfTurnToLevelUp = 2;
	public float 		m_levelUpStepDurationDiminishing = 0.1f;
	public float 		m_minimumStepDuration = 0.2f;
	public int 			m_currentPointsForLine = 10;
	public int 			m_currentTotalPointsForTetris = 100;

	private bool 		m_isPaused = false;
	public float 		m_currentTime = 0.0f;
	public int 			m_currentLevel = 1;
	public int 			m_currentLines = 0;
	public int 			m_currentScore = 0;

	public bool 		m_gameOver = false;

	private Tetrimino 	m_currentTetrimino;
	private GameGrid 	m_grid;

    private List<Tetrimino.eTetriminoType> m_availableTetrimini;


    public GameInstance(int _gridSizeX, int _gridSizeY, Func<bool> _refreshRenderingMethod, ref List<TweakingMenu.TetriminoTweak> _availableTetrimini) 
	{
        m_availableTetrimini = new List<Tetrimino.eTetriminoType>();
        for (int i = 0; i < _availableTetrimini.Count; i++)
        {
            if(_availableTetrimini[i].m_type != Tetrimino.eTetriminoType.BLANK)
                m_availableTetrimini.Add(_availableTetrimini[i].m_type);
        }

        m_grid = new GameGrid(_gridSizeX, _gridSizeY, _refreshRenderingMethod);
		m_grid.OnInstantiateTetrimino(InstantiateNewTetrimino());
	}

	public bool Update(float _elapsedTime)
	{
		if(m_gameOver) return false;
		if(m_isPaused) return true;

		m_currentTime += _elapsedTime;
		m_currentStepElapsedTime += _elapsedTime;

		if(m_currentStepElapsedTime >= m_currentStepDuration)
		{
			if(!m_grid.ProceedStep()) //if tetrimino is blocked -> proceedTurn
			{
				if(!ProceedTurn())
					return false;
			}
			
			m_currentStepElapsedTime = 0;
		}

		return true;
	}

	public void Pause(bool gameOver = false)
	{
		if(gameOver)
		{
			m_isPaused = true;
			m_gameOver = true;
			return;
		}
		
		m_isPaused = !m_isPaused;
	}

	public Tetrimino InstantiateNewTetrimino()
	{
		m_currentTetrimino = new Tetrimino(ref m_availableTetrimini);
		return m_currentTetrimino;
	}

	public bool ProceedTurn()
	{
		//checking gameOver
		int count = 0;
		foreach(Vector2Int pos in m_currentTetrimino.GetCurrentCellsPositions())
		{			
			if(!m_grid.PositionIsInsideGrid(pos))	
				count++;
			if(count >= m_currentTetrimino.GetCurrentCellsPositions().Count)
				return false;
		}		

		//increment turnsCount
		m_turnsCount++;

		//check if turns will add difficulty : increment level
		//level up = diminish stepDuration
		if(m_turnsCount % m_numberOfTurnToLevelUp == 0)
			IncrementLevel();

		//check if there are new lines created
		int linesCount = m_grid.ProceedTurn(); //this will absorb current tetri
		GrandPointsForLines(linesCount);
		m_currentLines += linesCount;

		m_grid.OnInstantiateTetrimino(InstantiateNewTetrimino()); //then create the new tetri

		return true;
	}		

	public void IncrementLevel()
	{
		m_currentLevel++;
		m_currentStepDuration -= m_levelUpStepDurationDiminishing;
		if(m_currentStepDuration <= m_minimumStepDuration)
			m_currentStepDuration = m_minimumStepDuration;

		m_currentPointsForLine *= 2; //we just double values -> we could tweak better
		m_currentTotalPointsForTetris *= 2;
	}

	public void GrandPointsForLines(int _numberOfLines)
	{
		int points = 0;
		switch(_numberOfLines)
		{
		case 1:
			points = m_currentPointsForLine;
			break;
		case 2:
			points = m_currentPointsForLine + Mathf.CeilToInt((float)(m_currentTotalPointsForTetris - m_currentPointsForLine) * 0.2f);
			break;
		case 3:
			points = m_currentPointsForLine + Mathf.CeilToInt((float)(m_currentTotalPointsForTetris - m_currentPointsForLine) * 0.5f);
			break;
		case 4:
			points = m_currentPointsForLine = m_currentTotalPointsForTetris;
			break;
		default: break;
		}

		m_currentScore += points;
	}

	public GameGrid GetGameGrid()
	{
		return m_grid;
	}

	public void TurnTetrimino()
	{
		if(m_grid.CanTetriminoTurn())
		{
			m_currentTetrimino.Turn();
			m_grid.UpdateGrid();
		}
	}

	public void MoveTetriminoLeft()
	{
		m_grid.MoveTetriminoLeft();
	}

	public void MoveTetriminoRight()
	{
		m_grid.MoveTetriminoRight();
	}

	public void MoveTetriminoDown()
	{
		//TODO : améliorer input / diff instant down avec long down
		m_grid.MoveTetriminoDown();
	}
}
