using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float Speed = 5;

    private Rigidbody _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        //var moveBy = MoveRandomly() * Speed; //* Time.deltaTime;
        //_rb.AddForce(moveBy);
        _rb.velocity = Random.insideUnitCircle * Speed; //already normalized
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //_rb.velocity = _rb.velocity.normalized * Speed; //normalizes into a unit vector (respects orientation/ resets magnitude
    }

    Vector3 MoveRandomly()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        return new Vector3(x, y);
    }
}
