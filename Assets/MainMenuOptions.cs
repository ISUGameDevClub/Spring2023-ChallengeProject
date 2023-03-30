using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuOptions : MonoBehaviour
{
    public void StartGameButton() {
        //Debug.Log("prints");
        SceneManager.LoadScene(1);
    }

    public void QuitGameButton()
    {
        //Debug.Log("prints");
        Application.Quit();
    }

    public void OptionsButton()
    {
        //Debug.Log("prints");
        SceneManager.LoadScene(2);
    }
}
