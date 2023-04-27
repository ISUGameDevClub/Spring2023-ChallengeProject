using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialWindow;
    [SerializeField] private Text text;
    private bool first = true;


    [SerializeField] private List<string> messages = new List<string>();
    private int curMessage = 0;

    public void OpenWindow()
    {
        if (!first)
        {

        }
    }

    public void CloseWindow()
    {
        tutorialWindow.SetActive(false);
    }


}
