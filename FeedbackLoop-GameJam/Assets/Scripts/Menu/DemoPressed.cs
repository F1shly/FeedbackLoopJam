using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DemoPressed : MonoBehaviour
{
    public void Clicked()
    {
        SceneManager.LoadScene(2);
    }
}
