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
    private float _hungerTime = 120f;
    [SerializeField]
    private float _chargeAttackTime = 10f;

    // Attack property
    bool _canAttack = true;
    public bool CanAttack
    {
        get { return _canAttack; }
        set { _canAttack = value; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TowerManager.Instance.RegisterTower(this);
        _hunger = _maxHunger;
    }

    private void OnDestroy()
    {
        TowerManager.Instance?.UnregisterTower(this);
    }

    public virtual void Attack(Enemy enemy)
    {
        StartCoroutine(ChargeAttack());
    }

    public IEnumerator UpdateHunger()
    {
        // NOT FINISHED
        yield return new WaitForSeconds(_hungerTime);
        Debug.Log("HUNGRY");
        _hunger -= _hungerRemoveAmount;
    }

    private IEnumerator ChargeAttack()
    {
        _canAttack = false;
        yield return new WaitForSeconds(_chargeAttackTime);
        _canAttack = true;
    }
}
