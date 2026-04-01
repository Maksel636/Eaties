using System;
using System.Collections;
using UnityEngine;

// Base class from which all towers inherit
public class TowerBase : MonoBehaviour
{
    // Hunger
    private int _hunger;
    private const int _maxHunger = 100;
    protected int _hungerRemoveAmount = 10;
    public EventHandler<HungerArgs> _onHungerChanged;
    [SerializeField]
    private float _hungerTime = 30f;


    // Attack
    [SerializeField]
    private float _chargeAttackTime = 1f;
    protected bool _canAttack = true;
    public bool CanAttack
    {
        get { return _canAttack; }
        set { _canAttack = value; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        TowerManager.Instance.RegisterTower(this);
        _hunger = _maxHunger;
    }

    private void OnDestroy()
    {
        TowerManager.Instance?.UnregisterTower(this);
    }

    private void Start()
    {
        InvokeRepeating(nameof(UpdateHunger), _hungerTime, _hungerTime);
        _onHungerChanged?.Invoke(this, new HungerArgs(_hunger));
    }

    public virtual void Attack(Enemy enemy)
    {
        if(_canAttack)
            StartCoroutine(ChargeAttack());

        // Set can attack to false in the inherrited class
    }
    

    public void UpdateHunger()
    {
        _hunger -= _hungerRemoveAmount;

        if (_hunger <= 0)
        {
            Destroy(gameObject);
            return;
        }
        
        _onHungerChanged?.Invoke(this, new HungerArgs(_hunger));
    }

    private IEnumerator ChargeAttack()
    {
        yield return new WaitForSeconds(_chargeAttackTime);
        _canAttack = true;
    }
}

public class HungerArgs : EventArgs
{
    public int _hunger { get; }
    public HungerArgs(int hunger)
    {
        _hunger = hunger;
    }
}