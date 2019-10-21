using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {
    public int healthPoints = 1;
    //public PowerUp = null;

    public void Break() {
        // SummonItem();
        Destroy(this.gameObject);
    }
}