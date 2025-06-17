using UnityEngine;

public class FirstPlay : MonoBehaviour
{
    public GameObject Player;
    public GameObject AEye;
    public AudioSource explosion;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        AEye.GetComponent<AIAnim>().OpenAnimation();
        explosion.Play();
    }
}
