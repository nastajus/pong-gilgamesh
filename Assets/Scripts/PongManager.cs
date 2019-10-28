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

    //private Paddle _paddleWestCopy;
    //private Paddle _paddleEastCopy;
    private Ball _ball;
    private Ball _ballCopy;
    private Collider _colBall;
    private Collider _colWall1;
    private Collider _colWall2;
    private Collider _colPaddle1;
    private Collider _colPaddle2;

    private Bounds _tableBounds;
    private Bounds _scoreBoundsWest;
    private Bounds _scoreBoundsEast;


    void Awake()
    {
        //define score regions
        _colBall = GetComponentInChildren<Ball>().GetComponent<Collider>();
        _colWall1 = WallNorth.GetComponent<Collider>();
        _colWall2 = WallSouth.GetComponent<Collider>();
        _tableBounds = GetTableBounds();

        _colPaddle1 = PaddleWest.GetComponent<Collider>();
        _colPaddle2 = PaddleEast.GetComponent<Collider>();

        var scoreWestCenteredBesidePaddleWest = _tableBounds.center - new Vector3(_tableBounds.extents.x, 0) + _colPaddle1.bounds.center - new Vector3(_colPaddle1.bounds.extents.x, 0);
        var scoreEastCenteredBesidePaddleEast = _tableBounds.center + new Vector3(_tableBounds.extents.x, 0) + _colPaddle2.bounds.center + new Vector3(_colPaddle2.bounds.extents.x, 0);

        _scoreBoundsWest = new Bounds(scoreWestCenteredBesidePaddleWest, _tableBounds.size);
        _scoreBoundsEast = new Bounds(scoreEastCenteredBesidePaddleEast, _tableBounds.size);

        //save copies to re-instantiate
        _ball = GetComponentInChildren<Ball>();
        _ball.gameObject.SetActive(false);
        ResetBall();

        //PaddleWest.gameObject.SetActive(false);
        //PaddleEast.gameObject.SetActive(false);
        //ResetPaddles();

    }

    void FixedUpdate()
    {
        //score points
        if (_colBall.bounds.Intersects(_scoreBoundsEast))
        {
            Score1++;
            Debug.Log("player 1 scores!");
            Destroy(_ballCopy.gameObject);
            CheckWinner();
            ResetBall();
            ResetPaddles();
        }

        if (_colBall.bounds.Intersects(_scoreBoundsWest))
        {
            Score2++;
            Debug.Log("player 2 scores!");
            Destroy(_ballCopy.gameObject);
            CheckWinner();
            ResetBall();
            ResetPaddles();
        }

        //fix out-of-bounds
        if (!_colBall.bounds.Intersects(_tableBounds))
        {
            //no points awarded
            Debug.Log("ball went out of bounds, destroyed it. now respawning...");
            Destroy(_ballCopy.gameObject);
            ResetBall();
            ResetPaddles();
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

    void ResetBall()
    {
        _ballCopy = Instantiate<Ball>(_ball, Vector3.zero, Quaternion.identity);
        _ballCopy.gameObject.SetActive(true);
        _ballCopy.transform.parent = transform;
        _ballCopy.name = nameof(Ball);
        _colBall = _ballCopy.GetComponent<Collider>();
    }

    void ResetPaddles()
    {
        //hardcoded, replace with below
        _colPaddle1.GetComponent<Rigidbody>().velocity = Vector3.zero;
        _colPaddle2.GetComponent<Rigidbody>().velocity = Vector3.zero;
        PaddleWest.transform.position = new Vector3(-7, 0);
        PaddleEast.transform.position = new Vector3( 7, 0);


        //use this soft-coded approach instead of above,... but cyclic dependency needs resolving, as Paddle <--> PongManager's Awake... sigh.
        
        //_paddleWestCopy = Instantiate<Paddle>(PaddleWest);
        //_paddleWestCopy.gameObject.SetActive(true);
        //_paddleWestCopy.transform.parent = transform;
        //_paddleWestCopy.name = PaddleWest.name;

        //_paddleEastCopy = Instantiate<Paddle>(PaddleEast);
        //_paddleEastCopy.gameObject.SetActive(true);
        //_paddleEastCopy.transform.parent = transform;
        //_paddleEastCopy.name = PaddleEast.name;

    }

    void CheckWinner()
    {
        if (Score1 > 11)
        {
            Debug.Log("Player 1 wins!");
            Score1 = 0;
            Score2 = 0;
            ResetPaddles();
        }

        if (Score2 > 11)
        {
            Debug.Log("Player 2 wins!");
            Score1 = 0;
            Score2 = 0;
            ResetPaddles();
        }
    }
}
