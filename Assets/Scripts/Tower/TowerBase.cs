using System.Collections;
using UnityEngine;

// Base class from which all towers inherit
public class TowerBase : MonoBehaviour
{
    // Hunger
    private int _hunger;
    private const int _maxHunger = 100;
    protected int _hungerRemoveAmount = 10;

    // Timers
    [SerializeField]
    private float _hungerTime = 10f;
    [SerializeField]
    private float _chargeAttackTime = 1f;

    // Attack property
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
    }

    public virtual void Attack(Enemy enemy)
    {
        if(_canAttack)
            StartCoroutine(ChargeAttack());

        // Set can attack to false in the inherrited class
    }

    public void UpdateHunger()
    {
        // NOT FINISHED
        Debug.Log("HUNGRY");
        _hunger -= _hungerRemoveAmount;
    }

    private IEnumerator ChargeAttack()
    {
        yield return new WaitForSeconds(_chargeAttackTime);
        _canAttack = true;
    }
}
