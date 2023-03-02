using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private float totalMoney;
    [SerializeField] private float startingCash;

    private void Start()
    {
        totalMoney = startingCash;
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
        totalMoney -= subtractedMoney;-------------------------------------+
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
