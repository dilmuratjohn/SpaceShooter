using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public GameObject player;
    public Vector3 spawnValues;
    public Text scoreText, gameOverText, buttonText;
    public Button startButton;
    public int hazardCount, enemyCount;
    public float spawnWait, startWait, waveWait;
    private int score;
    private bool gameover, restart;

    public void Play()
    {
        this.restart = false;
        this.gameover = false;
        this.score = 0;
        this.startButton.gameObject.SetActive(false);
        this.scoreText.gameObject.SetActive(true);
        this.gameOverText.gameObject.SetActive(false);
        this.player.gameObject.SetActive(true);
        this.UpdateScore();
        Instantiate(this.player);
        StartCoroutine(this.SpawnWaves());
    }

    private void Update()
    {
        if (restart && Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene("Main");
    }

    public void GameOver()
    {
        this.gameover = true;
        this.gameOverText.gameObject.SetActive(true);
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
                this.buttonText.text = "Restart";
                this.restart = true;
                this.startButton.gameObject.SetActive(true);
                break;
            }
        }
    }

    public void AddScore(int score)
    {
        this.score += score;
        this.UpdateScore();
    }

    private void UpdateScore()
    {
        this.scoreText.text = "Score: " + this.score;
    }
}
