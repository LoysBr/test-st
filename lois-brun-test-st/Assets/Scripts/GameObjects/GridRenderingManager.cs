using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridRenderingManager : MonoBehaviour {

	public GameFlowManager 	m_gameflowManager;

	public Tilemap 			m_tileMap;
	public Tile 			m_backgroundTileA;
	public Tile 			m_backgroundTileB;

	public Tile 			m_tetriminoTypeITile;
	public Tile 			m_tetriminoTypeOTile;
	public Tile 			m_tetriminoTypeTTile;
	public Tile 			m_tetriminoTypeLTile;
	public Tile 			m_tetriminoTypeJTile;
	public Tile 			m_tetriminoTypeZTile;
	public Tile 			m_tetriminoTypeSTile;

	private int 			m_gridSizeX;
	private int 			m_gridSizeY;

	void Update () 
	{
		//for each tile of tileMap, refresh its color depending on GameGrid's Cell TetriminoType 
		for(int i = 0; i < m_gridSizeX; i++)
		{
			for(int j = 0; j < m_gridSizeY; j++)
			{
				Tetrimino.eTetriminoType tetriType = m_gameflowManager.GetCellTetriminoType(i, j);

				Tile tile = new Tile();

				if(tetriType != Tetrimino.eTetriminoType.BLANK)
					tile = GetTetriminoTile(tetriType);
				else
					tile = i % 2 == 0 ? m_backgroundTileA : m_backgroundTileB; //if there is no Cell, put background tiles

				m_tileMap.SetTile(new Vector3Int(i, j, 0), tile);
			}
		} 
	}

	public void InitializeGrid(int _gridSizeX, int _gridSizeY)
	{
		m_gridSizeX = _gridSizeX;
		m_gridSizeY = _gridSizeY;

		for(int i = 0; i < m_gridSizeX; i++)
		{
			for(int j = 0; j < m_gridSizeY; j++)
			{
				//depending on column index we set different tile, to make columns visible
				if(i % 2 == 0)
					m_tileMap.SetTile(new Vector3Int(i, j, 0), m_backgroundTileA);
				else
					m_tileMap.SetTile(new Vector3Int(i, j, 0), m_backgroundTileB);
			}
		}

		Debug.Log("InitializeGrid()");
	}

	public void ClearGrid()
	{
		InitializeGrid(m_gridSizeX, m_gridSizeY);
	}

	public Tile GetTetriminoTile( Tetrimino.eTetriminoType _tetriminoType)
	{
		switch (_tetriminoType)
		{
		case Tetrimino.eTetriminoType.TYPE_I:
			return m_tetriminoTypeITile;
		case Tetrimino.eTetriminoType.TYPE_O:
			return m_tetriminoTypeOTile;
		case Tetrimino.eTetriminoType.TYPE_T:
			return m_tetriminoTypeTTile;
		case Tetrimino.eTetriminoType.TYPE_L:
			return m_tetriminoTypeLTile;
		case Tetrimino.eTetriminoType.TYPE_J:
			return m_tetriminoTypeJTile;
		case Tetrimino.eTetriminoType.TYPE_Z:
			return m_tetriminoTypeZTile;
		case Tetrimino.eTetriminoType.TYPE_S:
			return m_tetriminoTypeSTile;
		case Tetrimino.eTetriminoType.BLANK:
		default: return m_backgroundTileA; 
		}
	}
}
