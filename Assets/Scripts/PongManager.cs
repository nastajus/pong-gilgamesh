using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongManager : TSEnvironment
{

    [Header("Pong Manager")]
    public Wall WallNorth;
    public Wall WallSouth;

    //boundary logic if ball went too far... 
    //award points
    //recreate ball

    private Ball _ball;
    private Ball _ballCopy;
    private Collider _colBall;
    private Collider _colWall1;
    private Collider _colWall2;

    private Bounds _tableBounds;

    void Awake()
    {
        _ball = GetComponentInChildren<Ball>();
        _ballCopy = _ball; //clone? eh..
        _colBall = GetComponentInChildren<Ball>().GetComponent<Collider>();
        _colWall1 = WallNorth.GetComponent<Collider>();
        _colWall2 = WallSouth.GetComponent<Collider>();
        _tableBounds = GetTableBounds();

    }

    void FixedUpdate()
    {
        //when ball goes out of bounds, destroy it and respawn it.
        if (!_colBall.bounds.Intersects(_tableBounds))
        {
            Destroy(_colBall.gameObject);
            Debug.Log("ball went out of bounds, destroyed it. now respawning...");

            SpawnBall();

        }

    }

    Bounds GetTableBounds()
    {
        Collider[] colliders = new Collider[2];
        colliders[0] = _colWall1;
        colliders[1] = _colWall2;

        var tableBounds = new Bounds(Vector3.zero, Vector3.zero);

        foreach (Collider collider in colliders)
        {
            if (tableBounds.extents == Vector3.zero)
            {
                tableBounds = collider.bounds;
            }
            tableBounds.Encapsulate(collider.bounds);
        }

        return tableBounds;
    }

    void SpawnBall()
    {
        GameObject newBall = Instantiate<GameObject>(_ballCopy.gameObject, Vector3.zero, Quaternion.identity);
        newBall.transform.parent = transform.parent;
        _colBall = newBall.GetComponent<Collider>();
    }
}
