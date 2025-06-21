using System;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public GameObject[] LeftNumber, RightNumber;
    public int LvlNum = 0;
    public GameObject IntroObj;
    public bool ProgressTracker;
    public bool MenuDisplay;
    public int topScore;
    public int secondPlace;
    public int thirdPlace;
    public int fourthPlace;
    public int fithPlace;
    public int ScorePosition;

    private void Awake()
    {
        topScore = PlayerPrefs.GetInt("HighScore");
        secondPlace = PlayerPrefs.GetInt("SecondScore");
        thirdPlace = PlayerPrefs.GetInt("ThirdScore");
        fourthPlace = PlayerPrefs.GetInt("FourthScore");
        fithPlace = PlayerPrefs.GetInt("FithScore");
        Debug.Log(topScore);
        if (MenuDisplay)
        {
            NextNumber();
        }
    }
    public void NextNumber()
    {
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
                //if (LvlNum > PlayerPrefs.GetInt("HighScore"))
                //{
                //    PlayerPrefs.SetInt("HighScore", LvlNum);
                //    PlayerPrefs.Save(); // Ensure it's persisted to disk
                //}
            }
        }


        if(MenuDisplay)
        {
            int[] scores = {topScore, secondPlace, thirdPlace, fourthPlace, fithPlace};
            int a = ScorePosition;
            for (int i = 0; i < 10; i++)
            {
                if (i == scores[a] % 10)
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
                if (i == scores[a] / 10)
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

    public void OnPlayerDeath()
    {
        int[] scoreArray = {topScore, secondPlace, thirdPlace, fourthPlace, fithPlace, LvlNum};

        Array.Sort(scoreArray);
        Array.Reverse(scoreArray);

        PlayerPrefs.SetInt("HighScore", scoreArray[0]);
        PlayerPrefs.SetInt("SecondScore", scoreArray[1]);
        PlayerPrefs.SetInt("ThirdScore", scoreArray[2]);
        PlayerPrefs.SetInt("FourthScore", scoreArray[3]);
        PlayerPrefs.SetInt("FithScore", scoreArray[4]);

        PlayerPrefs.Save();
    }    
}
