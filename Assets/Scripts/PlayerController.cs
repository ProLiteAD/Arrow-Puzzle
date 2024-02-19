using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator theAnim;
    SpriteRenderer theSr;
    [SerializeField] List<Direction> nextMove;
    [SerializeField] int moveNumber = 0;
    [SerializeField] bool onArrow;
    [SerializeField] bool moveEnd = true;
    [SerializeField] bool onGo = false;
    [SerializeField] float moveTimer = 0f;
    float maxMoveTime = 1f;
    [SerializeField] float lerpTimer = 0f;
    [SerializeField] Vector3 currentPos, nextPos;
    [SerializeField] BoxCollider2D boxCol;


    void Start()
    {
        theAnim = transform.GetComponent<Animator>();
        theSr = transform.GetComponent<SpriteRenderer>();
        currentPos = transform.position;
        nextPos = transform.position;
        boxCol = GetComponent<BoxCollider2D>();
    }

    void TempControls()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            MovePlayer(Direction.Right);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            MovePlayer(Direction.Down);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            MovePlayer(Direction.Left);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            MovePlayer(Direction.Up);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            boxCol.enabled = true;
            onGo = true;
        }
        if (!onGo) { return; }


        if (moveEnd && onArrow)
        {
            if(moveTimer > maxMoveTime)
            {
                currentPos = transform.position;
            }

            //TempControls();
            CheckArrow();
        }
        else
        {
            if(moveTimer < maxMoveTime)
            {
                theAnim.SetBool("isIdle", false);
                moveTimer += Time.deltaTime;
                transform.position = Vector3.Lerp(currentPos, nextPos, moveTimer);

            }
            else
            {
                theAnim.SetBool("isIdle", true);
                moveEnd = true;
                moveTimer = 0f;
            }
        }
    }

    void CheckArrow()
    {
        if (onArrow)
        {
            try
            {
                MovePlayer(nextMove[moveNumber]);
            }
            catch { onArrow = false; }
        }
    }

    void MovePlayer(Direction newMove)
    {
        Vector3 newDir;
        switch (newMove)
        {
            case Direction.Right:
                newDir = new Vector3(2, 0);
                theSr.flipX = false;
                break;
            case Direction.Down:
                newDir = new Vector3(0, -2);
                break;
            case Direction.Left:
                newDir = new Vector3(-2, 0);
                theSr.flipX = true;
                break;
            case Direction.Up:
                newDir = new Vector3(0, 2);
                break;
            default:
                newDir = transform.position;
                break;
        }

        currentPos = transform.position;
        nextPos = transform.position + newDir;
        moveEnd = false;
        //transform.position += newDir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Arrow")
        {
            GameObject obj = collision.gameObject;
            ArrowController objArrow = obj.GetComponent<ArrowController>();
            //nextMove = objArrow.GetDirecttion();
            nextMove.Add(objArrow.GetDirecttion());
            onArrow = true;
            print("IN: " + objArrow.GetDirecttion());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Arrow")
        {
            GameObject obj = collision.gameObject;
            //onArrow = false;
            Destroy(obj);
            print("OUT");

            moveNumber++;
        }
    }
}
