using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using System.Linq;

public class PlayerEntityAiming : MonoBehaviour
{
    [SerializeField] private AttackPowerViewer attackPowerViewer;
    [SerializeField] private SpriteRenderer _circle;

    private InputMaster _inputMaster;
    private InputAction _mousePosition;
    private Coroutine _aimingCoroutine;
    private PathGenerator _pathGenerator;
    private EntityMovement _movement;
    private TurnSwitcher _turnSwitcher;

    private Transform _thisObjectTransform;
    private Vector2 _lastMousePos = Vector2.zero;
    private float _pathLength;
    
    private void Awake()
    {
        _inputMaster = new InputMaster();
        _mousePosition = _inputMaster.Inputs.MousePosition;

        _pathGenerator = FindObjectOfType<PathGenerator>();
        _movement = GetComponent<EntityMovement>();
        _thisObjectTransform = transform;
    }

    private void Start()
    {
        _turnSwitcher = FindObjectOfType<TurnSwitcher>();
    }

    public void StartAiming()
    {
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

        _lastMousePos = _mousePosition.ReadValue<Vector2>();
        _aimingCoroutine = StartCoroutine(AimingCoroutine());

        attackPowerViewer.EnablePreview();
    }

    public void CancelAiming()
    {
        StopCoroutine(_aimingCoroutine);

        gameObject.layer = 0;
        _pathGenerator.ClearPathDrawing();

        _movement.Move(_pathLength / 2, _thisObjectTransform.forward);
        _turnSwitcher.PrepareToSwitch();

        _circle.color = Color.clear;

        attackPowerViewer.DisablePreview();
    }

    private IEnumerator AimingCoroutine()
    {
        while (true)
        {
            Vector2 relativeMousePos = _mousePosition.ReadValue<Vector2>() - _lastMousePos;
            Vector3 translatedMousePos = new Vector3(relativeMousePos.x, 0, relativeMousePos.y);
            Vector3 lookAtPos = _thisObjectTransform.position - translatedMousePos;
            transform.LookAt(lookAtPos);

            DrawPath();

            yield return new WaitForFixedUpdate();
        }
    }

    private void DrawPath()
    {
        float attackPower = Vector3.Distance(_lastMousePos, _mousePosition.ReadValue<Vector2>()) / Screen.width * 40;
        Vector3 tempPos = transform.position + transform.forward * (attackPower * attackPower) / 2f;
        _pathLength = Vector3.Distance(_thisObjectTransform.position, tempPos);

        attackPowerViewer.SetPreview(attackPower);
        
        Vector3[] previewPath = _pathGenerator.GeneratePath(_thisObjectTransform.position, _thisObjectTransform.forward, _pathLength);
        _pathGenerator.DrawPath(previewPath);
    }

    private void OnEnable()
    {
        _inputMaster.Enable();
    }

    private void OnDisable()
    {
        _inputMaster.Disable();
    }
}
