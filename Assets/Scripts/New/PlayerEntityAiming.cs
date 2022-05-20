using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using System.Linq;

public class PlayerEntityAiming : MonoBehaviour
{
    private InputMaster _inputMaster = null;
    private InputAction _mousePosition = null;
    private Coroutine _aimingCoroutine = null;
    private PathGenerator _pathGenerator = null;
    private EntityMovement _movement = null;
    private TurnSwitcher _turnSwitcher = null;

    private Transform _thisObjectTransform = null;
    private Vector3[] _attackingPath = null;
    private Vector2 _lastMousePos = Vector2.zero;
    private float _pathLength = 0;

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
    }

    public void CancelAiming()
    {
        StopCoroutine(_aimingCoroutine);

        gameObject.layer = 0;
        _pathGenerator.ClearPathDrawing();
        _movement.MoveAlongPath(_attackingPath, _pathLength/30);
        _turnSwitcher.PrepareToSwitch();
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
        float attackPower = Vector3.Distance(_lastMousePos, _mousePosition.ReadValue<Vector2>()) / 100f;
        Vector3 tempPos = transform.position + transform.forward * (attackPower * attackPower) / 2f;
        _pathLength = Vector3.Distance(_thisObjectTransform.position, tempPos);

        _attackingPath = _pathGenerator.GeneratePath(_thisObjectTransform.position, _thisObjectTransform.forward, 100, 20);
        Vector3[] drawingPath = _pathGenerator.GeneratePath(_thisObjectTransform.position, _thisObjectTransform.forward, _pathLength, 4);
        _pathGenerator.DrawPath(drawingPath);
        _pathGenerator.DebugDrawPath(_attackingPath);
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
