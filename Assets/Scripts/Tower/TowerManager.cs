using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonobehaviourSingleton<TowerManager>
{
    List<TowerBase> _towers = new List<TowerBase>();
    List<Enemy> _enemies = new List<Enemy>();

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
        _enemies = EnemyManager.Instance.Enemies;
        Enemy closestEnemy = null;
        float closestDistance = float.MaxValue;
        // Each tower finds the closest enemy and attacks it
        foreach (var tower in _towers)
        {
            foreach (var enemy in _enemies)
            {
                if (enemy.IsPickedUp) continue;
                if(enemy.GetComponentInChildren<EscapeAnimal>().IsEscaping) continue; // dont should at escaping animals

                float distance = Vector2.Distance(tower.transform.position, enemy.transform.position);
                if (distance > tower.Range) continue;

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }
            if (closestEnemy != null)
            {
                tower.Attack(closestEnemy);
            }
        }
    }


}
