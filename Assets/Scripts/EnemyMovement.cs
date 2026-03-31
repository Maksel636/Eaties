using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private Transform _target;
    private int _waypointIndex = 0;

    private const float MOVEMENT_EPSILON = 0.3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _target = Path.Instance.Waypoints[_waypointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        transform.Translate(direction * (_moveSpeed * Time.deltaTime));

        if (Vector3.Distance(transform.position, _target.position) <= MOVEMENT_EPSILON)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        if ((_waypointIndex + 1) >= Path.Instance.Waypoints.Length)
        {
            Debug.Log("End of path");
            Destroy(gameObject);
            return;
        }

        _waypointIndex++;
        _target = Path.Instance.Waypoints[_waypointIndex];
    }
}
