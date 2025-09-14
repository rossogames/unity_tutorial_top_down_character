using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    private GameInputs _inputs;

    private float _moveSpeed = 5f;
    private Vector2 _moveDirection;

    private Vector2 MoveDirection
    {
        get => _moveDirection;
        set
        {
            _moveDirection = value;
            if (value != Vector2.zero)
            {
                _animator.SetFloat("MoveDirectionX", _moveDirection.x);
                _animator.SetFloat("MoveDirectionY", _moveDirection.y);
            }
        }
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _inputs = new GameInputs();
    }

    private void OnEnable()
    {
        _inputs.Player.Enable();
        _inputs.Player.Move.performed += OnMove;
        _inputs.Player.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        _inputs.Player.Disable();
        _inputs.Player.Move.performed -= OnMove;
        _inputs.Player.Move.canceled -= OnMove;
    }

    private void Update()
    {
        Vector3 move = Time.deltaTime * _moveSpeed * new Vector2(MoveDirection.x, MoveDirection.y);
        transform.Translate(move, Space.World);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        MoveDirection = context.ReadValue<Vector2>();
    }
}
