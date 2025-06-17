using UnityEngine;
using System.Collections;
public class AIAnim : MonoBehaviour
{
    public Animator anim;
    public GameObject cam;
    public Inputs inputs;
    public Movement movement;

    public GameObject musicBox;

    public GameObject[] walls;

    public GameObject Screen;

    private void Awake()
    {
        StartCoroutine(Blink());
        movement = inputs.gameObject.GetComponent<Movement>();
        walls = GameObject.FindGameObjectsWithTag("FallingWall");
    }

    IEnumerator Blink()
    {
        yield return new WaitForSeconds(Random.Range(10, 20));
        anim.SetBool("Blink", true);
        StartCoroutine(Blink());
    }
    
    public void OpenAnimation()
    {
        anim.SetTrigger("FirstInteraction");
        musicBox.GetComponent<Music>().PlayMusic();
        StartCoroutine(Shake());
        movement.enabled = false;
        movement.RunningTracker.SetActive(false);
        inputs.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 12, ForceMode2D.Impulse);
        inputs.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 12, ForceMode2D.Impulse);
        Screen.SetActive(true);
        foreach (var item in walls)
        {
            item.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        }
    }
    IEnumerator Shake()
    {
        Vector3 startPosition = cam.transform.position;
        float elapsedTime = 0f;

        while(elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            cam.transform.position = startPosition + Random.insideUnitSphere * 0.3f;
            yield return null;
        }
        cam.transform.position = startPosition;
        inputs.Jumping = false;
        movement.enabled = true;
    }
}
