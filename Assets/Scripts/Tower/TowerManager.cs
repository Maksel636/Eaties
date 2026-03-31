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
        List<Enemy> enemies = EnemyManager.Instance.Enemies;
        Enemy closestEnemy = null;
        float closestDistance = 0;
        // Each tower finds the closest enemy and attacks it
        foreach(var tower in  _towers)
        {
            foreach(var enemy in enemies) 
            {
                float distance = Vector2.Distance (tower.transform.position, enemy.transform.position);
                if(distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }
            tower.Attack(closestEnemy);
        }
    }
}
