using System.Collections;
using System.Collections.Generic;

public class Tetrimino {

	public Tetrimino()
	{
		m_type = eTetriminoType.BLANK;
	}

	private eTetriminoType m_type;

	public enum eTetriminoType
	{
		BLANK, //used for Cell which are empty
		TYPE_I,
		TYPE_O,
		TYPE_T,
		TYPE_L,
		TYPE_J,
		TYPE_Z,
		TYPE_S,
	}

	public eTetriminoType GetTetriminoType()
	{
		return m_type;
	}
}
