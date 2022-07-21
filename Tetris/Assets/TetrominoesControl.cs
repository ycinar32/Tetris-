using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoesControl : MonoBehaviour
{
    public MyGameController GameController;
    public float fallTime = 0.4f;
    public float previousTime;
    Transform[] allChildren;
    public List<Transform> Children;
    private bool invalidRightMove = false;
    private bool invalidLeftMove = false;
    private bool invalidVerticalMove = false;
    public bool isSet;
    public bool firstTetromino;

    void Start()
    {
        GameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MyGameController>();
        allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if (child != allChildren[0])
            {
                Children.Add(child);
            }
        }

        previousTime = Time.time;
    }

    private void FixedUpdate()
    {
        foreach (Transform child in Children)
        {
            if (child.position.x  >= 9)
            {
                invalidRightMove = true;
                break;
            }
            else invalidRightMove = false;

            if (child.position.x  <= 0)
            {
                invalidLeftMove = true;
                break;
            }
            else invalidLeftMove = false;

        }
        foreach (Transform child in Children)
        {
            if (child.position.y <= -23)
            {
                invalidVerticalMove = true;
                isSet = true;
                break;
            }
            else invalidVerticalMove = false;
        }
    }

    void Update()
    {
        MovementControl();
        GridControl();
    }

    void GridControl()
    {

    }
    void MovementControl()
    {
        if (!isSet)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && !invalidRightMove)
            {
                if(GameController.CollisionControl(Children, "right"))
                {
                    transform.position = ((transform.position + new Vector3(1f, 0f, 0f)));
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && !invalidLeftMove)
            {
                if(GameController.CollisionControl(Children, "left"))
                {
                    transform.position = ((transform.position + new Vector3(-1f, 0f, 0f)));
                }
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.Rotate(new Vector3(0f,0f,90f));
            }
            if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime) && !invalidVerticalMove)
            {
                if(GameController.CollisionControl(Children, "down"))
                {
                    transform.position = ((transform.position + new Vector3(0f, -1f, 0f)));
                    previousTime = Time.time;
                }
                else
                {
                    invalidVerticalMove = true;
                    isSet = true;
                }
            }


        }
        else
        {
            GameController.UpdateGrid(Children);
            this.GetComponent<TetrominoesControl>().enabled = false;
        }
    }
}
