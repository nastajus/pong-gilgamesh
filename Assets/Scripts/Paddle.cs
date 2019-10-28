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
    private Mesh _meshPaddle;


    private PongManager _manager;
    private Collider _colWall1;
    private Collider _colWall2;

    private float northWallBottom;
    private float southWallTop;


    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _meshPaddle = GetComponent<MeshFilter>().mesh;

        _manager = transform.parent.GetComponent<PongManager>();
        _colWall1 = _manager.WallNorth.GetComponent<Collider>();
        _colWall2 = _manager.WallSouth.GetComponent<Collider>();

        northWallBottom = _colWall1.bounds.center.y - _colWall1.bounds.extents.y;
        southWallTop = _colWall2.bounds.center.y + _colWall2.bounds.extents.y;

    }

    void FixedUpdate()
    {
        float moveDir = Move();
        if (moveDir == 0) return;

        Vector3 moveDestMaybe = transform.position + new Vector3(0, moveDir * Speed);

        var paddleTop = _meshPaddle.bounds.center.y + _meshPaddle.bounds.extents.y;
        var paddleBottom = _meshPaddle.bounds.center.y - _meshPaddle.bounds.extents.y;

        if (
                ((moveDir > 0) && !(paddleTop + moveDestMaybe.y > northWallBottom)) ||
                ((moveDir < 0) && !(paddleBottom + moveDestMaybe.y < southWallTop))
            )
        {
            _rb.MovePosition(moveDestMaybe);
        }
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

