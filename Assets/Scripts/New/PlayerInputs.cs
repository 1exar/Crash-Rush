using System;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerInputs : MonoBehaviour
{
    private InputMaster _inputMaster;
    private TurnSwitcher _turnSwitcher;

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
        _mouseButton.started += _ => StartAiming();
        _mouseButton.canceled += _ => CancelAiming();
    }

    private void Start()
    {
        _turnSwitcher = FindObjectOfType<TurnSwitcher>();
    }

    private void StartAiming()
    {
        if (!_canAim) return;
        
        _currentEntityAim = _turnSwitcher.CurrentEntity.GetComponent<PlayerEntityAiming>();
        _currentEntityAim.StartAiming();
    }

    private void CancelAiming()
    {
        if (!_canAim) return;

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
