using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using DG.Tweening;
using System.Collections;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] private GameObject touchPreview;
    [SerializeField] private Image crossSign;
    [SerializeField] private RectTransform aimPreview;

    private InputMaster _inputMaster;
    private InputAction _mousePosition;
    private TurnSwitcher _turnSwitcher;
    private PlayerEntityAiming _currentEntityAim;

    private Coroutine _aimPreviewCoroutine;

    private Vector2 _lastMousePos;
    private float distance;
    private bool _canAim = false;
    private bool _canCancelAiming = false;

    public bool CanAim
    {
        set { if (value) StartCoroutine(SetCanAimTrue()); }
    }

    private IEnumerator SetCanAimTrue()
    {
        yield return new WaitForSeconds(0.2f);
        _canAim = true;
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
        _currentEntityAim.StartAiming(_lastMousePos);
        _aimPreviewCoroutine = StartCoroutine(AimPreview());
    }

    private IEnumerator AimPreview()
    {
        while (true)
        {
            distance = Vector3.Distance(_lastMousePos / Screen.width, _mousePosition.ReadValue<Vector2>() / Screen.width);
            if (distance <= 0.25f && _canCancelAiming)
            {
                crossSign.transform.position = touchPreview.transform.position;
                crossSign.DOFade(1, 0.1f);
                crossSign.transform.DOScale(1f, 0.2f);
                _canCancelAiming = false;
            }
            else if (distance > 0.25f)
            {
                if (_canCancelAiming == false)
                {
                    crossSign.transform.DOScale(0.5f, 0.2f);
                    crossSign.DOFade(0, 0.1f);
                }
                _canCancelAiming = true;
            }
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
            if (distance > 0.18f)
            {
                _currentEntityAim.ProcessAiming();
                _canAim = false;
            }
            else
            {
                crossSign.transform.DOScale(0.5f, 0.2f);
                crossSign.DOFade(0, 0.1f);
                _currentEntityAim.CancelAiming();
            }
            touchPreview.SetActive(false);
            StopCoroutine(_aimPreviewCoroutine);
        }
    }

    private void OnEnable()
    {
        _inputMaster.Enable();
    }
}
