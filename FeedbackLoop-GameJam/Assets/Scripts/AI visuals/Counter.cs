using UnityEngine;

public class Counter : MonoBehaviour
{
    public GameObject[] LeftNumber, RightNumber;
    public int LvlNum = 0;
    public GameObject IntroObj;
    public bool ProgressTracker;
    public bool MenuDisplay;
    public int topScore;
    private void Awake()
    {
        topScore = PlayerPrefs.GetInt("HighScore");
        Debug.Log(topScore);
        if (MenuDisplay)
        {
            NextNumber();
        }
    }
    public void NextNumber()
    {
        topScore = PlayerPrefs.GetInt("HighScore");
        Debug.Log(topScore);
        if (IntroObj != null)
        {
            IntroObj.SetActive(false);
        }

        if(!MenuDisplay)
        {
            LvlNum += 1;
            for (int i = 0; i < 10; i++)
            {
                if (i == LvlNum % 10)
                {
                    RightNumber[i].SetActive(true);
                }
                else
                {
                    RightNumber[i].SetActive(false);
                }
            }
            for (int i = 0; i < 10; i++)
            {
                if (i == LvlNum / 10)
                {
                    LeftNumber[i].SetActive(true);
                }
                else
                {
                    LeftNumber[i].SetActive(false);
                }
            }
            if (ProgressTracker)
            {
                if (LvlNum > PlayerPrefs.GetInt("HighScore"))
                {
                    PlayerPrefs.SetInt("HighScore", LvlNum);
                    PlayerPrefs.Save(); // Ensure it's persisted to disk
                }
            }
        }

        if(MenuDisplay)
        {
            for (int i = 0; i < 10; i++)
            {
                if (i == PlayerPrefs.GetInt("HighScore") % 10)
                {
                    RightNumber[i].SetActive(true);
                }
                else
                {
                    RightNumber[i].SetActive(false);
                }
            }
            for (int i = 0; i < 10; i++)
            {
                if (i == PlayerPrefs.GetInt("HighScore") / 10)
                {
                    LeftNumber[i].SetActive(true);
                }
                else
                {
                    LeftNumber[i].SetActive(false);
                }
            }
        }
    }
}
