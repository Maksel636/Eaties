using UnityEngine;

public class TowerRanger : TowerBase
{
    [SerializeField] private int _damage = 1;
    public override void Attack(Enemy enemy)
    {
        base.Attack(enemy);
        Debug.Log(CanAttack);
        if(_canAttack)
        {
            enemy.Health -= _damage;
            _canAttack = false;
        }
    }
}
