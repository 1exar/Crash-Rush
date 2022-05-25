using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using DG.Tweening.Plugins.Options;
using DG.Tweening.Core;

public class EntityMovement_noRB : MonoBehaviour
{
    [SerializeField] private float speedLimit = 5f;
    [SerializeField] private float collisionDistance = 0.75f;

    private Transform _thisObjectTransform;
    private Entity _thisEntity;
    private Coroutine _movementCoroutine;
    private Coroutine _collisionHandleCoroutine;
    private PathGenerator _pathGenerator;
    private TweenerCore<float, float, FloatOptions> _changeSpeedTween;
    private TurnSwitcher _turnSwitcher;

    private Vector3 _motionDirection;

    private float _currentSpeed = 0;

    public float CurrentSpeed
    {
        get { return _currentSpeed; }
    }

    private void Awake()
    {
        _thisObjectTransform = transform;
        _thisEntity = GetComponent<Entity>();
    }

    private void Start()
    {
        _pathGenerator = FindObjectOfType<PathGenerator>();
        _turnSwitcher = FindObjectOfType<TurnSwitcher>();

        _collisionHandleCoroutine = StartCoroutine(CollisionHandle());
    }

    /*public void Bounce(Vector3 normal)
    {
        Vector3[] path = _pathGenerator.GeneratePath(transform.position, normal, 100f, 30);
        _pathGenerator.DebugDrawPath(path);
        MoveAlongPath(path, _currentSpeed / 1.05f);
    }*/

    /*public void MoveAlongPath(Vector3[] path, float speed)
    {
        if (_movementCoroutine == null) _movementCoroutine = StartCoroutine(MovementCoroutine(path, speed));
    }*/

    public void Move(float speed, Vector3 direction)
    {
        _changeSpeedTween.Kill();
        float speedChangingDuration = 2;
        _currentSpeed = Mathf.Clamp(speed, 0, speedLimit);
        _changeSpeedTween = DOTween.To(() => _currentSpeed, x => _currentSpeed = x, 0f, speedChangingDuration);
        _motionDirection = direction;
        _motionDirection.y = 0;
        if (_movementCoroutine == null) _movementCoroutine = StartCoroutine(MovementCoroutine());
    }

    private IEnumerator MovementCoroutine()
    {
        while (_currentSpeed > 0.001f)
        {
            transform.position = Vector3.MoveTowards(_thisObjectTransform.position, _thisObjectTransform.position + _motionDirection, _currentSpeed);
            yield return new WaitForFixedUpdate();
        }
        _currentSpeed = 0;

        _movementCoroutine = null;
    }

    /*private IEnumerator MovementCoroutine(Vector3[] path, float speed)
    {
        _thisObjectTransform.position = new Vector3(_thisObjectTransform.position.x, 0, _thisObjectTransform.position.z);
        _currentSpeed = Mathf.Clamp(speed, 0, speedLimit);
        float speedChangingDuration = 3;
        _changeSpeedTween = DOTween.To(() => _currentSpeed, x => _currentSpeed = x, 0f, speedChangingDuration);


        for (int i = 1; i < path.Length; i++)
        {
            while (true)
            {
                yield return new WaitForFixedUpdate();
                if (_currentSpeed == 0)
                {
                    StopCoroutine(_movementCoroutine);
                    _movementCoroutine = null;
                }
                _thisObjectTransform.position = Vector3.MoveTowards(_thisObjectTransform.position, path[i], _currentSpeed);
                if (_thisObjectTransform.position == path[i]) break;
            }

            gameObject.layer = 0;
        }
    }*/

    private IEnumerator CollisionHandle()
    {
        while (true)
        {
            bool isThisEntityTurn = _turnSwitcher.CurrentEntity.IsMine == _thisEntity.IsMine;
            Debug.Log(name + " - collision detection");
            Vector3 averageDirection = Vector3.zero;
            int directionsCount = 0;

            for (int i = 0; i < 8; i++)
            {
                _thisObjectTransform.Rotate(Vector3.up * 45);

                Ray ray = new Ray(_thisObjectTransform.position, _thisObjectTransform.forward);
                Debug.DrawLine(_thisObjectTransform.position, _thisObjectTransform.position + (_thisObjectTransform.forward * collisionDistance), Color.yellow);
                if (Physics.Raycast(ray, out RaycastHit hit, collisionDistance))
                {
                    Vector3 direction = Vector3.Reflect(_motionDirection, hit.normal);
                    if (hit.collider.TryGetComponent(out Entity entity))
                    {
                        if (isThisEntityTurn)
                        {
                            //entity.Movement.Move(_currentSpeed / 1.05f, -hit.normal);

                            if (_thisEntity.IsMine != entity.IsMine)
                            {
                                entity.TakeDamage(_thisEntity.Damage);
                            }
                        }

                        direction = hit.normal;
                    }

                    directionsCount += 1;
                    averageDirection += direction;
                }
            }

            if (averageDirection != Vector3.zero)
            {
                _changeSpeedTween.Kill();

                averageDirection /= directionsCount;
                Move(_currentSpeed / 1.05f, averageDirection);
            }

            yield return new WaitForFixedUpdate();
        }
    }
}
