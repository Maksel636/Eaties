using UnityEngine;
using UnityEngine.Rendering.UI;

public class Enemy : MonoBehaviour
{
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
            if (value == true) _movement.enabled = false;
        }
    }

    [SerializeField] private EnemyMovement _movement;


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        EnemyManager.Instance.UnRegisterEnemy(this);
    }
}
