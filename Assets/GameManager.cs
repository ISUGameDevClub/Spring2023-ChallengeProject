using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public BuildModeEnabler BME;
    public BoxSpawner boxSpawner;
    public MoneyManager moneyManager;

    public int currentRound = 0;

    public float factoryCost = 0;

    public float wageRatio = 1.0f;

    public bool isPlaying = false;
    public Dictionary<GameObject,int> workerAmount = new Dictionary<GameObject,int>();

    [SerializeField] private Text roundText;

    [SerializeField] private Text moneyText;
    [SerializeField] private Text negativeMoneySignText;
    private float wages;


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


        if(isPlaying && boxSpawner.noBoxesLeft)
        {
            EndRound();
        }

        if(moneyText.enabled)
        {
            moneyText.text = (wages + (factoryCost * currentRound)).ToString();
        }
    }
    void FixedUpdate()
    {   
        wages = 0;
        foreach (KeyValuePair<GameObject, int> entry in workerAmount)
        {
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

    void StartRound()
    {
        if(IsListOfListsEmpty(BME.minorGameObjectsList) && boxSpawner.noBoxesLeft)
        {
            BME.isBuildMode = false;
            BME.isEarseMode = false;

            currentRound++;
            boxSpawner.SetTimeline(currentRound);
            boxSpawner.spawnTimer = 0;

            moneyText.enabled = false;
            negativeMoneySignText.enabled = false;
            moneyManager.subtractMoney(wages + (factoryCost * currentRound));

        } else 
        {
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
    }
}
