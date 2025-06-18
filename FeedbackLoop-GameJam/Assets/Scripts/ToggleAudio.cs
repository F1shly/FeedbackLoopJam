using UnityEngine;
using UnityEngine.SceneManagement;

public class ToggleAudio : MonoBehaviour
{
    public bool on;
    public GameObject[] audioObjs;
    public GameObject OnState, OffState;
    public bool backButton;

    private void Awake()
    {
        if(!backButton)
        {
            Switch();
        }
    }
    public void Switch()
    {
        if (backButton)
        {
            SceneManager.LoadScene(0);
        }
        on = !on;
        if(on && !backButton)
        {
            OnState.SetActive(true);
            OffState.SetActive(false);
            foreach (var item in audioObjs)
            {
                item.GetComponent<AudioSource>().mute = false;
            }
        }
        else if(!on && !backButton)
        {
            OnState.SetActive(false);
            OffState.SetActive(true);
            foreach (var item in audioObjs)
            {
                item.GetComponent<AudioSource>().mute = true;
            }
        }
    }
}
