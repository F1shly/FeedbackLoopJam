using UnityEngine;

public class Counter : MonoBehaviour
{
    public GameObject[] LeftNumber, RightNumber;
    public int LvlNum = 0;
    public GameObject IntroObj;
    public void NextNumber()
    {
        if(IntroObj != null)
        {
            IntroObj.SetActive(false);
        }

        LvlNum += 1;
        for (int i = 0; i < 10; i++)
        {
            if(i == LvlNum % 10)
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
    }
}
