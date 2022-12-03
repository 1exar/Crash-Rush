using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class PlayerEntityAiming : MonoBehaviour
{
    [SerializeField] private AttackPowerViewer attackPowerViewer;
    [SerializeField] private SpriteRenderer _circle;
    
    private Coroutine _aimingCoroutine;
    private PathGenerator _pathGenerator;
    private EntityMovement _movement;
    private TurnSwitcher _turnSwitcher;

    private Transform _thisObjectTransform;
    private Vector3 _lastMousePos = Vector3.zero;
    private float _pathLength;
    
    private void Awake()
    {

        _pathGenerator = FindObjectOfType<PathGenerator>();
        _movement = GetComponent<EntityMovement>();
        _thisObjectTransform = transform;
    }

    private void Start()
    {
        _turnSwitcher = FindObjectOfType<TurnSwitcher>();
    }

    public void StartAiming(Vector2 pointerPosition)
    {
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

        _lastMousePos = Input.mousePosition;
        _aimingCoroutine = StartCoroutine(AimingCoroutine());

        attackPowerViewer.EnablePreview();
    }

    public void CancelAiming()
    {
        StopCoroutine(_aimingCoroutine);
        gameObject.layer = 0;
        _pathGenerator.ClearPathDrawing();
        attackPowerViewer.DisablePreview();
    }

    public void ProcessAiming()
    {
        CancelAiming();

        _movement.Move(_pathLength, _thisObjectTransform.forward);
        _turnSwitcher.PrepareToSwitch();

        _circle.color = Color.clear;
    }

    private IEnumerator AimingCoroutine()
    {
        while (true)
        {
            Vector3 relativeMousePos = Input.mousePosition - _lastMousePos;
            Vector3 translatedMousePos = new Vector3(relativeMousePos.x, 0, relativeMousePos.y);
            Vector3 lookAtPos = _thisObjectTransform.position - translatedMousePos;
            transform.LookAt(lookAtPos);

            DrawPath();

            yield return new WaitForFixedUpdate();
        }
    }

    private void DrawPath()
    {
        float attackPower = Vector3.Distance(_lastMousePos, Input.mousePosition) / Screen.width * 100;
        Vector3 tempPos = transform.position + transform.forward * (attackPower * attackPower) / 2f;
        _pathLength = Vector3.Distance(_thisObjectTransform.position, tempPos);

        attackPowerViewer.SetPreview(attackPower/2);
        
        Vector3[] previewPath = _pathGenerator.GeneratePath(_thisObjectTransform.position, _thisObjectTransform.forward, _pathLength);
        _pathGenerator.DrawPath(previewPath);
    }
}
