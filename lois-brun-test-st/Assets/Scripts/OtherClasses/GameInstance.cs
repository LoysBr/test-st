﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance {

	//a Turn is spent each time Tetriminos touches ground
	//a Step is the time between 2 tetrimino "falling step"

	private float 		m_currentStepDuration = 1;
	private float 		m_currentStepElapsedTime;
	private int 		m_turnsCount;

	public int 			m_numberOfTurnToLevelUp = 2;
	public float 		m_levelUpStepDurationDiminishing = 0.1f;
	public float 		m_minimumStepDuration = 0.2f;
	public int 			m_currentPointsForLine = 10;
	public int 			m_currentTotalPointsForTetris = 100;

	private bool 		m_isPaused = false;
	private float 		m_currentTime = 0.0f;
	private int 		m_currentLevel = 1;
	private int 		m_currentLines = 0;
	private int 		m_currentPoints = 0;

	private Tetrimino 	m_currentTetrimino;
	private GameGrid 	m_grid;


	public GameInstance(int _gridSizeX, int _gridSizeY) 
	{
		m_grid = new GameGrid(_gridSizeX, _gridSizeY, InstantiateNewTetrimino(_gridSizeY));
		m_grid.OnInstantiateTetrimino();
	}

	public void Update(float _elapsedTime)
	{
		if(m_isPaused) return;

		m_currentTime += _elapsedTime;
		m_currentStepElapsedTime += _elapsedTime;

		if(m_currentStepElapsedTime >= m_currentStepDuration)
		{
			m_grid.ProceedStep();
			m_currentStepElapsedTime = 0;
		}
	}

	public void Pause()
	{
		m_isPaused = !m_isPaused;
	}

	public Tetrimino InstantiateNewTetrimino(int _gridSizeY)
	{
		m_currentTetrimino = new Tetrimino(_gridSizeY);
		return m_currentTetrimino;
	}

	public void ProceedTurn()
	{
		//increment turnsCount
		m_turnsCount++;

		//check if turns will add difficulty : increment level
		//level up = diminish stepDuration
		if(m_turnsCount % m_numberOfTurnToLevelUp == 0)
			IncrementLevel();

		//check if there are new lines created
		int linesCount = m_grid.ProceedTurn(m_currentTetrimino);
		GrandPointsForLines(linesCount);

		InstantiateNewTetrimino(m_grid.GetGridSizeY());
	}		

	public void IncrementLevel()
	{
		m_currentLevel++;
		m_currentStepDuration -= m_levelUpStepDurationDiminishing;
		if(m_currentStepDuration <= m_minimumStepDuration)
			m_currentStepDuration = m_minimumStepDuration;
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

		m_currentPoints += points;
	}

	public GameGrid GetGameGrid()
	{
		return m_grid;
	}

	public void TurnTetrimino()
	{
		m_currentTetrimino.Turn();
		m_grid.UpdateGrid(m_currentTetrimino);
	}

	public void MoveTetriminoLeft()
	{
		m_grid.MoveTetriminoLeft();
		m_grid.UpdateGrid(m_currentTetrimino);
	}

	public void MoveTetriminoRight()
	{
		m_grid.MoveTetriminoRight();
		m_grid.UpdateGrid(m_currentTetrimino);
	}

	public void MoveTetriminoDown()
	{
		//TODO : améliorer input / diff instant down avec long down
		if(!m_grid.MoveTetriminoDown())
		{						
			ProceedTurn();
			m_grid.UpdateGrid(m_currentTetrimino);
		}
	}
}
