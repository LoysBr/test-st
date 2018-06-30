using System.Collections;
using System.Collections.Generic;

public class GameInstance {

	private bool 		m_isPaused = false;
	private float 		m_currentTime = 0.0f;
	private int 		m_currentLevel = 1;
	private int 		m_currentLines = 0;

	private GameGrid 	m_grid;

	public GameInstance(int _gridSizeX, int _gridSizeY) 
	{
		m_grid = new GameGrid(_gridSizeX, _gridSizeY);
	}

	public void Update(float _elapsedTime)
	{
		if(m_isPaused) return;

		m_currentTime += _elapsedTime;
	}

	public void Pause()
	{
		m_isPaused = !m_isPaused;
	}

	public GameGrid GetGameGrid()
	{
		return m_grid;
	}
}
