using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBossMovement : MonoBehaviour {

    public float skinWidth = 0.01f;
    public Transform target;
    public List<GameObject> obstacleTransforms = new List<GameObject>();
    public LayerMask whatIsObstacle;
    public GameObject edgePointPrefab;
    public bool playermoved = false;
    public Transform instantiatedEdges;

    private Collider2D collider2d;

    public List<Vector2> edgePoints1 = new List<Vector2>();
    public List<Vector2> edgePoints2 = new List<Vector2>();

	// Use this for initialization
	void Start () {
        collider2d = GetComponent<Collider2D>();
        obstacleTransforms.Add(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        if (playermoved)
        {
            foreach (Transform instObj in instantiatedEdges)
            {
                instObj.GetComponent<DestroyObject>().Destroy();
            }
            FindVisiblePointTo(target, gameObject.transform.position);
            playermoved = false;
        }
    }

    private void FindVisiblePointTo(Transform target, Vector3 caster)
    {
        Vector2 direction = (target.position - caster).normalized;
        RaycastHit2D hitInfo = Physics2D.Raycast((Vector2)caster + CastingPosition(direction), direction, whatIsObstacle);
        Debug.DrawRay((Vector2)caster + CastingPosition(direction), direction, Color.cyan);

        if (hitInfo.collider.CompareTag("Player"))
        {
            Debug.Log("Player Found");
        }
        else
        {
            Vector3[] edgePoints = EdgePoints(hitInfo);
            edgePoints1.Add(edgePoints[0]);
            edgePoints2.Add(edgePoints[1]);
            for (int i = 0; i < edgePoints.Length; i++)
            {
                Instantiate(edgePointPrefab, edgePoints[i], Quaternion.identity, instantiatedEdges);
                FindVisiblePointTo(target, edgePoints[i]);
            }
        }
    }

    private void DebugCasting(Transform target, List<GameObject> obstacles)
    {
        Vector3 dir = DirectionSetter(target.position, transform.position);
        Debug.DrawRay(transform.position, dir);
        for (int i = 0; i < obstacleTransforms.Count; i++)
        {
            dir = DirectionSetter(target.position, obstacleTransforms[i].transform.position);
            Debug.DrawRay(obstacles[i].transform.position, dir, Color.red);
        }
    }

    private Vector3 DirectionSetter(Vector3 targetPosition, Vector3 targetFrom)
    {
        Vector3 distance = Vector3.down;
        if(targetPosition != null)
        {
            distance = targetPosition - targetFrom;
        }
        return distance.normalized;
    }

    private Vector2 CastingPosition(Vector2 direction)
    {
        float radiusX = collider2d.bounds.size.x;
        float radiusY = collider2d.bounds.size.y;

        if (direction.x > 0)
        {
            if (direction.y > 0)
            {
                return new Vector2(collider2d.bounds.extents.x + skinWidth, collider2d.bounds.extents.y + skinWidth);
            }
            else
            {
                return new Vector2(collider2d.bounds.extents.x + skinWidth, -collider2d.bounds.extents.y - skinWidth);
            }
        }
        else
        {
            if (direction.y > 0)
            {
                return new Vector2(-collider2d.bounds.extents.x - skinWidth, collider2d.bounds.extents.y + skinWidth);
            }
            else
            {
                return new Vector2(-collider2d.bounds.extents.x - skinWidth, -collider2d.bounds.extents.y - skinWidth);
            }
        }
    }

    private Vector3[] EdgePoints(RaycastHit2D hitInfo)
    {
        if (hitInfo.collider.bounds.extents.x > hitInfo.collider.bounds.extents.y)
        {
            Vector3 edgePoint1 = new Vector2(hitInfo.collider.bounds.center.x + hitInfo.collider.bounds.extents.x + collider2d.bounds.extents.x, hitInfo.collider.bounds.center.y);
            Vector3 edgePoint2 = new Vector2(hitInfo.collider.bounds.center.x - hitInfo.collider.bounds.extents.x - collider2d.bounds.extents.x, hitInfo.collider.bounds.center.y);

            Vector3[] returnTransforms = { edgePoint1, edgePoint2 };
            return returnTransforms;
        }
        else
        {
            Vector3 edgePoint1 = new Vector2(hitInfo.collider.bounds.center.x, hitInfo.collider.bounds.center.y + hitInfo.collider.bounds.extents.y + collider2d.bounds.extents.y);
            Vector3 edgePoint2 = new Vector2(hitInfo.collider.bounds.center.x, hitInfo.collider.bounds.center.y - hitInfo.collider.bounds.extents.y - collider2d.bounds.extents.y);
            Vector3[] returnTransforms = { edgePoint1, edgePoint2 };
            return returnTransforms;
        }
    }

}
