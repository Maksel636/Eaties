using System.Collections;
using UnityEngine;

public class TowerRanger : TowerBase
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private GameObject _bulletPrefab;

    public override void Attack(Enemy enemy)
    {
        base.Attack(enemy);
        if(_canAttack)
        {
            //enemy.TakeDamage(_damage);
            _canAttack = false;

            
            //Debug drawing
            Vector3 start = transform.position;
            start.y += 0.6f;

            Vector3 end = enemy.transform.position;
            float duration = 0.4f;

            Debug.DrawLine(start, end, Color.red, duration);

            var bulletObj = Instantiate(_bulletPrefab, start, Quaternion.Euler(0,0,0));
            
            var bullet = bulletObj.GetComponent<Bullet>();
            bullet.Direction = (end - start).normalized;
            bullet.Damage = _damage;
        }       
    }
}
