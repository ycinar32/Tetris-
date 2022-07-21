using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameController : MonoBehaviour
{
    [SerializeField] GameObject[] Tetronimoes;
    enum Tetromino
    {
        BLANK,
        I_Tetromino,
        J_Tetromino,
        L_Tetromino,
        O_Tetromino,
        S_Tetromino,
        T_Tetromino,
        Z_Tetromino
    };

    public GameObject CurrentGameObject;

    public bool[,] gridArray;
    private const int gridSizeX = 10;
    private const int gridSizeY = 24;


    void Start()
    {
        AllocateGridArray();
        CurrentGameObject = Instantiate(Tetronimoes[Random.Range(0, 6)], this.transform);
        CurrentGameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentGameObject.GetComponent<TetrominoesControl>().isSet)
        {
            CurrentGameObject = Instantiate(Tetronimoes[Random.Range(0, 6)], this.transform);
            CurrentGameObject.SetActive(true);
        }
    }

    public void AllocateGridArray()
    {
        Debug.Log("ALLOCATING");
        gridArray = new bool[10, 24];
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                gridArray[x, y] = false;
            }
        }
    }
    public void UpdateGrid(List<Transform> Children)
    {
        foreach (Transform child in Children)
        {
            int xDim = ((int)child.position.x);
            int yDim = (-1 * ((int)child.position.y));
            gridArray[xDim, yDim] = true;
        }
    }

    public bool CollisionControl(List<Transform> Children, string direction)
    {

        foreach (Transform child in Children)
        {
            int xDim = ((int)child.position.x);
            int yDim = (-1 * ((int)child.position.y));
            if (direction == "right")
            {
                if (gridArray[xDim + 1, yDim])
                {
                    Debug.Log("Left Movement Not Available");
                    return false;
                }
            }

            if (direction == "left")
            {
                if (gridArray[xDim - 1, yDim])
                {
                    Debug.Log("Left Movement Not Available");
                    return false;
                }
            }

            if(direction == "down")
            {
                if(gridArray[xDim, yDim+1])
                {
                    Debug.Log("TETROMINO IS SET.");
                    return false;
                }
            }
        }
        return true;
    }

}
