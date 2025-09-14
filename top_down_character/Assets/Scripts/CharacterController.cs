using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    private Animator _animator;
    private InputSystem_Actions _playerInput;

    private Vector2 _movement;
    private float _speed = 5f;

    public Vector2 Movement
    {
        get => _movement;
        set
        {
            _movement = value;
            _animator.SetFloat("moveX", _movement.x);
            _animator.SetFloat("moveY", _movement.y);
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
        if (Movement != Vector2.zero)
            transform.Translate(Movement);
    }

    private void OnPlayerMove(InputAction.CallbackContext context)
    {
        var _movementInput = context.ReadValue<Vector2>();
        var direction = new Vector3(_movementInput.x, _movementInput.y, 0);
        Movement = _speed * Time.deltaTime * direction;
    }
}
