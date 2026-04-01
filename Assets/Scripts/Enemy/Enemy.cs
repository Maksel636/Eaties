using System;
using UnityEngine;
using UnityEngine.Rendering.UI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _startingHealth;
    private int _health;
    [SerializeField] private GameObject _meatPrefab;
    [SerializeField] private EnemyType _type;
    public EnemyType type => _type;
    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }

    private bool _isPickedUp = false;
    public bool IsPickedUp
    {
        get => _isPickedUp;
        set
        {
            _isPickedUp = value;
            _movement.enabled = !value;
            //_movement.GoBackToPath();
        }
    }

    [SerializeField] private EnemyMovement _movement;
    public float MoveSpeed => _movement.MoveSpeed;

    private void Awake()
    {
        _health = _startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Spawn one meat, drop chance 1 in 3
        int randomNr = Random.Range(0, 2);
        if (randomNr == 0)
        {
            Instantiate(_meatPrefab, transform.position, Quaternion.identity);
        }

        EnemyManager.Instance.EnemiesAlive--;
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        EnemyManager.Instance?.UnRegisterEnemy(this);
    }

    public Vector3 GetPredictedPosition(float futureTime)
    {
        return _movement.GetPredictedPosition(futureTime);
    }
}

public enum EnemyType
{
    Basic,
    Piercing,
    Octopus
}
