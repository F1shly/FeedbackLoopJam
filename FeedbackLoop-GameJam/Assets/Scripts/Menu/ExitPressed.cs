using UnityEngine;
using System.Collections;

public class ExitPressed : MonoBehaviour
{
    public GameObject Actived, Inactived;
    public AudioSource audioSource;
    public void Hovered()
    {
        Actived.SetActive(true);
        Inactived.SetActive(false);
    }
    private void FixedUpdate()
    {
        Actived.SetActive(false);
        Inactived.SetActive(true);
    }
    public void Clicked()
    {
        StartCoroutine(DelayChangeover());
    }
    IEnumerator DelayChangeover()
    {
        audioSource.Play();
        yield return new WaitForSeconds(0.2f);
        Application.Quit();
    }
}
