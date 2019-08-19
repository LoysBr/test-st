using System.Collections;
using System.Collections.Generic;

public class Cell {

	public Cell()
	{
		m_type = eCellType.EMPTY_CELL;
		m_tetriminoType = Tetrimino.eTetriminoType.BLANK;
	}

	private eCellType 					m_type;
	private Tetrimino.eTetriminoType 	m_tetriminoType;

	public enum eCellType
	{
		EMPTY_CELL,
		BUSY_CELL,
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

	public void SetCellType(eCellType _type)
	{
		 m_type = _type;
	}
	public void SetCellTetriminoType(Tetrimino.eTetriminoType _type)
	{
		m_tetriminoType = _type;
	}
}
