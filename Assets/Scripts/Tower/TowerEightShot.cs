using UnityEngine;

public class TowerEightShot : TowerBase
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _bulletSocket;
    [SerializeField] private float _bulletDistance;

    private float _bulletSpeed = 0;

    protected override void Start()
    {
        base.Start();
        _bulletSpeed = _bulletPrefab.GetComponent<Bullet>().FlightSpeed;
    }

    public override void Attack(Enemy enemy)
    {
        base.Attack(enemy);
        if (_canAttack)
        {
            Audio.Instance.InitiateSound(Audio.SoundType.TowerAttack);
            _canAttack = false;


            for (int i = 0; i < 8; i++)
            {
                ShootBullet(i * 45);
            }
        }
    }

    private void ShootBullet(float angle) // angle in degrees
    {
        angle = Mathf.Deg2Rad * angle;
        Vector3 direction = new Vector3(Mathf.Cos(angle),0, Mathf.Sin(angle));

        var bulletObj = Instantiate(_bulletPrefab, _bulletSocket.transform.position, Quaternion.Euler(0, 0, 0));

        var bullet = bulletObj.GetComponent<Bullet>();
        bullet.Direction = direction.normalized;
        bullet.Damage = _damage;
        bullet.MaxLifeTime = _bulletDistance/_bulletSpeed;
    }
}