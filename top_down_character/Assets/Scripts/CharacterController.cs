using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    private Animator _animator;
    private InputSystem_Actions _playerInput;

    private Vector2 _moveDirection;
    private float _speed = 5f;

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
        transform.Translate(_speed * Time.deltaTime * _moveDirection);
    }

    private void OnPlayerMove(InputAction.CallbackContext context)
    {
        var inputValue = context.ReadValue<Vector2>();
        _moveDirection = new Vector2(inputValue.x, inputValue.y);

        if (_moveDirection != Vector2.zero)
        {
            _animator.SetFloat("moveDirectionX", _moveDirection.x);
            _animator.SetFloat("moveDirectionY", _moveDirection.y);
        }
    }
}
