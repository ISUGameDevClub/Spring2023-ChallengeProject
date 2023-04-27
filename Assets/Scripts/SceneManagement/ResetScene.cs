using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    Scene curScene;

    private void Start()
    {
        curScene = SceneManager.GetActiveScene();
    }

    private void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(curScene.buildIndex);
        }
    }

}
