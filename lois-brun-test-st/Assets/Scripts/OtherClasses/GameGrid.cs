using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameGrid  {

	public Cell[,] 			m_gridTab;
	private int 			m_gridSizeX;
	private int 			m_gridSizeY;

	//private Vector2Int 		m_tetriminoPosition;
	private Tetrimino 		m_currentTetrimino;

	Func<bool> 				RefreshRendering;

	public GameGrid(int _gridSizeX, int _gridSizeY, Func<bool> _refreshRenderingMethod)
	{
		m_gridSizeY = _gridSizeY;
		m_gridSizeX = _gridSizeX;
		RefreshRendering = _refreshRenderingMethod;

		m_gridTab = new Cell[m_gridSizeX, m_gridSizeY];

		for(int x = 0; x < m_gridSizeX; x++)
		{
			for(int y = 0; y < m_gridSizeY; y++)
			{
				m_gridTab[x, y] = new Cell();
			}
		}

		//m_tetriminoPosition = new Vector2Int();
	}

	public void OnInstantiateTetrimino(Tetrimino _currentTetrimino)
	{
		m_currentTetrimino = _currentTetrimino;

		m_currentTetrimino.SetPosition(new Vector2Int(
			Mathf.CeilToInt(((float) m_gridSizeX) * 0.5f) - 2, // - 2 is because tetri config is adding 0-4 x
			m_gridSizeY - 1));
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

	public void MoveTetriminoLeft()
	{
		Vector2Int futurePos = new Vector2Int(m_currentTetrimino.GetPosition().x - 1, m_currentTetrimino.GetPosition().y);
		if(CanThoseCellsMove(futurePos))
		{			
			m_currentTetrimino.SetPosition(futurePos);
			UpdateGrid();
		}
	}

	public void MoveTetriminoRight()
	{
		Vector2Int futurePos = new Vector2Int(m_currentTetrimino.GetPosition().x + 1, m_currentTetrimino.GetPosition().y);
		if(CanThoseCellsMove(futurePos))
		{			
			m_currentTetrimino.SetPosition(futurePos);
			UpdateGrid();
		}
	}

	public bool MoveTetriminoDown()
	{
		Vector2Int futurePos = new Vector2Int(m_currentTetrimino.GetPosition().x, m_currentTetrimino.GetPosition().y - 1);

		foreach(Vector2Int pos in m_currentTetrimino.GetCellsPositions(futurePos))
		{
			if(pos.y < 0) //ground check
				return false;	
			
			//if it's a grid cell
			if(PositionIsInsideGrid(pos))
			{
				Cell cell = m_gridTab[pos.x, pos.y];
				if(cell.GetCellType() == Cell.eCellType.GRID_CELL)
					return false;				
			}						
		}

		m_currentTetrimino.SetPosition(futurePos);
		UpdateGrid();
		return true;
	}

	public bool CanThoseCellsMove(Vector2Int _futurePos)
	{
		foreach(Vector2Int pos in m_currentTetrimino.GetCellsPositions(_futurePos))
		{	
			if(PositionIsInsideGrid(pos))
			{
				if(m_gridTab[pos.x, pos.y].GetCellType() == Cell.eCellType.GRID_CELL)
					return false;	
			}
			else 
				return false; //out of limits 
		}

		return true;
	}

	public bool PositionIsInsideGrid(Vector2Int _pos)
	{
		if( _pos.x < m_gridSizeX && _pos.x >= 0 &&
			_pos.y < m_gridSizeY && _pos.y >= 0)
			return true;
		else
			return false;
	}

	//returns number of lines (0 to 4)
	public int UpdateGrid( bool _absorbTetrimino = false)
	{
		List<int> linesYIndex = new List<int>(); //will store lines

		List<Vector2Int> previousTetriminoCells = m_currentTetrimino.GetPreviousCellsPositions();
		List<Vector2Int> newTetriminoCells = m_currentTetrimino.GetCurrentCellsPositions();

		//clear old ones
		foreach(Vector2Int _pos in previousTetriminoCells)
		{
			if(PositionIsInsideGrid(_pos))
			{
				m_gridTab[_pos.x, _pos.y].SetCellType(Cell.eCellType.BLANK);
				m_gridTab[_pos.x, _pos.y].SetCellTetriminoType(Tetrimino.eTetriminoType.BLANK);
			}
		}
		//set new ones
		foreach(Vector2Int _pos in newTetriminoCells)
		{
			if(PositionIsInsideGrid(_pos))
			{
				m_gridTab[_pos.x, _pos.y].SetCellType(_absorbTetrimino ? Cell.eCellType.GRID_CELL : Cell.eCellType.TETRIMINO_CELL);
				m_gridTab[_pos.x, _pos.y].SetCellTetriminoType(m_currentTetrimino.GetTetriminoType());

				//check lines (maybe redundant but doesn't cost much...) 
				if(_absorbTetrimino)
				{
					if(IsMakingALine(_pos.y)) 
					{
						if(!linesYIndex.Contains(_pos.y))
							linesYIndex.Add(_pos.y);
					}
				}
			}
		}

		foreach(int lineIndex in linesYIndex)
		{
			RemoveLine(lineIndex);
		}

		RefreshRendering();

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
				if(cellCount >= GetGridSizeX())
					return true;				
			}
		}

		return false;
	}

	public int ProceedTurn()
	{
		return UpdateGrid(true);
	}

	public bool ProceedStep()
	{
		if(MoveTetriminoDown())
			return true;
		else
			return false;  //means we need to ProceedTurn
	}
		 
	public void RemoveLine(int _lineY)
	{
		//for each Cell with Y >= _lineY, do x = x + 1
		for(int x = 0; x < m_gridSizeX; x++)
		{
			for(int y = _lineY; y < m_gridSizeY - 1; y++)				
				m_gridTab[x, y] = m_gridTab[x, y + 1];

			//for last row (top) -> make new 
			m_gridTab[x, m_gridSizeY - 1] = new Cell();
		}

		Debug.Log("Removed line " + _lineY);
	}
}
