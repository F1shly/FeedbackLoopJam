using UnityEngine;
using System.Collections;
using System.Linq;

public class FallingBox : MonoBehaviour
{
    public bool Static;
    public Animator animator;
    float duration = 1f;
    private void Awake()
    {
        if(!Static)
        {
            StartCoroutine(Grow());
        }
    }
    IEnumerator Grow()
    {
        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            float scaleFactor = Mathf.Clamp01((Time.time - startTime) / duration);
            transform.localScale = new Vector2(scaleFactor, scaleFactor);
            yield return null;
        }

        transform.localScale = new Vector2(1, 1);
        GetComponent<Rigidbody2D>().AddForce(Vector2.down, ForceMode2D.Impulse);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        animator.enabled = true;
        StartCoroutine(WaitAndDestroy());
    }

    public string animationName;
    IEnumerator WaitAndDestroy()
    {
        animator.Play(animationName);
        float length = animator.runtimeAnimatorController.animationClips
            .First(clip => clip.name == animationName).length;

        yield return new WaitForSeconds(length);
        if(Static)
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
