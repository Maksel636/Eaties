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

    private float _verticalSpeed = 0f;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        //move in 4 directions
        Vector2 input = _playerInput.actions["Move"].ReadValue<Vector2>(); ;
        Vector3 moveVelocity = new Vector3(input.x, 0f, input.y) * _moveSpeed;
        Debug.Log(input);
        _controller.Move(moveVelocity * Time.deltaTime);

        //apply gravity
        _verticalSpeed += Physics.gravity.y * Time.fixedDeltaTime;
        _controller.Move(Vector3.up * _verticalSpeed * Time.fixedDeltaTime);
        if (_controller.isGrounded)
        {
            _verticalSpeed = 0f;
        }
    }
}
