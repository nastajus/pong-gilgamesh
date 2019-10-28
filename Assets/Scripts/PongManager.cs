using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongManager : TSEnvironment
{

    [Header("Pong Manager")]
    public Wall WallNorth;
    public Wall WallSouth;
    public Paddle PaddleWest;
    public Paddle PaddleEast;

    [HideInInspector] public int Score1;
    [HideInInspector] public int Score2;

    //boundary logic if ball went too far... 
    //award points
    //recreate ball

    private Ball _ball;
    private Ball _ballCopy;
    private GameObject _ballGo;
    private GameObject _ballGoCopy;
    private Collider _colBall;
    private Collider _colWall1;
    private Collider _colWall2;
    private Collider _paddle1;
    private Collider _paddle2;

    private Bounds _tableBounds;
    private Bounds _scoreBoundsWest;
    private Bounds _scoreBoundsEast;


    void Awake()
    {

        _colBall = GetComponentInChildren<Ball>().GetComponent<Collider>();
        _colWall1 = WallNorth.GetComponent<Collider>();
        _colWall2 = WallSouth.GetComponent<Collider>();
        _tableBounds = GetTableBounds();

        _paddle1 = PaddleWest.GetComponent<Collider>();
        _paddle2 = PaddleEast.GetComponent<Collider>();

        var scoreWestCenteredBesidePaddleWest = _tableBounds.center - new Vector3(_tableBounds.extents.x, 0) + _paddle1.bounds.center - new Vector3(_paddle1.bounds.extents.x, 0);
        var scoreEastCenteredBesidePaddleEast = _tableBounds.center + new Vector3(_tableBounds.extents.x, 0) + _paddle2.bounds.center + new Vector3(_paddle2.bounds.extents.x, 0);

        _scoreBoundsWest = new Bounds(scoreWestCenteredBesidePaddleWest, _tableBounds.size);
        _scoreBoundsEast = new Bounds(scoreEastCenteredBesidePaddleEast, _tableBounds.size);

        _ball = GetComponentInChildren<Ball>();
        _ball.gameObject.SetActive(false);
        SpawnBall();

    }

    void FixedUpdate()
    {
        if (_colBall.bounds.Intersects(_scoreBoundsWest))
        {
            Score2++;
            Debug.Log("player 2 scores. now respawning...");
            Destroy(_ballCopy.gameObject);
            SpawnBall();
        }

        if (_colBall.bounds.Intersects(_scoreBoundsEast))
        {
            Score1++;
            Debug.Log("player 1 scores. now respawning...");
            Destroy(_ballCopy.gameObject);
            SpawnBall();
        }

        //when ball goes out of bounds, destroy it and respawn it.
        if (!_colBall.bounds.Intersects(_tableBounds))
        {
            Debug.Log("ball went out of bounds, destroyed it. now respawning...");
            Destroy(_ballCopy.gameObject);
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
        _ballCopy = Instantiate<Ball>(_ball, Vector3.zero, Quaternion.identity);
        _ballCopy.gameObject.SetActive(true);
        _ballCopy.transform.parent = transform;
        _ballCopy.name = nameof(Ball);
        _colBall = _ballCopy.GetComponent<Collider>();
    }
}
