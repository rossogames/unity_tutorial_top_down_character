using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    private GameInputs _inputs;

    private Vector2 _moveDirection;
    private float _maxSpeed = 5;
    private float _acceleration = 5;
    private float _deceleration = 8;

    private float _currentSpeed;

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

    public float CurrentSpeed 
    { 
        get => _currentSpeed;
        set
        {
            _currentSpeed = value;
            _animator.SetFloat("Speed", _currentSpeed);
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
        if (MoveDirection != Vector2.zero)
        {
            CurrentSpeed = Mathf.MoveTowards(_currentSpeed, _maxSpeed, _acceleration * Time.deltaTime);
        }
        else
        {
            CurrentSpeed = Mathf.MoveTowards(_currentSpeed, 0, _deceleration * Time.deltaTime);
        }

        Vector3 move = Time.deltaTime * CurrentSpeed * new Vector2(MoveDirection.x, MoveDirection.y);
        transform.Translate(move, Space.World);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        MoveDirection = context.ReadValue<Vector2>();
    }
}
