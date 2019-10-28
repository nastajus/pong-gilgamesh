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
    private Mesh _meshPaddle;


    private PongManager _manager;
    private Collider _colWall1;
    private Collider _colWall2;

    private float northWallBottom;
    private float southWallTop;


    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _colPaddle = GetComponent<Collider>();
        _meshPaddle = GetComponent<MeshFilter>().mesh;

        _manager = transform.parent.GetComponent<PongManager>();
        _colWall1 = _manager.WallNorth.GetComponent<Collider>();
        _colWall2 = _manager.WallSouth.GetComponent<Collider>();

        northWallBottom = _colWall1.bounds.center.y - _colWall1.bounds.extents.y;
        southWallTop = _colWall2.bounds.center.y + _colWall2.bounds.extents.y;
    }


    GameObject cube2;
    GameObject cube3;

    void FixedUpdate()
    {
        if (Player == Players.Player2) return;

        float moveDir = Move();
        
        if (moveDir == 0) return;
        Vector3 moveDestMaybe = transform.position + new Vector3(0, moveDir * Speed);

        //var paddleTop = _colPaddle.bounds.center.y - _colPaddle.bounds.extents.y;
        //var paddleBottom = _colPaddle.bounds.center.y + _colPaddle.bounds.extents.y;

        var paddleTop = _meshPaddle.bounds.center.y - _meshPaddle.bounds.extents.y;
        var paddleBottom = _meshPaddle.bounds.center.y + _meshPaddle.bounds.extents.y;
        //The renderer.bounds values are given in world space, which is why it changes as the object moves. If the need the bounds in the object's local space, you should use Mesh.bounds.



        //debug code
        Destroy(cube2);
            cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            var rb2 = cube2.AddComponent<Rigidbody>();
            rb2.MovePosition(transform.position + new Vector3(0, paddleTop, -1));
            cube2.transform.localScale = new Vector3(1, 0.1f, 1);

            Destroy(cube3);
            cube3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            var rb3 = cube3.AddComponent<Rigidbody>();
            rb3.MovePosition(transform.position + new Vector3(0, paddleBottom, -1));
            cube3.transform.localScale = new Vector3(1, 0.1f, 1);

        _rb.MovePosition(moveDestMaybe);

        //TODO: fix logic error, gets stuck..

        //if (moveDir > 0 && paddleTop + moveDestMaybe.y < northWallBottom  ||
        //    moveDir < 0 && paddleBottom + moveDestMaybe.y < southWallTop) 

        //if (!(moveDir > 0 && paddleTop + moveDestMaybe.y < northWallBottom))
        //{
        //    _rb.MovePosition(moveDestMaybe);
        //}
        //if (!(moveDir < 0 && paddleBottom + moveDestMaybe.y < southWallTop))
        //{
        //    _rb.MovePosition(moveDestMaybe);
        //}



        //Debug.Log($"player: {Player}  md: {moveDir}, pt: {paddleTop}, mdm: {moveDestMaybe.y}, nwb: {northWallBottom}");

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

