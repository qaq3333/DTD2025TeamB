using System;
using System.Collections;
using System.Collections.Generic;
using Gamekit2D;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SceneName]
    public string SceneToLoad;

    public void GamePlay()
    {
        SceneManager.LoadScene(SceneToLoad);
        Time.timeScale = 1f;
    }
    public void GameQuit()
    {
        Application.Quit();
    }


}
