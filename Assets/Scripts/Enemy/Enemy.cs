using UnityEngine;
using UnityEngine.Rendering.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _startingHealth;
    private int _health;
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
            _movement.GoBackToPath();
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
        EnemyManager.Instance.EnemiesAlive--;
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        EnemyManager.Instance?.UnRegisterEnemy(this);
    }
}
