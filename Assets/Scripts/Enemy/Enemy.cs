using System;
using UnityEngine;
using UnityEngine.Rendering.UI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _startingHealth;
    private int _health;
    [SerializeField] private GameObject _meatPrefab;
    [SerializeField] private GameObject _poofParticle;
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

    Color _originalColor;
    Material _material;
    private void Awake()
    {
       _material = GetComponentInChildren<Renderer>().material;

        _originalColor = _material.color;
        _health = _startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        if(GetComponentInChildren<EscapeAnimal>().IsEscaping) return; // don't take damage when capturing
        _health -= damage;
        // FEEDBACK
        
        _material.color = Color.red;

        Invoke("ResetColor", 0.1f);

        if (_health <= 0)
        {
            Die();
        }
    }
    private void ResetColor()
    {
        GetComponentInChildren<Renderer>().material.color = _originalColor;
    }

    private void Die()
    {
        Instantiate(_poofParticle, transform.position, Quaternion.identity);

        // Spawn one meat, drop chance 1 in 3
        int randomNr = Random.Range(0, 1); // 50% chance to drop meat
        if (randomNr == 0)
        {
            Instantiate(_meatPrefab, transform.position, Quaternion.identity);
        }
        Audio.Instance.InitiateSound(Audio.SoundType.CreatureDeath);

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
    Eve,
    Jeff,
    Rau,
    Pigird,
    EveBoss,
    JeffBoss,
    RauBoss,
    PigirdBoss

}
