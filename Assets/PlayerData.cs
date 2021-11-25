using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;
    public int amountOfMoney;
    public StoreItem currentTrailData;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        amountOfMoney = PlayerPrefs.GetInt("playerMoney");
    }

    public void BuyItem(string code, int price)
    {
        amountOfMoney -= price;
        // Player prefs to remember "code" has been bought
        PlayerPrefs.SetInt("playerMoney", amountOfMoney);
    }

    public void ChangeTrailTo(StoreItem item)
    {
        currentTrailData.isActiveTrail = false;
        currentTrailData = item;
        currentTrailData.isActiveTrail = true;
    }
}
