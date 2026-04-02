using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TowerRanger : TowerBase
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _bulletSocket;

    private float _bulletSpeed = 0;

    protected override void Start()
    {
        base.Start();
        _bulletSpeed = _bulletPrefab.GetComponent<Bullet>().FlightSpeed;
    }

    public override void Attack(Enemy enemy)
    {
        base.Attack(enemy);
        if(_canAttack)
        {
            Audio.Instance.InitiateSound(Audio.SoundType.TowerAttack);
            _canAttack = false;

            Vector3 end = enemy.transform.position;
            float time = Vector3.Distance(_bulletSocket.transform.position, end) / _bulletSpeed;
            Vector3 futureEnd = enemy.GetPredictedPosition(time);

            var bulletObj = Instantiate(_bulletPrefab, _bulletSocket.transform.position, Quaternion.Euler(0,0,0));
            
            var bullet = bulletObj.GetComponent<Bullet>();
            bullet.Direction = (futureEnd - _bulletSocket.transform.position).normalized;
            bullet.Damage = _damage;
        }       
    }
}
