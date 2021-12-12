using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Object for storing details about abilities
/// </summary>
[CreateAssetMenu(fileName="AbilityItem", menuName = "Abilities/AbilityDetails", order = 0)]
public class AbilityDetails : ScriptableObject
{
    public string abilityName, code;
    [TextArea] public string description;
    
    public bool unlocked, inUse;
    
    [Tooltip("The duration (in seconds) of the ability when activated.")]
    public float duration = 10f;
    
    [Tooltip("The cooldown (in seconds) for the ability. Used at start of game and once " +
             "an ability is finished being used (after duration).")]
    public float cooldown;

    [ColorUsage(true, false)] 
    public Color chargeBarColor= Color.yellow;
    
    
    // Extension stuff
    [Header("Extension Stuff (Not Used Yet)"), SerializeField]
    private int unlockLevel = -1;
    
    /// <summary>
    /// NOT IMPLEMENTED YET
    /// </summary>
    /// <param name="playerLevel"></param>
    /// <returns></returns>
    public bool UnlockAbility(int playerLevel)
    {
        // Do something in PlayerPrefs
        unlocked = playerLevel >= unlockLevel;
        return unlocked;
    }

    /// <summary>
    /// Returns if the ability is in use
    /// </summary>
    /// <param name="useItem">Boolean value for if you want to use the item</param>
    /// <returns>True if ability is in use, otherwise false</returns>
    public bool SetActive(bool useItem)
    {
        inUse = false;
        if (unlocked) inUse = useItem;
        // Do something in PlayerPrefs  
        return inUse;
    }

    /// <summary>
    /// Sets the code automatically
    /// </summary>
    private void OnValidate()
    {
        code = "ability_" + abilityName.ToLower().Replace(' ', '_');
    }
}
