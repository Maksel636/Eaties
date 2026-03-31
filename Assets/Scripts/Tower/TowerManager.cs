using NUnit.Framework;
using PD4.Singleton; // NAMESPACE WEGHALEN
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonobehaviourSingleton<TowerManager>
{
    List<TowerBase> _towers = new List<TowerBase>();

    public void RegisterTower(TowerBase tower)
    {
        _towers.Add(tower);
    }

    public void UnregisterTower(TowerBase tower)
    {
        _towers.Remove(tower);
    }

    private void Update()
    {
        HandleAttack();
        HandleHunger();
    }

    private void HandleAttack()
    {
        foreach(var tower in  _towers)
        {
            tower.Attack();
        }
    }

    private void HandleHunger()
    {
        foreach(var tower in _towers)
        {
            tower.UpdateHunger();
            // TODO: update hunger bar event
        }
    }
}
