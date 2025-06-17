using UnityEngine;

public class DropDown : MonoBehaviour
{
    public bool dropScreen;
    public float YPos;
    public int stage;
    public float speed;
    public float speedMultiplier;

    public bool Died;

    public Entrence entrence;
    public Exit exit;

    public void OnWin()
    {
        dropScreen = true;
        //drop
        //dounce
        //lift
    }

    private void Update()
    {
        if(dropScreen)
        {
            transform.position = new Vector2(transform.position.x, 9 + YPos);

            if(stage == 0)
            {
                speed += Time.deltaTime * speedMultiplier;
                YPos -= speed * Time.deltaTime;
                if (YPos <= -9)
                {
                    stage = 1;
                }
            }
            if (stage == 1)
            {
                speed -= Time.deltaTime * speedMultiplier * 11;
                YPos += speed * Time.deltaTime;
                if (speed <= 0)
                {
                    stage = 2;
                    speed = 0;
                }
            }
            if (stage == 2)
            {
                speed += Time.deltaTime * speedMultiplier * 2;
                YPos -= speed * Time.deltaTime;
                if (YPos <= -9)
                {
                    stage = 3;
                    YPos = -9;
                    speed = 0;
                    exit.DelayWin();
                }
            }
            if (stage == 3)
            {
                speed += Time.deltaTime;
                if(speed >= 0.25f && !Died)
                {
                    stage = 4;
                    speed = 0;
                }
            }
            if (stage == 4)
            {
                entrence.OnWin();
                speed += Time.deltaTime * speedMultiplier;
                YPos += speed * Time.deltaTime;
                if(YPos >= 0)
                {
                    dropScreen = false;
                    stage = 0;
                    YPos = 0;
                    speed = 0;
                }
            }
        }
    }
}
