using UnityEngine;

public class Entrence : MonoBehaviour
{
    public GameObject playerObj;
    public GameObject playerSprite;

    private void Awake()
    {
        OnWin();
    }
    public void OnWin()
    {
        playerObj.transform.position = transform.position;
        playerObj.GetComponent<Movement>().enabled = true;
        playerSprite.GetComponent<SpriteRenderer>().enabled = true;
    }
}
