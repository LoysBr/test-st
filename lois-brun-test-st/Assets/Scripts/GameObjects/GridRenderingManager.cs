using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridRenderingManager : MonoBehaviour {

	public Tilemap tileMap;
	public Tile tile1;
	// Use this for initialization
	void Start () {
		tileMap.SetTile(new Vector3Int(10,2,0), tile1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
