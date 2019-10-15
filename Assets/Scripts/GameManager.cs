using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public BoardManager boardScript;
    public int playerLife = 1;

    // Start is called before the first frame update
    void Awake() {

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

    void InitGame() {
        boardScript.SetupScene();
    }

    public void GameOver() {
        enabled = false;
    }

    // Update is called once per frame
    void Update() {

    }
}