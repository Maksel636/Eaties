using System.Collections;
using UnityEngine;

public class TowerTriple : TowerBase
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _bulletSocket;

    private float _bulletSpeed = 0;

    protected override void Start()
    {
        base.Start();
        _bulletSpeed = _bulletPrefab.GetComponentInChildren<Bullet>().FlightSpeed;
    }

    public override void Attack(Enemy enemy)
    {
        base.Attack(enemy);
        if(_canAttack)
        {
            _canAttack = false;
            StartCoroutine(DelayShotByAFrame());
        }       
    }

    private IEnumerator DelayShotByAFrame()
    {
        yield return null;

        var bulletObj = Instantiate(_bulletPrefab, _bulletSocket.transform.position, _rotatable.transform.localRotation);

        var bullets = bulletObj.GetComponentsInChildren<Bullet>();
        foreach (Bullet bullet in bullets)
        {
            bullet.Direction = transform.forward.normalized;
            bullet.Damage = _damage;
        }
    } 
}
