using System.Collections;
using System.Collections.Generic;

public class Cell {

	public Cell()
	{
		m_type = eCellType.BLANK;
		m_tetriminoType = Tetrimino.eTetriminoType.BLANK;
	}

	private eCellType 					m_type;
	private Tetrimino.eTetriminoType 	m_tetriminoType;

	public enum eCellType
	{
		BLANK,
		GRID_CELL,
		TETRIMINO_CELL
	}

	public eCellType GetCellType()
	{
		return m_type;
	}

	public Tetrimino.eTetriminoType GetCellTetriminoType()
	{
		return m_tetriminoType;
	}
}
