using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    void Awake()
    {
        GameUtil.pauseGame();
    }
    public void Restart()
    {
        StartCoroutine(closeGameOverScene());
    }

    IEnumerator closeGameOverScene()
    {
        // continueGame();
        AsyncOperation load = SceneManager.UnloadSceneAsync("GameOverScene"); ;
        yield return load;
    }
}
