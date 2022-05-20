using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntityAim : MonoBehaviour
{
    private EntityMovement _movement = null;
    private EntityContainer _container = null;
    private TurnSwitcher _turnSwitcher = null;

    private void Awake()
    {
        _movement = GetComponent<EntityMovement>();
    }

    private void Start()
    {
        _container = FindObjectOfType<EntityContainer>();
        _turnSwitcher = FindObjectOfType<TurnSwitcher>();
    }

    public void Aim()
    {
        Vector3[] path = CalculatePath();
        foreach (Vector3 point in path)
        {
            Debug.Log(point);
            Debug.DrawRay(point, Vector3.up, Color.red, 10f);
        }

        float pathLength = 0;
        for (int i = 0; i < path.Length - 1; i++)
        {
            pathLength += Vector3.Distance(path[i], path[i + 1]);
        }

        _movement.MoveAlongPath(path, pathLength / 30);
        _turnSwitcher.PrepareToSwitch();
    }

    private Vector3[] CalculatePath()
    {
        foreach (Entity entity in _container.PlayerEntities)
        {
            Transform entityTransform = entity.transform;
            Ray ray = new Ray(transform.position, entityTransform.position - transform.position);
            if (Physics.SphereCast(ray, 1.5f, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent<Entity>(out Entity detectedEntity) == false)
                {
                    return CalculateAdvancedPath();
                }
                else
                {
                    if (detectedEntity.IsMine)
                    {
                        return new Vector3[2] { transform.position, entityTransform.position };
                    }
                    else
                    {
                        return CalculateAdvancedPath();
                    }
                }
            }
            else return null;
        }
        return null;
    }

    private Vector3[] CalculateAdvancedPath()
    {
        transform.Rotate(Vector3.up * Random.Range(0, 360));

        int reflectionsCount = 10;
        int multiplier = 1;
        if (Random.Range(0, 2) == 0) multiplier = -1;

        Vector3[] longestPath = new Vector3[reflectionsCount];
        float longestPathLength = 0;

        for (int i = 360; i > 0; i -= 5)
        {
            Vector3[] path = new Vector3[reflectionsCount];
            float pathLength = 0;

            Vector3 originPoint = transform.position;
            Vector3 direction = transform.forward;
            path[0] = transform.position;
            for (int j = 1; j < reflectionsCount; j++)
            {
                Ray ray = new Ray(originPoint, direction);

                if (Physics.SphereCast(ray, 1.5f, out RaycastHit hit))
                {
                    path[j] = hit.point;

                    Debug.DrawLine(originPoint, hit.point, Color.black, 20f);
                    if (hit.collider.TryGetComponent(out Entity entity))
                    {
                        if (entity.IsMine) return path;
                        else break;
                    }
                    else
                    {
                        pathLength += Vector3.Distance(originPoint, hit.point);

                        direction = Vector3.Reflect(direction, hit.normal);
                        direction.y = 0;

                        originPoint = hit.point;
                    }
                }
            }

            if (pathLength > longestPathLength) longestPath = path;

            transform.Rotate(Vector3.up * (5f * multiplier));
        }

        return longestPath;
    }
}
