using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("LoadingMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
