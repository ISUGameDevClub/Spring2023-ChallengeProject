using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuOptions : MonoBehaviour
{
    public void StartGameButton() {
        SceneManager.LoadScene(1);
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }

    public void OptionsButton()
    {
        SceneManager.LoadScene(2);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
