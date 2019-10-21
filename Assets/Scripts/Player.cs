using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MovingObject {

    //public int bombDamage = 1;
    public int healthPoints = 1;
    public int bombsPlaced = 0;
    int maxBombs = 1;
    public Bomb bombPrefab;

    private Animator animator;

    // Start is called before the first frame update
    protected override void Start() {
        animator = GetComponent<Animator>();

        base.Start();
    }

    private void OnDisable() {

    }

    protected override void AttemptMove<T>(int xDir, int yDir) {
        base.AttemptMove<T>(xDir, yDir);

        CheckIfGameOver();
    }

    // change this for multiplayer, if a player dies the game doesnt necessarily ends
    private void CheckIfGameOver() {
        if (healthPoints <= 0) {
            GameManager.instance.GameOver();
        }
    }

    // Update is called once per frame
    void Update() {
        int horizontal = 0;
        int vertical = 0;

        horizontal = (int) Input.GetAxisRaw("Horizontal");
        vertical = (int) Input.GetAxisRaw("Vertical");

        // no diagonal moves
        if (horizontal != 0)
            vertical = 0;

        if (horizontal != 0 || vertical != 0)
            AttemptMove<Box>(horizontal, vertical);

        if (Input.GetKeyDown(KeyCode.X) && bombsPlaced < maxBombs) {
            PlaceBomb(transform.position);
        }
    }

    private void PlaceBomb(Vector3 position) {
        animator.SetTrigger("PlaceBomb");
        Bomb bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        bomb.setOwner(this);
        bombsPlaced++;
    }

    protected override void OnCantMove<T>(T component) { }

    private void Restart() {
        SceneManager.LoadScene(0);
    }

    public void LoseHealth(int loss) {
        healthPoints -= loss;
        CheckIfGameOver();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "explosion")
            LoseHealth(1);
        else if (other.tag == "power up") {
            other.gameObject.SetActive(false);
            // modify player properties based on item 
        }
    }
}