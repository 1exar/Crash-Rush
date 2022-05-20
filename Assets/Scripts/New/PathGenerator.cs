using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private int _layermask;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _layermask = ~LayerMask.GetMask("Ignore Raycast");
    }

    public Vector3[] GeneratePath(Vector3 originPosition, Vector3 originDirection, float pathLength, int maxReflections = 10)
    {
        List<Vector3> pathPoints = new List<Vector3>();

        Vector3 originPoint = originPosition;
        Vector3 direction = originDirection;

        float remainLegth = pathLength;
        float radius = 1f;

        for (int i = 0; i < maxReflections; i++)
        {
            pathPoints.Add(originPoint);

            Ray ray = new Ray(originPoint, direction);

            Physics.Raycast(ray, out RaycastHit raycastHit, 100f, _layermask);
            Physics.SphereCast(ray, radius, out RaycastHit sphereCastHit, 100f, _layermask);

            RaycastHit finalHit = sphereCastHit;
            if (raycastHit.point != sphereCastHit.point) finalHit = raycastHit;

            Vector3 point = finalHit.point + finalHit.normal * (radius / 2);
            float currentDistance = Vector3.Distance(originPoint, point);

            if (remainLegth <= currentDistance && pathPoints.Count < maxReflections) 
            {
                point = originPoint + direction * remainLegth;
                pathPoints.Add(point);
                break;
            }

            if (finalHit.collider != null && finalHit.collider.TryGetComponent(out Entity _))
            {
                pathPoints.Add(point);
                break;
            }

            remainLegth -= currentDistance;
            originPoint = point;
            direction = Vector3.Reflect(direction, finalHit.normal);
        }

        return pathPoints.ToArray();
    }

    public void DebugDrawPath(Vector3[] path)
    {
        for (int i = 0; i < path.Length - 1; i++)
        {
            Debug.DrawLine(path[i], path[i + 1], Color.red);
        }
    }

    public void DrawPath(Vector3[] path)
    {
        _lineRenderer.positionCount = path.Length;
        _lineRenderer.SetPositions(path);
    }

    public void ClearPathDrawing()
    {
        _lineRenderer.positionCount = 0;
    }
}
