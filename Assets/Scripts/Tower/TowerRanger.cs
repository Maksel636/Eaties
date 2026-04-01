using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TowerRanger : TowerBase
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private GameObject _bulletPrefab;

    private float _bulletSpeed = 0;

    private void Start()
    {
        _bulletSpeed = _bulletPrefab.GetComponent<Bullet>().FlightSpeed;
    }

    public override void Attack(Enemy enemy)
    {
        base.Attack(enemy);
        if(_canAttack)
        {
            _canAttack = false;

            Vector3 start = transform.position;
            start.y += 0.6f;
            Vector3 end = enemy.transform.position;
            float time = Vector3.Distance(start, end) / _bulletSpeed;
            Vector3 futureEnd = enemy.GetPredictedPosition(time);

            var bulletObj = Instantiate(_bulletPrefab, start, Quaternion.Euler(0,0,0));
            
            var bullet = bulletObj.GetComponent<Bullet>();
            bullet.Direction = (futureEnd - start).normalized;
            bullet.Damage = _damage;
        }       
    }
}
