using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float Speed = 5;

    private Rigidbody _rb;

    private List<Vector3> _velocityHistory = new List<Vector3>();

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = Random.insideUnitCircle * Speed; //already normalized

        //experimental speed improvements, semi-chaotic... 
        //if (_rb.velocity.x < 0.25f)
        //{
        //    _rb.velocity = new Vector3(_rb.velocity.x * 4, _rb.velocity.y);
        //}

        //when it's too sluggish to kick it to go faster
        if (_rb.velocity.x < 0.5f)
        {
            _rb.velocity = new Vector3(_rb.velocity.x * 2, _rb.velocity.y);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        VelocityStat();
    }

    void VelocityStat()
    {
        var magnitudeOnly = new Vector3(Mathf.Abs(_rb.velocity.x), Mathf.Abs(_rb.velocity.y), Mathf.Abs(_rb.velocity.z));
        _velocityHistory.Add(magnitudeOnly);
    }

    private void OnDestroy()
    {
        var velocityAverage = new Vector2( _velocityHistory.Average(x => x.x), _velocityHistory.Average(y => y.y) );

        var velocityMax = new Vector2( _velocityHistory.Max(x => x.x), _velocityHistory.Max(y => y.y) );

        var velocityMin = new Vector2( _velocityHistory.Min(x => x.x), _velocityHistory.Min(y => y.y) );

        Debug.Log($"{nameof(velocityAverage)}: {velocityAverage}, " +
            $"{nameof(velocityMax)}: {velocityMax}, " +
            $"{nameof(velocityMin)}: {velocityMin}, ");
    }

}
