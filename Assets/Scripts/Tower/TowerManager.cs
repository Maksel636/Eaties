using NUnit.Framework;
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
    }

    private void HandleAttack()
    {
        foreach(var tower in  _towers)
        {
            tower.Attack();
        }
    }
}
