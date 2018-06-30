using System.Collections;
using System.Collections.Generic;

public class GameInstance {

	public bool isPaused = false;
	public float currentTime = 0.0f;
	public int currentLevel = 1;
	public int currentLines = 0;

	public GameInstance() { }

	public void Update(float _elapsedTime)
	{
		if(isPaused) return;

		currentTime += _elapsedTime;
	}

	public void Terminate()
	{
		//delete objects ?
	}

	public void Pause()
	{
		isPaused = !isPaused;
	}
}
