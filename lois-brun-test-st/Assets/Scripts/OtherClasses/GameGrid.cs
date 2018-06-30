using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid  {

	public Cell[,] 			m_gridTab;
	private int 			m_gridSizeX;
	private int 			m_gridSizeY;

	private Vector2Int 		m_tetriminoPosition;
	private Tetrimino 		m_currentTetrimino;

	public GameGrid(int _gridSizeX, int _gridSizeY, Tetrimino _currentTetrimino)
	{
		m_gridSizeY = _gridSizeY;
		m_gridSizeX = _gridSizeX;
		m_gridTab = new Cell[m_gridSizeX, m_gridSizeY];
		m_tetriminoPosition = new Vector2Int();
		m_currentTetrimino = _currentTetrimino;

	}

	public Tetrimino.eTetriminoType GetCellTetriminoType(int _x, int _y)
	{
		Cell cell = m_gridTab[_x, _y];

		//error case !
		if(cell == null)
			return Tetrimino.eTetriminoType.BLANK;

		switch (cell.GetCellType())
		{
		case Cell.eCellType.BLANK:
			return Tetrimino.eTetriminoType.BLANK;
		case Cell.eCellType.GRID_CELL:			
		case Cell.eCellType.TETRIMINO_CELL:
			return cell.GetCellTetriminoType();
		default:
			return Tetrimino.eTetriminoType.BLANK;
		}
	}

	public int GetGridSizeX()
	{
		return m_gridSizeX;
	}
	public int GetGridSizeY()
	{
		return m_gridSizeY;
	}

	public void OnInstantiateTetrimino()
	{
		m_tetriminoPosition.x = Mathf.CeilToInt(((float) m_gridSizeX) * 0.5f);
		m_tetriminoPosition.y = m_gridSizeY - 1;
	}

	public void MoveTetriminoLeft()
	{
		if(CanMoveTetriminoLeft())
			m_tetriminoPosition.x--;
	}

	public void MoveTetriminoRight()
	{
		if(CanMoveTetriminoRight())
			m_tetriminoPosition.x++;
	}

	public bool MoveTetriminoDown()
	{
		if(CanMoveTetriminoDown())
		{			
			m_tetriminoPosition.y--;
			return true;
		}
		else
			return false;
	}

	public bool CanMoveTetriminoLeft()
	{
		return true;
	}
	public bool CanMoveTetriminoRight()
	{
		return true;
	}
	public bool CanMoveTetriminoDown()
	{
		if(m_tetriminoPosition.y + m_currentTetrimino.GetBottomY() <= 0) //means pos = 0 or -1 and bottY 0 or 1
			return false;

		return true;
	}

	//returns number of lines (0 to 4)
	public int UpdateGrid(Tetrimino _currentTetrimino, bool _absorbTetrimino = false)
	{
		List<int> linesYIndex = new List<int>();

		//let's parse every cell of tetrimono's config
		for(int x = 0; x < 4; x++)
		{
			for(int y = 0; y < 4; y++)
			{
				if(_currentTetrimino.GetCurrentConfiguration()[x, y] == 1)
				{	
					if (y + m_tetriminoPosition.y < m_gridSizeY - 1) // TODO resovle null error here
					{
						m_gridTab[x + m_tetriminoPosition.x, y + m_tetriminoPosition.y].SetCellType(_absorbTetrimino ? Cell.eCellType.GRID_CELL : Cell.eCellType.TETRIMINO_CELL);
						m_gridTab[x + m_tetriminoPosition.x, y + m_tetriminoPosition.y].SetCellTetriminoType(_currentTetrimino.GetTetriminoType());
					}
					//check lines (maybe redundant but doesn't cost much...) 
					if(IsMakingALine(m_tetriminoPosition.y + y)) 
					{
						if(!linesYIndex.Contains(m_tetriminoPosition.y + y))
							linesYIndex.Add(m_tetriminoPosition.y + y);
					}
				}
			}
		}

		foreach(int lineIndex in linesYIndex)
		{
			RemoveLine(lineIndex);
		}

		return linesYIndex.Count;
	}

	public bool IsMakingALine(int _gridY)
	{
		int cellCount = 0;
		for(int xIndex = 0; xIndex < GetGridSizeX(); xIndex++)
		{
			if(m_gridTab[xIndex, _gridY].GetCellType() != Cell.eCellType.BLANK)
			{
				cellCount++;
				if(cellCount >= GetGridSizeY())
					return true;				
			}
		}

		return false;
	}

	public int ProceedTurn(Tetrimino _currentTetrimino)
	{
		return UpdateGrid(_currentTetrimino, true);
	}

	public void ProceedStep()
	{
		MoveTetriminoDown();
	}

	public void RemoveLine(int _lineY)
	{
		//for each Cell with Y >= _lineY, do x = x + 1
		for(int x = 0; x < m_gridSizeX; x++)
		{
			for(int y = _lineY; y < m_gridSizeY - 1; y++)				
				m_gridTab[x, y] = m_gridTab[x, y + 1];

			//for last row (top) -> make new 
			m_gridTab[x, m_gridSizeY] = new Cell();
		}

		Debug.Log("Removed line " + _lineY);
	}
}
