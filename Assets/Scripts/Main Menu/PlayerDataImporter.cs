using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerDataImporter : MonoBehaviour
{
    public static PlayerDataImporter instance;

    public int amountOfMoney => playerData.amountOfMoney;
    public List<string> ownedItems => playerData.ownedItems;

    //[SerializeField] 
    private PlayerData playerData;
    
    public StoreItem currentTrailData;

    public GameObject trail;

    private void Awake()
    {
        instance = this;
        playerData = new PlayerData();
        playerData.ImportData();

        foreach (StoreItem item in StoreData.instance.storeItems)
            IsCurrentTrail(item);
        
    }
    

    public void BuyItem(string code, int price)
    {
        playerData.Buy(code, price);
    }

    public void ChangeTrailTo(StoreItem item)
    {
        currentTrailData.isActiveTrail = false;
        currentTrailData = item;
        currentTrailData.isActiveTrail = true;

        playerData.SetCurrentTrail(item.code);
        
        if(trail != null) Destroy(trail);
        trail = Instantiate(currentTrailData.trailOrParticleSystem, transform.position, Quaternion.identity);
        trail.transform.SetParent(transform);
    }

    public bool IsOwned(StoreItem item)
    {
        if (ownedItems.Contains(item.code))
        {
            item.playerHasBought = true;
            return true;
        }

        item.playerHasBought = false;
        return false;
    }

    public bool IsCurrentTrail(StoreItem item)
    {
        if (playerData.currentPlayerTrail.Equals(item.code))
        {
            currentTrailData = item;
            ChangeTrailTo(item);
            return true;
        }

        item.isActiveTrail = false;
        return false;
    }

    public void ResetPlayerPrefsData()
    {
        PlayerPrefs.DeleteAll();
        playerData = new PlayerData();
        playerData.ImportData();
    }

    public void GiveMoney(int amount)
    {
        playerData.GiveMoney(amount);
    }
    


    [Serializable]
    private class PlayerData
    {
        public int amountOfMoney { get; private set; }
        public string currentPlayerTrail { get; private set; }
        
        public List<string> ownedItems = new List<string>();

        public void ImportData()
        {
            amountOfMoney = PlayerPrefs.GetInt("playerMoney", 0);
            currentPlayerTrail = PlayerPrefs.GetString("playerTrail", "default_trail");
            
            ownedItems = PlayerPrefs.GetString("ownedItems", "default_trail").Split(',').ToList();
            if (ownedItems.Count == 1)
            {
                PlayerPrefs.SetString("ownedItems", "default_trail");
                SetCurrentTrail("default_trail");
            }
            
            PlayerPrefs.Save();
        }

        public void Buy(string item, int cost)
        {
            amountOfMoney -= cost;
            ownedItems.Add(item);
            PlayerPrefs.SetInt("playerMoney", amountOfMoney);
            PlayerPrefs.SetString("ownedItems", PlayerPrefs.GetString("ownedItems") + "," + item);
            PlayerPrefs.Save();
        }

        public void GiveMoney(int amount)
        {
            amountOfMoney += amount;
            PlayerPrefs.SetInt("playerMoney", amountOfMoney);
            PlayerPrefs.Save();
        }

        public void SetCurrentTrail(string trailCode)
        {
            currentPlayerTrail = trailCode;
            PlayerPrefs.SetString("playerTrail", trailCode);
            PlayerPrefs.Save();
        }
        
    }
    
}

