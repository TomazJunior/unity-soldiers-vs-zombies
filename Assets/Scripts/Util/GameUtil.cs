using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUtil
{
    internal static void pauseGame()
    {
        Debug.Log("pauseGame");
        Time.timeScale = 0;
    }
    internal static void continueGame()
    {
        Debug.Log("continueGame");
        Time.timeScale = 1;
    }
}
