using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerInputs : MonoBehaviour
{
    private InputMaster _inputMaster = null;
    private InputAction _mousePosition = null;
    private TurnSwitcher _turnSwitcher = null;
    private Camera _mainCamera = null;

    private PlayerEntityAiming _currentEntityAim = null;

    private bool _canAim = false;

    public bool CanAim
    {
        set { _canAim = value; }
    }

    private void Awake()
    {
        _inputMaster = new InputMaster();

        InputAction _mouseButton = _inputMaster.Inputs.LeftMouseButton;
        _mousePosition = _inputMaster.Inputs.MousePosition;
        _mouseButton.performed += _ => StartAiming();
        _mouseButton.canceled += _ => CancelAiming();

        _mainCamera = Camera.main;
    }

    private void Start()
    {
        _turnSwitcher = FindObjectOfType<TurnSwitcher>();
    }

    private void StartAiming()
    {
        if (!_canAim) return;
        Ray ray = _mainCamera.ScreenPointToRay(_mousePosition.ReadValue<Vector2>());
        if (Physics.Raycast(ray, out RaycastHit hit)) 
        {
            if (hit.collider.TryGetComponent(out Entity detectedEntity)) 
            {
                if (detectedEntity == _turnSwitcher.CurrentEntity)
                {
                    _currentEntityAim = detectedEntity.GetComponent<PlayerEntityAiming>();
                    _currentEntityAim.StartAiming();
                }
            }
        }
    }

    private void CancelAiming()
    {
        if (_currentEntityAim != null)
        {
            _currentEntityAim.CancelAiming();
            _canAim = false;
        }
    }

    private void OnEnable()
    {
        _inputMaster.Enable();
    }
}
