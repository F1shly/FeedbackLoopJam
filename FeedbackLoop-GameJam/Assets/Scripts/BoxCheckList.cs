using UnityEngine;

public class BoxCheckList : MonoBehaviour
{
    public Collider2D RunningTracker, JumpingTracker, FastFallTracker;
    public bool activatedRun = false;
    public bool activatedJump = false;
    public bool activatedFall = false;
    public bool activated;
    public bool occupied;

    public bool playerInBox;

    public float timer;
    public bool activatedStay = false;

    public int activeLayer, deactiveLayer;
    public Material Run, Jump, Fall, Stay, def;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!occupied)
        {
            if (collision == FastFallTracker && !activated)
            {
                activatedFall = true;
                activated = true;
                gameObject.layer = activeLayer;
                GetComponent<SpriteRenderer>().material = Fall;
            }
            if (collision == RunningTracker)
            {
                activatedRun = true;
                activatedFall = false;
                activated = true;
                gameObject.layer = activeLayer;
                GetComponent<SpriteRenderer>().material = Run;
            }
            if (collision == JumpingTracker)
            {
                activatedJump = true;
                activatedFall = false;
                activated = true;
                gameObject.layer = activeLayer;
                GetComponent<SpriteRenderer>().material = Jump;
            }
        }
        if (collision == RunningTracker || collision == JumpingTracker || collision == FastFallTracker)
        {
            playerInBox = true;
        }
    }
    private void Update()
    {
        if(activated && playerInBox && !occupied)
        {
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                activatedStay = true;
                GetComponent<SpriteRenderer>().material = Stay;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == RunningTracker || collision == JumpingTracker|| collision == FastFallTracker)
        {
            playerInBox = false;
        }
    }
}
