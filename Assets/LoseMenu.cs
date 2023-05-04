using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoseMenu : MonoBehaviour
{
    public TextMeshProUGUI bestMoneyText;
    public TextMeshProUGUI bestRoundText;
    public TextMeshProUGUI totalMoneyText;
    public TextMeshProUGUI currentRoundText;

    void Start()
    {
        // Load the saved values from PlayerPrefs
        int bestMoney = PlayerPrefs.GetInt("BestMoney");
        int bestRound = PlayerPrefs.GetInt("BestRound");
        int totalMoney = PlayerPrefs.GetInt("Money");
        int currentRound = PlayerPrefs.GetInt("Round");

        // Set the text of the TextMeshPro Text components
        bestMoneyText.text = "$ " + bestMoney.ToString();
        bestRoundText.text = "Months Worked " + bestRound.ToString();
        totalMoneyText.text = "$ " + totalMoney.ToString();
        currentRoundText.text = "Months Worked  " + currentRound.ToString();
    }
}
