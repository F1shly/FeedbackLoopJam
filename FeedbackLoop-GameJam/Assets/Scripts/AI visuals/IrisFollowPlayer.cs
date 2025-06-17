using UnityEngine;

public class IrisFollowPlayer : MonoBehaviour
{
    public GameObject Target;
    public GameObject Iris;
    public float followDistance = 1.7f;
    public LayerMask Layer;
    private void Update()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, Target.transform.position - transform.position, Mathf.Infinity, Layer);
        if(hit.distance > followDistance)
        {
            var desiredDistance = followDistance;
            Vector2 V2OfPosition = new Vector2(transform.position.x, transform.position.y);
            var direction = (hit.point - V2OfPosition).normalized;

            Iris.transform.position = new Vector3(transform.position.x + direction.x * desiredDistance, transform.position.y + direction.y * desiredDistance, transform.position.z);
        }
        else
        {
            Iris.transform.position = hit.point;
        }
    }
}
