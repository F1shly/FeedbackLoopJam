using UnityEngine;

public class Conveyer : MonoBehaviour
{
    public GameObject playerObj;
    public float conveyerSpeed;
    private void Awake()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        conveyerSpeed = Random.Range(-1, 1);
        
        if(conveyerSpeed < 0)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
            transform.position = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);
            conveyerSpeed = -2;
        }
        else
        {
            conveyerSpeed = 2;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision == playerObj.GetComponent<BoxCollider2D>())
        {
            Debug.Log("onConveyer");
            playerObj.GetComponent<Movement>().extraSpeed += conveyerSpeed;
        }
    }
}
