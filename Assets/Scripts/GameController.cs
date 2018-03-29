using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public GUIText scoreText, restartText, gameOverText;

	private bool restart, gameOver;
	private int score;


	void Start()
	{
		restart = false;
		gameOver = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		updateScore ();
		StartCoroutine (SpawnWaves ());
	}

	void Update()
	{
		if (restart) {	
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}

	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true) 
		{
			for(int i = 0; i < hazardCount; i++)
			{
				Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate(hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if (gameOver) 
			{
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}
	}

	public void Addscore(int newScoreValue)
	{
		score += newScoreValue;
		updateScore();	
	}

	void updateScore ()
	{
		scoreText.text = "Score: " + score;
	}

	public void GameOver()
	{
		gameOverText.text = "GAME OVER";
		gameOver = true;
	}

}
