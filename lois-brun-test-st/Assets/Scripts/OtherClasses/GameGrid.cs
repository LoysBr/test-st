using System.Collections;
using System.Collections.Generic;

public class GameGrid  {

	private Cell[,] m_gridTab;

	public GameGrid(int _gridSizeX, int _gridSizeY)
	{
		m_gridTab = new Cell[_gridSizeX, _gridSizeY];
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
}
