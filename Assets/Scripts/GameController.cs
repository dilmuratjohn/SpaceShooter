using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public Text scoreText, restartText, gameOverText;
    public int hazardCount, enemyCount;
    public float spawnWait, startWait, waveWait;
    private int score;
    private bool gameover, restart;

    private void Start()
    {
        this.score = 0;
        this.gameover = false;
        this.restart = false;
        this.restartText.text = "";
        this.gameOverText.text = "";

        this.UpdateScore();
        StartCoroutine(this.SpawnWaves());
    }

    private void Update()
    {
        if (restart && Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene("Main");
    }

    private IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < this.hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazards[Random.Range(0, hazards.Length)], spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            if (this.gameover)
            {
                restartText.text = "Press 'R' to Restart";
                this.restart = true;
                break;
            }
        }
    }

    public void AddScore(int score)
    {
        this.score += score;
        this.UpdateScore();
    }

    public void GameOver()
    {
        this.gameover = true;
        this.gameOverText.text = "Game Over";
    }

    private void UpdateScore()
    {
        this.scoreText.text = "Score: " + score;
    }
}
