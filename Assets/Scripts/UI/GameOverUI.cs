using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public struct GameOverSceneModel
{
    public static int enemiesKilled = 0;
    public static int coins = 0;
}

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinsText;
    [SerializeField] TextMeshProUGUI enemiesKilledText;

    void Awake()
    {
        GameUtil.pauseGame();
    }

    void Start()
    {
        coinsText.text = GameOverSceneModel.coins.ToString();
        enemiesKilledText.text = GameOverSceneModel.enemiesKilled.ToString();
    }
    public void Restart()
    {
        StartCoroutine(closeGameOverScene());
    }

    IEnumerator closeGameOverScene()
    {
        AsyncOperation load = SceneManager.UnloadSceneAsync("GameOverScene"); ;
        yield return load;
    }
}
