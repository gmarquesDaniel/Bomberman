using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public float timer = 0.7f;

    // Start is called before the first frame update
    void Start() {

    }

    // explosion collided with something
    void OnTriggerEnter2D(Collider2D col) {
        //hit box
        if (col.gameObject.layer == LayerMask.NameToLayer("BoxLayer")) {
            Box box = col.GetComponent<Box>();
            box.Break();
            // hit player
        } else if (col.gameObject.layer == LayerMask.NameToLayer("PlayerLayer")) {
            Player player = col.GetComponent<Player>();
            player.LoseHealth(1);
            // hit item
        } else if (col.gameObject.layer == LayerMask.NameToLayer("ItemLayer")) {
            // Item item = col.GetComponent<Item>();
            // Destroy(item);
        }
    }

    // Update is called once per frame
    void Update() {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            Destroy(gameObject);
        }

    }
}