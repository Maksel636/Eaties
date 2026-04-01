using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    public float MoveSpeed => _moveSpeed;

    private Transform _target;
    private int _waypointIndex = 0;

    private const float MOVEMENT_EPSILON = 0.3f;

    [SerializeField] private float _maxPathOffset;
    private Vector3 _pathOffset;
    private Vector3 _targetPos;

    private Vector3 _lastPathPos;

    private Vector3 _direction = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float offsetX = Random.Range(-_maxPathOffset, _maxPathOffset);
        float offsetZ = Random.Range(-_maxPathOffset, _maxPathOffset);
        _pathOffset = new Vector3(offsetX, 0f, offsetZ);

        _target = Path.Instance.Waypoints[_waypointIndex];
        _targetPos = _target.position + _pathOffset;

        transform.position += _pathOffset;

        _direction = (_targetPos - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        _direction = (_targetPos - transform.position).normalized;
        transform.Translate(_direction * (_moveSpeed * Time.deltaTime));

        if (Vector3.Distance(transform.position, _targetPos) <= MOVEMENT_EPSILON)
        {
            GetNextWaypoint();
        }

        _lastPathPos = transform.position;
    }

    private void GetNextWaypoint()
    {
        if ((_waypointIndex + 1) >= Path.Instance.Waypoints.Length)
        {
            GameHealth.Instance.TakeDamage();
            Destroy(gameObject);
            return;
        }

        _waypointIndex++;
        _target = Path.Instance.Waypoints[_waypointIndex];
        _targetPos = _target.transform.position + _pathOffset;
    }


    public void GoBackToPath()
    {
        transform.position = _lastPathPos;
    }

    public Vector3 GetPredictedPosition(float futureTime)
    {
        Vector3 predictedPos = transform.position + _direction * (_moveSpeed * futureTime);
        return predictedPos;
    }
}
