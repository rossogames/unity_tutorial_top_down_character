using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    private Animator _animator;
    private InputSystem_Actions _playerInput;

    private Vector2 _movementInput;
    private Vector2 _moveDirection;
    private float _speed = 5f;

    public Vector2 MoveDirection 
    {
        get => _moveDirection;
        set
        {
            _moveDirection = value;
            _animator.SetFloat("moveDirectionX", _moveDirection.x);
            _animator.SetFloat("moveDirectionY", _moveDirection.y);
        }
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerInput = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        _playerInput.Player.Enable();
        _playerInput.Player.Move.performed += OnPlayerMove;
        _playerInput.Player.Move.canceled += OnPlayerMove;
    }

    private void OnDisable()
    {
        _playerInput.Player.Disable();
        _playerInput.Player.Move.performed -= OnPlayerMove;
        _playerInput.Player.Move.canceled -= OnPlayerMove;
    }

    // Update is called once per frame
    void Update()
    {
        MoveDirection = new Vector3(_movementInput.x, _movementInput.y, 0);
        transform.Translate(_speed * Time.deltaTime * MoveDirection);
    }

    private void OnPlayerMove(InputAction.CallbackContext context)
    { 
        _movementInput = context.ReadValue<Vector2>();
    }
}
