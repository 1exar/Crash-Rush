using System;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using System.Collections;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] private GameObject touchPreview;
    [SerializeField] private RectTransform aimPreview;

    private InputMaster _inputMaster;
    private InputAction _mousePosition;
    private TurnSwitcher _turnSwitcher;
    private PlayerEntityAiming _currentEntityAim;

    private Coroutine _aimPreviewCoroutine;

    private Vector2 _lastMousePos;
    private float distance;
    private bool _canAim = false;

    public bool CanAim
    {
        set { _canAim = value; }
    }

    private void Awake()
    {
        _inputMaster = new InputMaster();
        _mousePosition = _inputMaster.Inputs.MousePosition;

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
        _lastMousePos = _mousePosition.ReadValue<Vector2>();

        touchPreview.SetActive(true);
        touchPreview.transform.position = _lastMousePos;

        _currentEntityAim = _turnSwitcher.CurrentEntity.GetComponent<PlayerEntityAiming>();
        _currentEntityAim.StartAiming();
        _aimPreviewCoroutine = StartCoroutine(AimPreview());
    }

    private IEnumerator AimPreview()
    {
        while (true)
        {
            distance = Vector3.Distance(_lastMousePos / Screen.width, _mousePosition.ReadValue<Vector2>() / Screen.width);
            touchPreview.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -_currentEntityAim.transform.eulerAngles.y + 90));
            aimPreview.sizeDelta = new Vector2(distance * 300, 50);
            yield return new WaitForFixedUpdate();
        }
    }

    private void CancelAiming()
    {
        if (!_canAim) return;

        if (_currentEntityAim != null)
        {
            StopCoroutine(_aimPreviewCoroutine);
            if (distance <= 0.05f)
            {
                _currentEntityAim.CancelAiming();
            }
            else
            {
                _currentEntityAim.ProcessAiming();
                _canAim = false;
            }
            touchPreview.SetActive(false);
        }
    }

    private void OnEnable()
    {
        _inputMaster.Enable();
    }
}
