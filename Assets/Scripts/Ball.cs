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
        _velocityHistory.Add(_rb.velocity);
    }

    private void OnDestroy()
    {
        var velocityAverage = new Vector3(
            _velocityHistory.Average(x => x.x), _velocityHistory.Average(y => y.y), _velocityHistory.Average(z => z.z)
        );

        var velocityMax = new Vector3(
                    _velocityHistory.Max(x => x.x), _velocityHistory.Max(y => y.y), _velocityHistory.Max(z => z.z)
        );

        var velocityMin = new Vector3(
                    _velocityHistory.Min(x => x.x), _velocityHistory.Min(y => y.y), _velocityHistory.Min(z => z.z)
        );

        Debug.Log($"{nameof(velocityAverage)}: {velocityAverage}, " +
            $"{nameof(velocityMax)}: {velocityMax}, " +
            $"{nameof(velocityMin)}: {velocityMin}, ");
    }

}
