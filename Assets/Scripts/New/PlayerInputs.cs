using System;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerInputs : MonoBehaviour
{
    private InputMaster _inputMaster;
    private InputAction _mousePosition;
    private TurnSwitcher _turnSwitcher;
    private Camera _mainCamera;

    private PlayerEntityAiming _currentEntityAim;

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
                    if (_mainCamera.TryGetComponent(out CameraMovement _camera))
                    {
                        _camera.EnableMoving();
                    }
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
            _currentEntityAim = null;
        }
    }

    private void OnEnable()
    {
        _inputMaster.Enable();
    }
}
