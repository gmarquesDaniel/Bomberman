using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

    public int columns = 15;
    public int rows = 15;
    public GameObject floorTile;
    public GameObject wallTile;
    public GameObject box;

    private int[, ] forbiddenPositions = new int[15, 15];
    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    // void InitialiseList() {
    //     gridPositions.Clear();

    //     for (int x = 0; x < columns; x++) {
    //         for (int y = 0; x < rows; y++) {
    //             gridPositions.Add(new Vector3(x, y, 0f));
    //         }
    //     }
    // }

    private void InitialiseForbiddenPositions() {

        for (int x = 0; x < columns; x++) {
            for (int y = 0; y < rows; y++) {
                forbiddenPositions[x, y] = 0;
            }
        }

        forbiddenPositions[1, 1] = 1;
        forbiddenPositions[2, 1] = 1;
        forbiddenPositions[1, 2] = 1;
        forbiddenPositions[13, 1] = 1;
        forbiddenPositions[12, 1] = 1;
        forbiddenPositions[13, 2] = 1;
        forbiddenPositions[1, 13] = 1;
        forbiddenPositions[1, 12] = 1;
        forbiddenPositions[2, 13] = 1;
        forbiddenPositions[13, 13] = 1;
        forbiddenPositions[12, 13] = 1;
        forbiddenPositions[13, 12] = 1;
    }

    void BoardSetup() {
        boardHolder = new GameObject("Board").transform;
        GameObject toInstantiate = floorTile;

        InitialiseForbiddenPositions();

        for (int x = 0; x < columns; x++) {
            for (int y = 0; y < rows; y++) {

                Vector3 position = new Vector3(x, y, 0f);
                // filling the borders with walls         
                if ((x == 0) || (x == columns - 1) || (y == 0) || (y == rows - 1)) {
                    // Console.WriteLine("valor de x:" + (String) x + "e o valor de y:" + (String) y);
                    toInstantiate = wallTile;
                    forbiddenPositions[x, y] = 1;
                }

                // placing some walls on the map
                else if (x % 2 == 0 && y % 2 == 0 & x != 0 && x != columns - 1 && y != 0 && y != rows - 1) {
                    toInstantiate = wallTile;
                    forbiddenPositions[x, y] = 1;
                } else if (forbiddenPositions[x, y] == 0) {
                    Instantiate(box, position, Quaternion.identity);
                }
                GameObject instance = Instantiate(toInstantiate, position, Quaternion.identity) as GameObject;

                instance.transform.SetParent(boardHolder);

                toInstantiate = floorTile;
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