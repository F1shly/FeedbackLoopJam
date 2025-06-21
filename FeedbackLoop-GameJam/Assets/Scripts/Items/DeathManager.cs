using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DeathManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject DeathSprtie;
    public SpriteRenderer PlayerSprite;
    public Material deathMat;

    public GameObject Exit;

    public DropDown drop;

    public GameObject Counter;
    public void HasDied()
    {
        Exit.SetActive(false);

        PlayerSprite.material = deathMat;
        Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        Player.GetComponent<Movement>().enabled = false;
        DeathSprtie.SetActive(true);

        drop.dropScreen = true;
        drop.Died = true;

        Counter.SetActive(true);
        Counter.GetComponent<Counter>().OnPlayerDeath();

        StartCoroutine(LetPlayerLook());
    }
    IEnumerator LetPlayerLook()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
}
