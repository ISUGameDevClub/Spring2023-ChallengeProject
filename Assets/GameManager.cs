using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //events 
    public event Action roundStart;
    public event Action roundEnd;
    public event Action roundWin;
    public event Action roundLose;
    public event Action win;

    //not events
    public BuildModeEnabler BME;
    public BoxSpawner boxSpawner;
    public MoneyManager moneyManager;

    public int currentRound = 0;

    public float factoryCost = 0;

    public float wageRatio = 1.0f;

    public bool isPlaying = false;
    [SerializeField]
    public Dictionary<GameObject,int> workerAmount = new Dictionary<GameObject,int>();

    [SerializeField] private Text roundText;

    [SerializeField] private Text moneyText;
    [SerializeField] private Text negativeMoneySignText;
    public float wages;


    // Start is called before the first frame update
    void Start()
    {
        boxSpawner = FindObjectOfType<BoxSpawner>();
        moneyManager = FindObjectOfType<MoneyManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(roundText.enabled)
        {
            roundText.text = currentRound.ToString();
        }



        if(moneyText.enabled)
        {
            moneyText.text = (wages + (factoryCost * currentRound)).ToString();
        }
        if(isPlaying && boxSpawner.noBoxesLeft)
        {
            EndRound();
        }
    }
    void FixedUpdate()
    {   
        wages = 0;
        foreach (KeyValuePair<GameObject, int> entry in workerAmount)
        {
            Debug.Log(entry);
            wages += entry.Key.GetComponent<Worker>().cost * wageRatio * entry.Value;
        }  
    }

    public void StartRoundButton()
    {
        if(!isPlaying)
        {
            isPlaying = true;

            StartRound();
        }
    }

    public void StartRound()
    {
        Debug.Log("test start");
        if(IsListOfListsEmpty(BME.minorGameObjectsList) && (currentRound == 0 || boxSpawner.noBoxesLeft))
        {
            moneyManager.subtractMoney(wages + (factoryCost * currentRound));
            moneyText.enabled = false;
            negativeMoneySignText.enabled = false;

            BME.isBuildMode = false;
            BME.isEarseMode = false;

            currentRound++;
            boxSpawner.SetTimeline(currentRound);
            boxSpawner.spawnTimer = 0;

            roundStart?.Invoke();
        } else 
        {
            isPlaying = false;
            Debug.Log("failed" + BME.minorGameObjectsList.Count); 
        }

    }


    bool IsListOfListsEmpty<T>(List<List<T>> list)
{
    if (list == null || list.Count == 0)
    {
        return true;
    }

    foreach (var sublist in list)
    {
        if (sublist != null && sublist.Count > 0)
        {
            return false;
        }
    }

    return true;
}


    void EndRound()
    {
        isPlaying = false;
        moneyText.enabled = true;
        negativeMoneySignText.enabled = true;
        
        roundEnd?.Invoke();
        if(moneyManager.getMoney() < 0)
        {
            //Game Over
            Debug.Log("Game Over");
            roundLose?.Invoke();
            LoseScene("LoseScene");
        }if(currentRound > 20)
        {
            if (PlayerPrefs.GetInt("BestMoney") < ((int)moneyManager.totalMoneyMade)) PlayerPrefs.SetInt("BestMoney", ((int)moneyManager.totalMoneyMade));
            if (PlayerPrefs.GetInt("BestRound") < currentRound) PlayerPrefs.SetInt("BestRound", currentRound);
            PlayerPrefs.SetInt("Money", ((int)moneyManager.totalMoneyMade));
            PlayerPrefs.SetInt("Round", currentRound);
            win?.Invoke();
            SceneManager.LoadScene("Title Screen");
        }
        else{
            //Win
            Debug.Log("next Round");
            roundWin?.Invoke();
        }
     
    }


    public void LoseScene( string sceneName)
{
    // Save the integer value using PlayerPrefs
    if(PlayerPrefs.GetInt("BestMoney") < ((int)moneyManager.totalMoneyMade)) PlayerPrefs.SetInt("BestMoney", ((int)moneyManager.totalMoneyMade));
    if(PlayerPrefs.GetInt("BestRound") < currentRound) PlayerPrefs.SetInt("BestRound", currentRound);
    PlayerPrefs.SetInt("Money", ((int)moneyManager.totalMoneyMade));
    PlayerPrefs.SetInt("Round", currentRound);

    PlayerPrefs.Save();

    // Load the specified scene
    SceneManager.LoadScene(sceneName);
}
}
