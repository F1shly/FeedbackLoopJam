using UnityEngine;

public class ToggleAudio : MonoBehaviour
{
    public bool on;
    public GameObject[] audioObjs;
    public GameObject OnState, OffState;

    private void Awake()
    {
        Switch();
    }
    public void Switch()
    {
        on = !on;
        if(on)
        {
            OnState.SetActive(true);
            OffState.SetActive(false);
            foreach (var item in audioObjs)
            {
                item.GetComponent<AudioSource>().mute = false;
            }
        }
        else
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
