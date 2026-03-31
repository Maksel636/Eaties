using System.Collections;
using UnityEngine;

// Base class from which all towers inherit
public abstract class TowerBase : MonoBehaviour
{
    private int _hunger;
    private const int _maxHunger = 100;
    protected int _hungerRemoveAmount = 10;

    [SerializeField]
    private float _waitTime = 120f;

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

    public abstract void Attack();

    public IEnumerator UpdateHunger()
    {
        yield return new WaitForSeconds(_waitTime);
        Debug.Log("HUNGRY");
        _hunger -= _hungerRemoveAmount;
    }
}
