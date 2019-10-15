using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MovingObject {

    public int bombDamage = 1;
    public int healthPoints = 1;

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

        RaycastHit2D hit;

        CheckIfGameOver();
    }

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
    }

    protected override void OnCantMove<T>(T component) { }

    private void Restart() {
        SceneManager.LoadScene(0);
    }

    private void LoseHealth(int loss) {
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