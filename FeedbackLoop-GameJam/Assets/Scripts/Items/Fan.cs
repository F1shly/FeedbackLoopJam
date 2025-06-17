using UnityEngine;

public class Fan : MonoBehaviour
{
    public GameObject playerObj;
    public float fanSpeed;
    private void Awake()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");

        if (fanSpeed < 0)
        {
            fanSpeed = -1;
        }
        else
        {
            fanSpeed = 1;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision == playerObj.GetComponent<BoxCollider2D>())
        {
            Debug.Log("onFan");
            playerObj.GetComponent<Rigidbody2D>().linearVelocityY += fanSpeed;
        }
    }
}
