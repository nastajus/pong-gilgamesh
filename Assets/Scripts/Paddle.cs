using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private Rigidbody _rb;

    public Players Player;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //if ()
        //{

        //}
    }

    public enum Players { Unset, Player1, Player2 }
}
