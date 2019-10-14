using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

    public int columns = 15;
    public int rows = 15;
    public GameObject floorTile;
    public GameObject wallTile;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    void InitialiseList() {
        gridPositions.Clear();

        for (int x = 0; x < columns; x++) {
            for (int y = 0; x < rows; y++) {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    void BoardSetup() {
        boardHolder = new GameObject("Board").transform;
        GameObject toInstantiate = floorTile;

        for (int x = 0; x < columns; x++) {
            for (int y = 0; x < rows; y++) {

                // filling the borders with walls         
                if (x == 0 || x == columns - 1 || y == 0 || y == rows - 1) {
                    toInstantiate = wallTile;
                }

                // placing some walls on the map
                if (x % 2 == 0 && y % 2 == 0 & x != 0 && x != columns - 1 && y != 0 && y != rows - 1) {
                    toInstantiate = wallTile;
                }

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                instance.transform.SetParent(boardHolder);
            }
        }
    }

    // place boxes on the map
    //void PlaceBoxes(){}

    // function called by GameManager to prepare the scene
    public void SetupScene() {
        BoardSetup();
    }
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}