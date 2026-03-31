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
        Vector3 moveVelocity = new Vector3(input.x, 0f, input.y) * _moveSpeed;
        _controller.Move(moveVelocity * Time.deltaTime);

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
        Collider[] hits = Physics.OverlapSphere(transform.position, _grabRadius);
        foreach (Collider hit in hits)
        {
            if (hit.gameObject.name == "Enemy")
            {
                Debug.Log("Enemy nearby");
            }
        }
    }
}
