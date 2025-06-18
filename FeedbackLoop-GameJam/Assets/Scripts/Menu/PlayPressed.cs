using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayPressed : MonoBehaviour
{
    public GameObject Actived, Inactived;
    public AudioSource audioSource;
    public bool spacePressed;
    public void Hovered()
    {
        Actived.SetActive(true);
        Inactived.SetActive(false);
    }
    private void FixedUpdate()
    {
        if(!spacePressed)
        {
            Actived.SetActive(false);
            Inactived.SetActive(true);
        }
    }
    public void Clicked()
    { 
        StartCoroutine(DelayChangeover());
    }
    IEnumerator DelayChangeover()
    {
        audioSource.Play();
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(1);
    }
}
