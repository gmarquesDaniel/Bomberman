using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public BoardManager boardScript;
    public int playerLife = 1;

    public GameObject gameOver;

    // Start is called before the first frame update
    void Awake() {
        print("called awake");
        // there can only be one GameManager
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
        InitGame();
    }

    public void InitGame() {
        boardScript.SetupScene();
    }

    void RestartScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GameOver() {
        enabled = false;
        Instantiate(gameOver);
        Invoke("RestartScene", 5);

        //InitGame();
    }

    // Update is called once per frame
    void Update() {

    }
}