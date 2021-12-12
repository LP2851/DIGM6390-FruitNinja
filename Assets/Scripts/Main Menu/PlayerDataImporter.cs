using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Class that handles the players data
/// </summary>
public class PlayerDataImporter : MonoBehaviour
{
    public static PlayerDataImporter instance;

    // References to playerData
    public int amountOfMoney => playerData.amountOfMoney;
    public List<string> ownedItems => playerData.ownedItems;
    public int highScore => playerData.highScore;

    // Class that handles getting PlayerPrefs data
    private PlayerData playerData;
    
    // Data for player's trail
    public StoreItem currentTrailData;
    
    // Player's trail
    public GameObject trail;
    
    public AbilityDetails currentAbility;

    private void Start()
    {
        instance = this;
        playerData = new PlayerData();
        playerData.ImportData();

        // Checks against each item to see if it is the trail the player has equipped.
        foreach (StoreItem item in StoreData.instance.storeItems)
            IsCurrentTrail(item);

        foreach (AbilityDetails a in StoreData.instance.abilities)
        {
            if (playerData.currentAbility == a.code)
            {
                a.inUse = true;
                currentAbility = a;
            }
            else
            {
                a.inUse = false;
            }
        }
    }
    

    /// <summary>
    /// Buys the item for the player.
    /// </summary>
    /// <param name="code">String code for the item.</param>
    /// <param name="price">Price of the item</param>
    public void BuyItem(string code, int price)
    {
        playerData.Buy(code, price);
    }

    /// <summary>
    /// Changes the trail of the player to the trail that is received
    /// </summary>
    /// <param name="item">The data for the trail that is going to be used.</param>
    public void ChangeTrailTo(StoreItem item)
    {
        // Sets the data for the old trail to inactive.
        currentTrailData.isActiveTrail = false;
        // Sets data for new trail as active
        currentTrailData = item;
        currentTrailData.isActiveTrail = true;
        
        // Updates PlayerPrefs
        playerData.SetCurrentTrail(item.code);
        
        // Adds the trail to the player
        if(trail != null) Destroy(trail);
        trail = Instantiate(currentTrailData.trailOrParticleSystem, transform.position, Quaternion.identity);
        trail.transform.SetParent(transform);
    }

    /// <summary>
    /// Returns if the item is owned by the player. Updates the data for the store item as well as
    /// returning the result.
    /// </summary>
    /// <param name="item">The data for the item that is being checked</param>
    /// <returns>If the item is owned by the player</returns>
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
    
    /// <summary>
    /// Returns if the trail data passed is of the one in use.
    /// Also changes the trail to the passed trail if it is the one in use (for start of scene). 
    /// </summary>
    /// <param name="item">The data for the item</param>
    /// <returns>If the trail passed is the one in use.</returns>
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

    /// <summary>
    /// Resets all stored PlayerPrefs
    /// </summary>
    public void ResetPlayerPrefsData()
    {
        PlayerPrefs.DeleteAll();
        playerData = new PlayerData();
        playerData.ImportData();
    }
    
    /// <summary>
    /// Gives amount to player
    /// </summary>
    /// <param name="amount">Amount of money to give the player. </param>
    public void GiveMoney(int amount)
    {
        playerData.GiveMoney(amount);
    }

    /// <summary>
    /// Sets the high score of the player
    /// </summary>
    /// <param name="score">New high score</param>
    public void SetNewHighScore(int score)
    {
        playerData.SetNewHighScore(score);
    }

    public void SetAbilityTo(AbilityDetails ability)
    {
        currentAbility.SetActive(false);
        currentAbility = ability;
        currentAbility.SetActive(true);

        playerData.SetAbility(ability.code);
    }
    

    /// <summary>
    /// Class used to store all player data retrieved from PlayerPrefs. 
    /// </summary>
    [Serializable]
    private class PlayerData
    {
        public int amountOfMoney { get; private set; }
        public int highScore { get; private set; }
        public string currentPlayerTrail { get; private set; }
        
        public string currentAbility { get; private set; }
        
        public List<string> ownedItems = new List<string>();

        /// <summary>
        /// Imports all of the data for the player from PlayerPrefs and stores results.
        /// </summary>
        public void ImportData()
        {
            amountOfMoney = PlayerPrefs.GetInt("playerMoney", 0);
            currentPlayerTrail = PlayerPrefs.GetString("playerTrail", "default_trail");
            highScore = PlayerPrefs.GetInt("highScore", 0);
            // Owned items is a string formatted with item codes with commas in between.
            ownedItems = PlayerPrefs.GetString("ownedItems", "default_trail").Split(',').ToList();
            // If nothing is found in PlayerPrefs then added the default trail to the owned items and sets default trail 
            // as current trail.
            if (ownedItems.Count == 1)
            {
                PlayerPrefs.SetString("ownedItems", "default_trail");
                SetCurrentTrail("default_trail");
            }

            currentAbility = PlayerPrefs.GetString("playerAbility", "ability_shockwave");
            if (currentAbility.Equals("ability_shockwave"))
            {
                PlayerPrefs.SetString("playerAbility", "ability_shockwave");
            }
            
            PlayerPrefs.Save();
        }

        /// <summary>
        /// Buys an item and removes the cost from the players balance
        /// </summary>
        /// <param name="item">The item code for the item</param>
        /// <param name="cost">The cost of the item</param>
        public void Buy(string item, int cost)
        {
            amountOfMoney -= cost;
            ownedItems.Add(item);
            PlayerPrefs.SetInt("playerMoney", amountOfMoney);
            PlayerPrefs.SetString("ownedItems", PlayerPrefs.GetString("ownedItems") + "," + item);
            PlayerPrefs.Save();
        }
        
        /// <summary>
        /// Adds amount of money to the players balance
        /// </summary>
        /// <param name="amount">Amount of money to be added</param>
        public void GiveMoney(int amount)
        {
            amountOfMoney += amount;
            PlayerPrefs.SetInt("playerMoney", amountOfMoney);
            PlayerPrefs.Save();
        }

        /// <summary>
        /// Sets the current trail for the player
        /// </summary>
        /// <param name="trailCode">The item code for the trail</param>
        public void SetCurrentTrail(string trailCode)
        {
            currentPlayerTrail = trailCode;
            PlayerPrefs.SetString("playerTrail", trailCode);
            PlayerPrefs.Save();
        }

        /// <summary>
        /// Sets the player's highest score
        /// </summary>
        /// <param name="score">New high score.</param>
        public void SetNewHighScore(int score)
        {
            highScore = score;
            PlayerPrefs.SetInt("highScore", highScore);
            PlayerPrefs.Save();
        }

        public void SetAbility(string abilityCode)
        {
            currentAbility = abilityCode;
            PlayerPrefs.SetString("playerAbility", abilityCode);
            PlayerPrefs.Save();
        }
        
    }
    
}

