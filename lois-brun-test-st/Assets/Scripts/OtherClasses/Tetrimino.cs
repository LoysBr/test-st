using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tetrimino {
	
	public List<Matrix4x4> 	m_configurations;
	private int 			m_currentConfigIndex;

	public Tetrimino(int _gridSizeY)
	{
		m_configurations = new List<Matrix4x4>();

		//random Type at creation;
		int intMin = (int) eTetriminoType.TYPE_I;
		int intMax = Enum.GetNames(typeof(eTetriminoType)).Length;
		m_type = (eTetriminoType) UnityEngine.Random.Range(intMin, intMax);

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
				new Vector4( 0, 0, 0, 0), 		//to read this, turn your screen counter clockwise
				new Vector4( 0, 1, 1, 0),
				new Vector4( 0, 1, 1, 0),
				new Vector4( 0, 0, 0, 0)));
			break;
		case eTetriminoType.TYPE_S:			
			m_configurations.Add(new Matrix4x4(
				new Vector4( 0, 1, 0, 0), 		//to read this, turn your screen counter clockwise
				new Vector4( 0, 1, 1, 0),
				new Vector4( 0, 0, 1, 0),
				new Vector4( 0, 0, 0, 0)));
			m_configurations.Add(new Matrix4x4(
				new Vector4( 0, 1, 1, 0), 		//to read this, turn your screen counter clockwise
				new Vector4( 1, 1, 0, 0),
				new Vector4( 0, 0, 0, 0),
				new Vector4( 0, 0, 0, 0)));
			break;
		case eTetriminoType.TYPE_Z:			
			m_configurations.Add(new Matrix4x4(
				new Vector4( 0, 0, 1, 0), 		//to read this, turn your screen counter clockwise
				new Vector4( 0, 1, 1, 0),
				new Vector4( 0, 1, 0, 0),
				new Vector4( 0, 0, 0, 0)));
			m_configurations.Add(new Matrix4x4(
				new Vector4( 1, 1, 0, 0), 		//to read this, turn your screen counter clockwise
				new Vector4( 0, 1, 1, 0),
				new Vector4( 0, 0, 0, 0),
				new Vector4( 0, 0, 0, 0)));
			break;
		case eTetriminoType.TYPE_L:			
			m_configurations.Add(new Matrix4x4(
				new Vector4( 0, 1, 0, 0), 		//to read this, turn your screen counter clockwise
				new Vector4( 0, 1, 0, 0),
				new Vector4( 0, 1, 1, 0),
				new Vector4( 0, 0, 0, 0)));
			m_configurations.Add(new Matrix4x4(
				new Vector4( 0, 0, 1, 0), 		//to read this, turn your screen counter clockwise
				new Vector4( 1, 1, 1, 0),
				new Vector4( 0, 0, 0, 0),
				new Vector4( 0, 0, 0, 0)));
			m_configurations.Add(new Matrix4x4(
				new Vector4( 1, 1, 0, 0), 		//to read this, turn your screen counter clockwise
				new Vector4( 0, 1, 0, 0),
				new Vector4( 0, 1, 0, 0),
				new Vector4( 0, 1, 0, 0)));
			m_configurations.Add(new Matrix4x4(
				new Vector4( 0, 0, 0, 0), 		//to read this, turn your screen counter clockwise
				new Vector4( 1, 1, 1, 0),
				new Vector4( 1, 0, 0, 0),
				new Vector4( 0, 0, 0, 0)));
			break;
		case eTetriminoType.TYPE_J:			
			m_configurations.Add(new Matrix4x4(
				new Vector4( 0, 1, 1, 0), 		//to read this, turn your screen counter clockwise
				new Vector4( 0, 1, 0, 0),
				new Vector4( 0, 1, 0, 0),
				new Vector4( 0, 0, 0, 0)));
			m_configurations.Add(new Matrix4x4(
				new Vector4( 1, 0, 0, 0), 		//to read this, turn your screen counter clockwise
				new Vector4( 1, 1, 1, 0),
				new Vector4( 0, 0, 0, 0),
				new Vector4( 0, 0, 0, 0)));
			m_configurations.Add(new Matrix4x4(
				new Vector4( 0, 1, 0, 0), 		//to read this, turn your screen counter clockwise
				new Vector4( 0, 1, 0, 0),
				new Vector4( 1, 1, 0, 0),
				new Vector4( 0, 0, 0, 0)));
			m_configurations.Add(new Matrix4x4(
				new Vector4( 0, 0, 0, 0), 		//to read this, turn your screen counter clockwise
				new Vector4( 1, 1, 1, 0),
				new Vector4( 0, 0, 1, 0),
				new Vector4( 0, 0, 0, 0)));
			break;
		case eTetriminoType.TYPE_T:			
			m_configurations.Add(new Matrix4x4(
				new Vector4( 0, 1, 0, 0), 		//to read this, turn your screen counter clockwise
				new Vector4( 0, 1, 1, 0),
				new Vector4( 0, 1, 0, 0),
				new Vector4( 0, 0, 0, 0)));
			m_configurations.Add(new Matrix4x4(
				new Vector4( 0, 1, 0, 0), 		//to read this, turn your screen counter clockwise
				new Vector4( 1, 1, 1, 0),
				new Vector4( 0, 0, 0, 0),
				new Vector4( 0, 0, 0, 0)));
			m_configurations.Add(new Matrix4x4(
				new Vector4( 0, 1, 0, 0), 		//to read this, turn your screen counter clockwise
				new Vector4( 1, 1, 0, 0),
				new Vector4( 0, 1, 0, 0),
				new Vector4( 0, 0, 0, 0)));
			m_configurations.Add(new Matrix4x4(
				new Vector4( 0, 0, 0, 0), 		//to read this, turn your screen counter clockwise
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

	public void Turn()
	{		
		m_currentConfigIndex++;
		if(m_currentConfigIndex >= m_configurations.Count) 
			m_currentConfigIndex = 0;
	}

	public Matrix4x4 GetCurrentConfiguration()
	{
		return m_configurations[m_currentConfigIndex];
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
