using UnityEngine;


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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float offsetX = Random.Range(-_maxPathOffset, _maxPathOffset);
        float offsetZ = Random.Range(-_maxPathOffset, _maxPathOffset);
        _pathOffset = new Vector3(offsetX, 0f, offsetZ);

        _target = Path.Instance.Waypoints[_waypointIndex];
        _targetPos = _target.position + _pathOffset;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (_targetPos - transform.position).normalized;
        transform.Translate(direction * (_moveSpeed * Time.deltaTime));

        if (Vector3.Distance(transform.position, _targetPos) <= MOVEMENT_EPSILON)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        if ((_waypointIndex + 1) >= Path.Instance.Waypoints.Length)
        {
            Debug.Log("End of path");
            GameHealth.Instance.TakeDamage();
            Destroy(gameObject);
            return;
        }

        _waypointIndex++;
        _target = Path.Instance.Waypoints[_waypointIndex];
        _targetPos = _target.transform.position + _pathOffset;
    }
}
