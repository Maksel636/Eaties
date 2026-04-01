using UnityEngine;

public class PiercingBullet : Bullet
{
    private int _piercedTargets = 0;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(Damage);

            _piercedTargets++;
            Debug.Log(_piercedTargets);
        }
    }
}
