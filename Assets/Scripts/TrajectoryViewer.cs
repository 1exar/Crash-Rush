using UnityEngine;

public class TrajectoryViewer : MonoBehaviour
{
    private LineRenderer _lineRenderer = null;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        Vector3[] points = new Vector3[4];
        Vector3 originPoint = transform.position;
        Vector3 direction = transform.forward;
        Ball enemyBall = null;

        RaycastHit hit = new RaycastHit();
        for (int i = 0; i < 4; i++)
        {
            points[i] = originPoint;

            Ray ray = new Ray(originPoint, direction);
            Physics.Raycast(ray, out hit);

            if (hit.collider.TryGetComponent(out Ball ball))
            {
                if (enemyBall == null)
                {
                    if (!ball.IsMine) enemyBall = ball;
                }
            }

            originPoint = hit.point;
            direction = Vector3.Reflect(direction, hit.normal);
        }

        _lineRenderer.SetPositions(points);
    }
}
