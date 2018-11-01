using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public int score;
    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
            this.gameController = gameControllerObject.GetComponent<GameController>();
        if (this.gameController == null)
            Debug.Log("Can not Find 'GameController' script.");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary") return;
        Instantiate(explosion, transform.position, transform.rotation);
        if (other.tag == "Player"){
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            this.gameController.GameOver();
        }
        Destroy(other.gameObject);
        Destroy(gameObject);
        this.gameController.AddScore(this.score);
    }
}
