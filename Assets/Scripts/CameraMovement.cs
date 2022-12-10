using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Events;

public class CameraMovement : MonoBehaviour
{
    private Camera _cam;

    private Coroutine _movingCoroutine = null;
    private Coroutine _scalingFovCoroutine = null;

    private Vector3 _screenResolution = Vector3.zero;
    private Vector3 _cursorClickPos = Vector3.zero;
    private Vector3 _mainPosition = Vector3.zero;

    private bool _canMove = true;
    private float _mainFov = 0;

    private void Awake()
    {
        _cam = GetComponent<Camera>();

        _screenResolution = new Vector2(Screen.width, Screen.height);
        _mainPosition = transform.position;

        _mainFov = _cam.fieldOfView;
    }

    public void EnableMoving()
    {
        _canMove = true;
    }

    public void DisableMoving()
    {
        StopCoroutine(_movingCoroutine);
        StopCoroutine(_scalingFovCoroutine);

        _canMove = false;
        transform.DOMove(_mainPosition, 1f);
        DOTween.To(() => _cam.fieldOfView, x => _cam.fieldOfView = x, _mainFov, 0.2f);
    }

    private void Update()
    {
        if (!_canMove) return;

        if (Input.GetMouseButtonDown(0))
        {
            _cursorClickPos = Input.mousePosition;

            _movingCoroutine = StartCoroutine(MovingCoroutine());
            _scalingFovCoroutine = StartCoroutine(ScalingFovCoroutine());
        }

        if (Input.GetMouseButtonUp(0))
        {
            DisableMoving();
        }
    }

    private IEnumerator MovingCoroutine()
    {
        while (true)
        {
            Vector3 newPos = (Input.mousePosition - _cursorClickPos) / _screenResolution.x;
            Vector3 translatedPos = (transform.right * newPos.x) + (transform.up * newPos.y);
            Vector3 finalPos = _mainPosition + translatedPos;

            transform.localPosition = Vector3.Slerp(transform.position, finalPos, 0.5f);

            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator ScalingFovCoroutine()
    {
        while (true)
        {
            float mouseDistance = Vector3.Distance(Input.mousePosition, _cursorClickPos) / _screenResolution.x;
            _cam.fieldOfView = _mainFov + mouseDistance * 5f;

            yield return new WaitForEndOfFrame();

            var a = 0;
        }
    }
}
