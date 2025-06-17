using UnityEngine;

public class DestroyBlocks : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == ("FallingWall"))
        {
            Destroy(collision.gameObject);
        }
    }
}
