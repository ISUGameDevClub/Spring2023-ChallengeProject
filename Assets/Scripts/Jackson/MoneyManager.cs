using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] public float totalMoney;
    [SerializeField] public float startingCash;
    [SerializeField] private Text moneyText;

    private void Start()
    {
        totalMoney = startingCash;
    }

    private void Update()
    {
        moneyText.text = totalMoney.ToString();
    }

    public float getMoney()
    {
        return totalMoney;
    }

    public void addMoney(float addedMoney)
    {
        totalMoney += addedMoney;
    }

    public void subtractMoney(float subtractedMoney)
    {
        totalMoney -= subtractedMoney;
    }

    public bool checkPrice(float price)
    {
        if (price <= totalMoney)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
