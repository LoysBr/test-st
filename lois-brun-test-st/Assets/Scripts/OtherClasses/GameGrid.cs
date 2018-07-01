using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid  {

	public Cell[,] 			m_gridTab;
	private int 			m_gridSizeX;
	private int 			m_gridSizeY;

	//private Vector2Int 		m_tetriminoPosition;
	private Tetrimino 		m_currentTetrimino;

	//public GridRenderingManager

	public GameGrid(int _gridSizeX, int _gridSizeY)
	{
		m_gridSizeY = _gridSizeY;
		m_gridSizeX = _gridSizeX;
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
		if(CanMoveTetriminoLeft())
			m_currentTetrimino.SetPosition(new Vector2Int(m_currentTetrimino.GetPosition().x - 1, m_currentTetrimino.GetPosition().y));
	}

	public void MoveTetriminoRight()
	{
		if(CanMoveTetriminoRight())
			m_currentTetrimino.SetPosition(new Vector2Int(m_currentTetrimino.GetPosition().x + 1, m_currentTetrimino.GetPosition().y));
	}

	public bool MoveTetriminoDown()
	{
		if(CanMoveTetriminoDown())
		{			
			m_currentTetrimino.SetPosition(new Vector2Int(m_currentTetrimino.GetPosition().x, m_currentTetrimino.GetPosition().y - 1));
			return true;
		}
		else
			return false;
	}

	public bool CanMoveTetriminoLeft()
	{
		//TODO
		return true;
	}
	public bool CanMoveTetriminoRight()
	{
		//TODO
		return true;
	}
	public bool CanMoveTetriminoDown()
	{
		if(m_currentTetrimino.GetPosition().y + m_currentTetrimino.GetBottomY() <= 0) //means pos = 0 or -1 and bottY 0 or 1
			return false;

		return true;
	}

	//returns number of lines (0 to 4)
	public int UpdateGrid( bool _absorbTetrimino = false)
	{
		List<int> linesYIndex = new List<int>();

//		//let's parse every cell of tetrimono's config
//		for(int x = 0; x < 4; x++)
//		{
//			for(int y = 0; y < 4; y++)
//			{
//				//new Tetri position -> change Cell
//				if(m_currentTetrimino.GetCurrentConfiguration()[x, y] == 1)
//				{	
//					if (y + m_tetriminoPosition.y < m_gridSizeY - 1) // TODO resovle null error here
//					{
//						m_gridTab[x + m_tetriminoPosition.x, y + m_tetriminoPosition.y].SetCellType(_absorbTetrimino ? Cell.eCellType.GRID_CELL : Cell.eCellType.TETRIMINO_CELL);
//						m_gridTab[x + m_tetriminoPosition.x, y + m_tetriminoPosition.y].SetCellTetriminoType(m_currentTetrimino.GetTetriminoType());
//					}
//
//					//check lines (maybe redundant but doesn't cost much...) 
//					if(_absorbTetrimino)
//					{
//						if(IsMakingALine(m_tetriminoPosition.y + y)) 
//						{
//							if(!linesYIndex.Contains(m_tetriminoPosition.y + y))
//								linesYIndex.Add(m_tetriminoPosition.y + y);
//						}
//					}
//				}
//			}
//		}

		List<Vector2Int> previousTetriminoCells = m_currentTetrimino.GetPreviousCellPositions();
		List<Vector2Int> newTetriminoCells = m_currentTetrimino.GetCurrentCellPositions();

		//clear old ones
		foreach(Vector2Int _pos in previousTetriminoCells)
		{
			if(_pos.x < m_gridSizeX && _pos.y < m_gridSizeY)
			{
				m_gridTab[_pos.x, _pos.y].SetCellType(Cell.eCellType.BLANK);
				m_gridTab[_pos.x, _pos.y].SetCellTetriminoType(Tetrimino.eTetriminoType.BLANK);
			}
		}
		//set new ones
		foreach(Vector2Int _pos in newTetriminoCells)
		{
			if(_pos.x < m_gridSizeX && _pos.y < m_gridSizeY)
			{
				m_gridTab[_pos.x, _pos.y].SetCellType(_absorbTetrimino ? Cell.eCellType.GRID_CELL : Cell.eCellType.TETRIMINO_CELL);
				m_gridTab[_pos.x, _pos.y].SetCellTetriminoType(m_currentTetrimino.GetTetriminoType());
			}

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

		foreach(int lineIndex in linesYIndex)
		{
			RemoveLine(lineIndex);
		}

		return linesYIndex.Count;
	}

	public void RefreshGridRendering()
	{
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

	public int ProceedTurn()
	{
		return UpdateGrid(true);
	}

	public bool ProceedStep()
	{
		if(MoveTetriminoDown())
		{
			UpdateGrid(false);
			return true;
		}
		else
			return false;
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
