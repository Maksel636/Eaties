using UnityEngine;

public class ItemAnimator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float _rotationSpeed = 30f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, Time.time * _rotationSpeed, 0);
    }
}
