using UnityEngine;
using Random = UnityEngine.Random;

public class EscapeAnimal : MonoBehaviour
{
    [SerializeField] private float _escapSpeed = 5f;
    [SerializeField] private float _rotation = 0;
    private bool _isEscaping = false;
    private int _direction;
    [SerializeField]private float _escapeSteps;
    void Start()
    {
        StartRotating();
    }

    void Update()
    {
        if (_isEscaping)
            UpdateRotation();
    }
    public void StartRotating()
    {
        _rotation = Random.Range(0,360);
        _isEscaping = true;
    }

    private void UpdateRotation()
    {
        _escapeSteps -= 100 * Time.deltaTime;
        if (_escapeSteps <= 0)
        {
            _escapeSteps = Random.Range(0, 200);
            ChooseDirection();
        }

        _rotation += _escapSpeed * _direction * Time.deltaTime;

        transform.rotation = Quaternion.Euler(0, _rotation, 0);
    }

    private int ChooseDirection()
    {
        return _direction = 1 == Random.Range(0, 2)? -1: 1;
    }
}
