using UnityEngine;
using UnityEngine.InputSystem;

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
    public LayerMask _layerMask;

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
        Collider[] hits = Physics.OverlapSphere(transform.position, _grabRadius, _layerMask);
        foreach (Collider hit in hits)
        {
            _currentEnemy = hit.GetComponent<Enemy>();
            _currentEnemy.IsPickedUp = true;
            _currentEnemy.transform.SetParent(_grabSocket);
            _currentEnemy.transform.localPosition = Vector3.zero;
            break;
        }
    }
}
