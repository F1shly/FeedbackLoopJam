using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    public Collider2D playerObj;
    DeathManager death;
    private void Awake()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
        death = GameObject.FindGameObjectWithTag("DeathManager").GetComponent<DeathManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == playerObj.GetComponent<BoxCollider2D>())
        {
            death.HasDied();
        }
    }
}
