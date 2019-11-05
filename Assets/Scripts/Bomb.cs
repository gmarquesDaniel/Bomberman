using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public int bombDamage = 1;
    public int bombRange = 1;
    public float timer = 3;
    public LayerMask blockingLayer;
    public LayerMask boxLayer;
    public GameObject explosion;
    private Player owner;

    private BoxCollider2D boxCollider;

    public void setOwner(Player player) {
        this.owner = player;
    }

    private void Explode() {
        Vector3 bombPosition = transform.position;
        // int x = (int) bombPosition.x;
        // int y = (int) bombPosition.y;    
        RaycastHit2D hit, boxHit;
        Vector3[] explosionDirections = new Vector3[4] { new Vector3(-1, 0, 0f), new Vector3(1, 0, 0f), new Vector3(0, 1, 0f), new Vector3(0, -1, 0f) };
        // blockingLayer = LayerMask.NameToLayer("BlockingLayer");

        Instantiate(explosion, bombPosition, Quaternion.identity);
        for (int i = 1; i <= bombRange; i++) {
            for (int j = 0; j < 4; j++) {
                boxCollider.enabled = false;
                hit = Physics2D.Linecast((explosionDirections[j] * (i - 1)) + bombPosition, (explosionDirections[j] * i) + bombPosition, blockingLayer);
                boxHit = Physics2D.Linecast((explosionDirections[j] * (i - 1)) + bombPosition, (explosionDirections[j] * i) + bombPosition, boxLayer);
                boxCollider.enabled = true;

                if (hit.transform == null) {
                    Instantiate(explosion, (explosionDirections[j] * i) + bombPosition, Quaternion.identity);
                }

                if (boxHit.collider) {
                    print(boxHit.collider.transform.position);
                    Box hitReciver = boxHit.collider.gameObject.GetComponent<Box>();
                    if (hitReciver != null) {
                        print("colidiu com a box");
                        hitReciver.Break();
                    }
                }
            }

        }
    }
    // Start is called before the first frame update
    void Start() {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            Explode();
            owner.bombsPlaced--;
            Destroy(gameObject);
        }
    }
}