using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using System.Linq;

public class PlayerEntityAiming : MonoBehaviour
{
    private InputMaster _inputMaster;
    private InputAction _mousePosition;
    private Coroutine _aimingCoroutine;
    private PathGenerator _pathGenerator;
    private EntityMovement _movement;
    private TurnSwitcher _turnSwitcher;

    private Transform _thisObjectTransform;
    private Vector2 _lastMousePos = Vector2.zero;
    private float _pathLength;
    [SerializeField]
    private SpriteRenderer _powerIndicator;
    [SerializeField]
    private SpriteRenderer _circle;
    
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
        
        _powerIndicator.gameObject.SetActive(true);
    }

    public void CancelAiming()
    {
        StopCoroutine(_aimingCoroutine);

        gameObject.layer = 0;
        _pathGenerator.ClearPathDrawing();

        _movement.Move(_pathLength / 3, _thisObjectTransform.forward);
        _turnSwitcher.PrepareToSwitch();
        
        _powerIndicator.gameObject.SetActive(false);
        _circle.color = new Color(0, 0, 0, 0);
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
        float attackPower = Vector3.Distance(_lastMousePos, _mousePosition.ReadValue<Vector2>()) / Screen.width * 20;
        Vector3 tempPos = transform.position + transform.forward * (attackPower * attackPower) / 2f;
        _pathLength = Vector3.Distance(_thisObjectTransform.position, tempPos);

        print(attackPower);
        
        if (attackPower < 1)
        {
            _powerIndicator.size = new Vector2(8, 10);
            _powerIndicator.transform.localPosition = new Vector3(0, 0, 1);
        }
        else if (attackPower < 10)
        {
            _powerIndicator.size = new Vector2(12.8f, 10);
            _powerIndicator.transform.localPosition = new Vector3(0, 0, 1.2f);
        }
        else if(attackPower < 20)
        {
            _powerIndicator.size = new Vector2(17.4f, 10);
            _powerIndicator.transform.localPosition = new Vector3(0, 0, 1.4f);
        }
        else if (attackPower < 30)
        {
            _powerIndicator.size = new Vector2(22.4f, 10);
            _powerIndicator.transform.localPosition = new Vector3(0, 0, 1.6f);
        }
        else
        {
            _powerIndicator.size = new Vector2(27f, 10);
            _powerIndicator.transform.localPosition = new Vector3(0, 0, 1.8f);
        }
        
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
