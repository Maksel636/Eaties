using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField]
    private InputActionReference _moveInput;
    [SerializeField]
    private float _moveSpeed = 3f;
    [SerializeField]
    private PlayerInput _playerInput;
    [SerializeField] private InputActionReference _grabInput;
    [SerializeField] private float _grabRadius;
    [SerializeField] private Transform _grabSocket;
    private Enemy _currentEnemy = null;
    [SerializeField] private LayerMask _enemyMask;
    [SerializeField] private LayerMask _towerSpotMask;
    GameObject _currentMeat;
    [SerializeField] private LayerMask _meatMask;
    [SerializeField] private Animator _animator;

    private float _verticalSpeed = 0f;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();

        _grabInput.action.performed += HandleGrabInput;
    }
    void Update()
    {
        //move in 4 directions
        Vector2 input = _playerInput.actions["Move"].ReadValue<Vector2>(); ;
        Vector3 moveDirection = new Vector3(input.x, 0f, input.y);
        
        if(!(moveDirection.magnitude < 0.01) && _animator != null)
        {
            if (!_animator.GetBool("isWalking")) 
            _animator.SetBool("isWalking", true);
        }

        _controller.Move(moveDirection * (_moveSpeed * Time.deltaTime));

        //Rotation
        if (moveDirection.sqrMagnitude >= 0.01f)
        {
            transform.forward = moveDirection.normalized;
        }
        

        //apply gravity
        _verticalSpeed += Physics.gravity.y * Time.fixedDeltaTime;
        _controller.Move(Vector3.up * (_verticalSpeed * Time.fixedDeltaTime));
        if (_controller.isGrounded)
        {
            _verticalSpeed = 0f;
        }
    }

    void HandleGrabInput(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        DropMeat();
        if (_currentEnemy)
        {
            if (Physics.CheckSphere(transform.position, 0.1f, _towerSpotMask))
            {
                TryTowerConstruction();
            }
            else
            {
                //DropEnemy();
            }

        }
        else
        {
            TryPickup();
        }
    }

    void TryPickup()
    {
        if (_currentEnemy != null || _currentMeat != null) // Something is already picked up
            return;

        
        // Pickup enemy
        Collider[] hits = Physics.OverlapSphere(transform.position, _grabRadius, _enemyMask);
        //foreach (Collider hit in hits)
        //{
        //    //Enemy enemy = hit.GetComponent<Enemy>();

        //    //EscapeAnimal escape = enemy.GetComponentInChildren<EscapeAnimal>();
        //    //if (escape == null || !escape.IsCaptured) // verify that the creature is captured, if not
        //    //    continue; // skip this enemy

        //    Debug.LogError("Captures creature");

        //    _currentEnemy = hit.GetComponent<Enemy>();
        //    _currentEnemy.IsPickedUp = true;
        //    _currentEnemy.transform.SetParent(_grabSocket);
        //    _currentEnemy.transform.localPosition = Vector3.zero;
        //    break;
        //}
        

        if (_currentEnemy != null) return; // You just picked up an enemy
        // Pickup meat
        hits = Physics.OverlapSphere(transform.position, _grabRadius, _meatMask);
        foreach (Collider hit in hits)
        {
            _currentMeat = hit.gameObject;
            _currentMeat.transform.SetParent(_grabSocket);
            _currentMeat.transform.localPosition = Vector3.zero;
            break;
        }
    }

    void TryTowerConstruction()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 0.1f, _towerSpotMask);
        if (hits.Length > 0)
        {
            var towerSpot = hits[0].GetComponent<TowerSpot>();

            if (!towerSpot.HasActiveTower)
            {
                if (towerSpot.PlaceTower(_currentEnemy))
                {
                    Destroy(_currentEnemy.gameObject);
                    _currentEnemy = null;
                }
            }
        }
    }

    private void DropEnemy()
    {
        _currentEnemy.IsPickedUp = false;
        _currentEnemy.transform.rotation = Quaternion.Euler(0, 0, 0);
        var pos = _currentEnemy.transform.position;
        pos.y = 0;
        _currentEnemy.transform.position = pos;
        _currentEnemy.transform.SetParent(null);
        _currentEnemy = null;
    }

    private void DropMeat()
    {
        // When holding meat, delete it
        if(_currentMeat != null && _currentEnemy == null)
        {
            Destroy(_currentMeat.gameObject);
        }
    }
    public void GrabCapturedAnimal(GameObject capturedEnemy)
    {
        _currentEnemy = capturedEnemy.GetComponent<Enemy>();
        _currentEnemy.IsPickedUp = true;
        _currentEnemy.transform.SetParent(_grabSocket);
        _currentEnemy.transform.localPosition = Vector3.zero;
    }
}
