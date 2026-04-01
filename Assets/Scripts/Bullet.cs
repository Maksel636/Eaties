using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 Direction { get; set; } = Vector3.zero;
    [SerializeField] private float _flightSpeed;
    public float FlightSpeed => _flightSpeed;
    public int Damage { get; set; } = 0;
    [SerializeField] private float _maxLifetime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, _maxLifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Direction * (_flightSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(Damage);
            Destroy(gameObject);
        }

        //Debug.Log(other.gameObject.name);
    }
}
