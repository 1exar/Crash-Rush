using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryViewer : MonoBehaviour
{
    [SerializeField]
    private BallAttack _ballAttack;
    [SerializeField]
    private BallMovment _ballMovement;

    private LineRenderer _lineRenderer = null;
    private Ball _damagePreviewBall = null;
    private Coroutine _calculatePreviewCoroutine = null;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void Enable()
    {
        _lineRenderer.enabled = true;
    }

    public void Disable()
    {
        _lineRenderer.enabled = false;
    }

    public void CalculatePreview(Vector3 trajectoryPos)
    {
        float trajectoryLength = Vector3.Distance(transform.position, trajectoryPos);

        List<Vector3> points = new List<Vector3>();
        Vector3 originPoint = transform.position;
        Vector3 direction = transform.forward;
        bool caughtBall = false;

        RaycastHit hit = new RaycastHit();
        _lineRenderer.positionCount = 4;
        for (int i = 0; i < 4; i++)
        {
            points.Add(originPoint);

            Ray ray = new Ray(originPoint, direction);
            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                if (Vector3.Distance(transform.position, hit.point) > trajectoryLength)
                {
                    points.Add(originPoint + direction * trajectoryLength);
                    break;
                }

                trajectoryLength -= Vector3.Distance(originPoint, hit.point);


                originPoint = hit.point + hit.normal * 0.75f;
                direction = Vector3.Reflect(direction, hit.normal);
            }
        }

        if (!caughtBall && _damagePreviewBall != null)
        {
            _damagePreviewBall.Health.CancelPreview();
            _damagePreviewBall = null;
        }

        _lineRenderer.positionCount = points.Count;
        _lineRenderer.SetPositions(points.ToArray());
    }
}
