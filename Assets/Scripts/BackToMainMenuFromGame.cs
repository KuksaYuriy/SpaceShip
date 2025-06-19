using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenuFromGame : MonoBehaviour
{
    public bool isMenuOpen = false;
    public GameObject doUWantToGetBackToMainMenuPanel;

    void Start()
    {
        doUWantToGetBackToMainMenuPanel.SetActive(false);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            doUWantToGetBackToMainMenuPanel.SetActive(true);
            isMenuOpen = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }    
    }

    public void YesButtonFunc()
    {
        SceneManager.LoadScene(0);
    }

    public void NoButtonFunc()
    {
        doUWantToGetBackToMainMenuPanel.SetActive(false);
        isMenuOpen = false;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
