using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Exit : MonoBehaviour
{
    public BoxManager boxmanager;
    public Entrence entrence;
    public DropDown drop;
    public Collider2D playerObj;
    public GameObject Player;
    public GameObject playerSprite;
    public int sceneNumber = 1;
    public GameObject[] lasers;
    public GameObject counter,counter1, introTxt, guides;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == playerObj.GetComponent<BoxCollider2D>())
        {
            drop.OnWin();
            Player.GetComponent<Movement>().enabled = false;
            StartCoroutine(DelaySpriteVanish());
            lasers = GameObject.FindGameObjectsWithTag("LaserBeam");
        }
    }
    public void DelayWin()
    {
        boxmanager.OnWin();
        StartCoroutine(DelayDelay());
    }
    IEnumerator DelayDelay()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(guides);
        boxmanager.ResetBoxes();
        foreach (var item in lasers)
        {
            LaserAwake laserAwake = item.GetComponent<LaserAwake>();
            laserAwake.ResetBeam();
        }
        counter.GetComponent<Counter>().NextNumber();
        counter1.GetComponent<Counter>().NextNumber();
        introTxt.SetActive(false);
    }
    IEnumerator DelaySpriteVanish()
    {
        yield return new WaitForSeconds(0.2f);
        playerSprite.GetComponent<SpriteRenderer>().enabled = false;
    }
}
