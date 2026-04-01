using System.Collections;
using UnityEngine;

public class TowerRanger : TowerBase
{
    [SerializeField] private int _damage = 1;

    public override void Attack(Enemy enemy)
    {
        base.Attack(enemy);
        if(_canAttack)
        {
            Debug.Log("Attack " + enemy.gameObject.name);
            enemy.TakeDamage(_damage);
            _canAttack = false;

            
            //Debug drawing
            Vector3 start = transform.position;
            start.y += 0.6f;

            Vector3 end = enemy.transform.position;
            float duration = 0.4f;

            Debug.DrawLine(start, end, Color.red, duration);

        }       
    }
}
