using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public Players Player;
    public KeyCode Up;
    public KeyCode Down;

    public float Speed;


    private Rigidbody _rb;
    private Collider _colPaddle;


    private PongManager _manager;
    private Collider _colWall1;
    private Collider _colWall2;


    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _colPaddle = GetComponent<Collider>();

        _manager = transform.parent.GetComponent<PongManager>();
        _colWall1 = _manager.WallNorth.GetComponent<Collider>();
        _colWall2 = _manager.WallSouth.GetComponent<Collider>();
    }

    void FixedUpdate()
    {
        float moveDir = Move();
        Vector3 moveDestMaybe = transform.position + new Vector3(0, moveDir * Speed);

        var northWallBottom = _colWall1.bounds.center.y - _colWall1.bounds.extents.y;
        var southWallTop = _colWall2.bounds.center.y + _colWall2.bounds.extents.y;

        var paddleTop = _colPaddle.bounds.center.y - _colPaddle.bounds.extents.y;
        var paddleBottom = _colPaddle.bounds.center.y + _colPaddle.bounds.extents.y;

        //TODO: fix logic error, gets stuck..
        //if (moveDir > 0 && paddleTop + moveDestMaybe.y < northWallBottom  ||
        //    moveDir < 0 && paddleBottom + moveDestMaybe.y < southWallTop) 
        {
            _rb.MovePosition(moveDestMaybe);
        }
        //Debug.Log($"md: {moveDir}, nwb: {northWallBottom}, pt: {paddleTop}, mdm: {moveDestMaybe.y}");

    }

    private float Move()
    {
        float moveDir;
        if (Input.GetKey(Up) && Input.GetKey(Down))
        {
            moveDir = 0;
        }
        else if (Input.GetKey(Up))
        {
            moveDir = 1;
        }
        else if (Input.GetKey(Down))
        {
            moveDir = -1;
        }
        else
        {
            moveDir = 0;
        }
        return moveDir;
    }
}
public enum Players { Unset, Player1, Player2 }

