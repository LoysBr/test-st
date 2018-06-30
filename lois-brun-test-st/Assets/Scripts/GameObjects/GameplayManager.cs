using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour {

	public MenuManager menuManager;
	private GameInstance gameInstance;

	public void ClearCurrentGame()
	{
		if(gameInstance != null)
			gameInstance.Terminate();

		gameInstance = null;

		Debug.Log("ClearCurrentGame()");
	}
	public void StartNewGame()
	{
		gameInstance = new GameInstance();

		Debug.Log("StartNewGame ()");
	}
	public void PauseGame()
	{
		if(gameInstance != null)
		{
			gameInstance.Pause();
		}
	}
	public void SaveGame()
	{
		Debug.Log("Save Game ()");
	}

	void Update () {
		if(Input.GetKeyUp(KeyCode.Escape))
			OnInput_Escape();

		if(gameInstance == null)
			return;

		gameInstance.Update(Time.deltaTime);

		if(Input.GetKeyDown(KeyCode.UpArrow))
			OnInput_Up();
		if(Input.GetKeyDown(KeyCode.LeftArrow))
			OnInput_Left();
		if(Input.GetKeyDown(KeyCode.RightArrow))
			OnInput_Right();
		if(Input.GetKeyDown(KeyCode.DownArrow))
			OnInput_Down();
	}

	public void OnInput_Up()
	{
	}
	public void OnInput_Left()
	{
	}
	public void OnInput_Right()
	{
	}
	public void OnInput_Down()
	{
	}
	public void OnInput_Escape()
	{
		menuManager.Quit();
	}


}
