//By ALBERT Esteban
using UnityEngine;

public class GameSpeedMultiplier : MonoBehaviour
{
    public void SetGameSpeed(int speed)
    {
        if (speed < 0)
        {
            speed = 0;
        }
        Time.timeScale = speed;
    }
}
