using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Right,
    Down,
    Left,
    Up
}

public class ArrowController : MonoBehaviour
{
    [SerializeField] bool changedOnce = false;
    [SerializeField] Direction currentDirection;
    [SerializeField] Vector2 orginialPos;
    public Vector2 nextPos;
    public bool doChange = false;
    public bool mouseChange = false;
    [SerializeField] SpriteRenderer theSr;
    [SerializeField] GameObject otherArrow;

    void Start()
    {
        theSr = GetComponent<SpriteRenderer>();
        SetDirection(currentDirection);
        orginialPos = transform.position;
        nextPos = orginialPos;
    }

    void Update()
    {
        if (changedOnce)
        {
            theSr.color = Color.red;
        }
        else
        {
            theSr.color = Color.green;
            if (doChange)
            {
                changedOnce = true;
                transform.position = nextPos;
                if (otherArrow)
                {
                    otherArrow.GetComponent<ArrowController>().doChange = true;
                }
            }
        }
    }

    public void SetDirection(Direction newDir)
    {
        currentDirection = newDir;
        int directionVal;

        switch (newDir)
        {
            case Direction.Right:
                directionVal = 0;
                break;
            case Direction.Down:
                directionVal = -90;
                break;
            case Direction.Left:
                directionVal = 180;
                break;
            case Direction.Up:
                directionVal = 90;
                break;
            default:
                directionVal = 0;
                break;
        }

        transform.eulerAngles = new Vector3(
                transform.eulerAngles.x,
                transform.eulerAngles.y,
                transform.eulerAngles.z + directionVal
            );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Arrow" && mouseChange)
        {
            otherArrow = collision.gameObject;
            ArrowController colArrow = otherArrow.GetComponent<ArrowController>();
            if (colArrow.changedOnce) { return; }

            colArrow.nextPos = orginialPos;
            nextPos = collision.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision == otherArrow)
        {
            otherArrow = null;
            nextPos = orginialPos;
        }
    }

    public Direction GetDirecttion()
    {
        return currentDirection;
    }

    public bool GetChangeOnce()
    {
        return changedOnce;
    }
}
