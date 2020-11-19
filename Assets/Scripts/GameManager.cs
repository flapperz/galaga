using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    public static int Score = 0;

    public static void ResetScore()
    {
        Score = 0;
    }

    public static void GameOver()
    {
        SceneManager.LoadScene("EndScene");
    }

    public static void ReGame()
    {
        ResetScore();
        SceneManager.LoadScene("MainScene");
    }

}
