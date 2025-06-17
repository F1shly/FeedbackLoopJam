using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15;
    public Vector2 TargetPos;

    void Update()
    {
        var step = Time.deltaTime * speed;
        transform.position = Vector2.MoveTowards(transform.position, TargetPos, step);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "conveyer")
        {
            Destroy(gameObject);
        }
    }
}
