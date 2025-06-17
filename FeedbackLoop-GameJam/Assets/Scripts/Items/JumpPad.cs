using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public GameObject playerObj;
    public float jumpStrength = 14f;
    private void Awake()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision == playerObj.GetComponent<BoxCollider2D>())
        {
            Debug.Log("onPad");
            playerObj.GetComponent<Movement>().OnJumpPad();
        }
    }
}
