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
    private PongManager _manager;

    private Collider _col1;
    private Collider _col2;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _manager = transform.parent.GetComponent<PongManager>();
        _col1 = _manager.WallNorth.GetComponent<Collider>();
        _col2 = _manager.WallSouth.GetComponent<Collider>();

    }

    void FixedUpdate()
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
        _rb.MovePosition(transform.position + new Vector3(0, moveDir * Speed));

    }

    public enum Players { Unset, Player1, Player2 }
}
