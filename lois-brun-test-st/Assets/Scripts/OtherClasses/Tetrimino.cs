using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*  TESTING CUSTOM PROPERTY EDITOR
[Serializable]
public class NewTetriTypeAttribute : PropertyAttribute
{
    public int m_testInt;
    public enum eNewTetriminoType
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

    public eNewTetriminoType m_type;

    public NewTetriTypeAttribute()
    {
        m_type = eNewTetriminoType.TYPE_I;
    }
}
*/

public class Tetrimino {

    //new
    public Color            m_color;

	public List<Matrix4x4> 	m_configurations;

	private int 			m_currentConfigIndex;
	private Vector2Int 		m_position;
	private List<Vector2Int> m_previousCellsPositions;

    //CHANGE : on lui passe directement une List<Matrix4x4> !
	public Tetrimino(ref List<Tetrimino.eTetriminoType> _availableTetrimini)
	{
		m_configurations = new List<Matrix4x4>();
		m_previousCellsPositions = new List<Vector2Int>();
		m_position = new Vector2Int(-99, -99);

        //random Type at creation among _availableTetrimini list
        int intMin = 0;
        int intMax = _availableTetrimini.Count;
        int typeIndex = UnityEngine.Random.Range(intMin, intMax);
        m_type = _availableTetrimini[typeIndex];

        //int intMin = (int) eTetriminoType.TYPE_I;
		//int intMax = Enum.GetNames(typeof(eTetriminoType)).Length;
		//m_type = (eTetriminoType) UnityEngine.Random.Range(intMin, intMax);

		switch(m_type)
		{
		case eTetriminoType.TYPE_I:			
			m_configurations.Add(new Matrix4x4(
				new Vector4( 0, 1, 0, 0), 		//to read this, turn your screen  clockwise
				new Vector4( 0, 1, 0, 0),
				new Vector4( 0, 1, 0, 0),
				new Vector4( 0, 1, 0, 0)));
			m_configurations.Add(new Matrix4x4(
				new Vector4( 0, 0, 0, 0), 		
				new Vector4( 1, 1, 1, 1),
				new Vector4( 0, 0, 0, 0),
				new Vector4( 0, 0, 0, 0)));
			break;
		case eTetriminoType.TYPE_O:			
			m_configurations.Add(new Matrix4x4(
				new Vector4( 0, 0, 0, 0), 		
				new Vector4( 0, 1, 1, 0),
				new Vector4( 0, 1, 1, 0),
				new Vector4( 0, 0, 0, 0)));
			break;
		case eTetriminoType.TYPE_S:			
			m_configurations.Add(new Matrix4x4(
				new Vector4( 0, 1, 0, 0), 		
				new Vector4( 0, 1, 1, 0),
				new Vector4( 0, 0, 1, 0),
				new Vector4( 0, 0, 0, 0)));
			m_configurations.Add(new Matrix4x4(
				new Vector4( 0, 1, 1, 0), 		
				new Vector4( 1, 1, 0, 0),
				new Vector4( 0, 0, 0, 0),
				new Vector4( 0, 0, 0, 0)));
			break;
		case eTetriminoType.TYPE_Z:			
			m_configurations.Add(new Matrix4x4(
				new Vector4( 0, 0, 1, 0), 		
				new Vector4( 0, 1, 1, 0),
				new Vector4( 0, 1, 0, 0),
				new Vector4( 0, 0, 0, 0)));
			m_configurations.Add(new Matrix4x4(
				new Vector4( 1, 1, 0, 0), 		
				new Vector4( 0, 1, 1, 0),
				new Vector4( 0, 0, 0, 0),
				new Vector4( 0, 0, 0, 0)));
			break;
		case eTetriminoType.TYPE_L:			
			m_configurations.Add(new Matrix4x4(
				new Vector4( 0, 1, 0, 0), 		
				new Vector4( 0, 1, 0, 0),
				new Vector4( 0, 1, 1, 0),
				new Vector4( 0, 0, 0, 0)));
			m_configurations.Add(new Matrix4x4(
				new Vector4( 0, 0, 1, 0), 		
				new Vector4( 1, 1, 1, 0),
				new Vector4( 0, 0, 0, 0),
				new Vector4( 0, 0, 0, 0)));
			m_configurations.Add(new Matrix4x4(
				new Vector4( 1, 1, 0, 0), 		
				new Vector4( 0, 1, 0, 0),
				new Vector4( 0, 1, 0, 0),
				new Vector4( 0, 1, 0, 0)));
			m_configurations.Add(new Matrix4x4(
				new Vector4( 0, 0, 0, 0), 		
				new Vector4( 1, 1, 1, 0),
				new Vector4( 1, 0, 0, 0),
				new Vector4( 0, 0, 0, 0)));
			break;
		case eTetriminoType.TYPE_J:			
			m_configurations.Add(new Matrix4x4(
				new Vector4( 0, 1, 1, 0), 		
				new Vector4( 0, 1, 0, 0),
				new Vector4( 0, 1, 0, 0),
				new Vector4( 0, 0, 0, 0)));
			m_configurations.Add(new Matrix4x4(
				new Vector4( 1, 0, 0, 0), 		
				new Vector4( 1, 1, 1, 0),
				new Vector4( 0, 0, 0, 0),
				new Vector4( 0, 0, 0, 0)));
			m_configurations.Add(new Matrix4x4(
				new Vector4( 0, 1, 0, 0), 		
				new Vector4( 0, 1, 0, 0),
				new Vector4( 1, 1, 0, 0),
				new Vector4( 0, 0, 0, 0)));
			m_configurations.Add(new Matrix4x4(
				new Vector4( 0, 0, 0, 0), 		
				new Vector4( 1, 1, 1, 0),
				new Vector4( 0, 0, 1, 0),
				new Vector4( 0, 0, 0, 0)));
			break;
		case eTetriminoType.TYPE_T:			
			m_configurations.Add(new Matrix4x4(
				new Vector4( 0, 1, 0, 0), 		
				new Vector4( 0, 1, 1, 0),
				new Vector4( 0, 1, 0, 0),
				new Vector4( 0, 0, 0, 0)));
			m_configurations.Add(new Matrix4x4(
				new Vector4( 0, 1, 0, 0), 		
				new Vector4( 1, 1, 1, 0),
				new Vector4( 0, 0, 0, 0),
				new Vector4( 0, 0, 0, 0)));
			m_configurations.Add(new Matrix4x4(
				new Vector4( 0, 1, 0, 0), 		
				new Vector4( 1, 1, 0, 0),
				new Vector4( 0, 1, 0, 0),
				new Vector4( 0, 0, 0, 0)));
			m_configurations.Add(new Matrix4x4(
				new Vector4( 0, 0, 0, 0), 		
				new Vector4( 1, 1, 1, 0),
				new Vector4( 0, 1, 0, 0),
				new Vector4( 0, 0, 0, 0)));
			break;
		}
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

	public void SetPosition(Vector2Int _pos)
	{
		m_previousCellsPositions = GetCurrentCellsPositions();
		m_position = _pos;
	}

	public Vector2Int GetPosition()
	{
		return m_position;
	}

	public void Turn()
	{		
		m_previousCellsPositions = GetCurrentCellsPositions();

		m_currentConfigIndex++;
		if(m_currentConfigIndex >= m_configurations.Count) 
			m_currentConfigIndex = 0;
	}

	public Matrix4x4 GetCurrentConfiguration()
	{
		return m_configurations[m_currentConfigIndex];
	}	

	public Matrix4x4 GetNextConfiguration()
	{
		int nextConfigIndex = m_currentConfigIndex + 1;
		if(nextConfigIndex >= m_configurations.Count)
			nextConfigIndex = 0;
		
		return m_configurations[nextConfigIndex];
	}	

	public List<Vector2Int> GetCurrentCellsPositions()
	{
		return GetCellsPositions(m_position, GetCurrentConfiguration());
	}

	public List<Vector2Int> GetCellsPositions(Vector2Int _pos, Matrix4x4 _config)
	{
		List<Vector2Int> cells = new List<Vector2Int>();

		//let's parse every cell of tetrimono's config
		for(int x = 0; x < 4; x++)
		{
			for(int y = 0; y < 4; y++)
			{
				if(_config[x, y] == 1)
				{	
					cells.Add(new Vector2Int(x + _pos.x, (3 - y) + _pos.y));
                    //it's not  'y'  but '3 - y' because we choose to build our grid
                    //with the 0,0 bottom left, and max,max in top right, so the Y 
                    //values are inversed from the matrix reading sens
				}
			}
		}

		return cells;
	}

	public List<Vector2Int> GetPreviousCellsPositions()
	{
		return m_previousCellsPositions;
	}

	//return current lowest Y pos (0 to 4)
	public int GetBottomY()
	{
		int lowestY = 4;
		//let's parse every cell of tetrimono's config
		for(int x = 0; x < 4; x++)
		{
			for(int y = 0; y < 4; y++)
			{
				if(GetCurrentConfiguration()[x, y] == 1)
					if(y < lowestY)
						lowestY = y;
			}
		}

		return lowestY;
	}
}
