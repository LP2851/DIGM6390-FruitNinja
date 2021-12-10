using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="AbilityItem", menuName = "Abilities/AbilityDetails", order = 0)]
public class AbilityDetails : ScriptableObject
{
    public string abilityName, code;

    [TextArea] public string description;
    
    [SerializeField] private bool unlocked, inUse;
    [Tooltip("The duration (in seconds) of the ability when activated.")]
    public float duration = 10f;
    [Tooltip("The cooldown (in seconds) for the ability. Used at start of game and once " +
             "an ability is finished being used (after duration).")]
    public float cooldown;

    // Extension stuff
    [Header("Extension Stuff (Not Used Yet)"), SerializeField]
    private int unlockLevel = -1;

    public bool UnlockAbility(int playerLevel)
    {
        // Do something in PlayerPrefs
        unlocked = playerLevel >= unlockLevel;
        return unlocked;
    }

    public bool SetActive(bool useItem)
    {
        inUse = false;
        if (unlocked) inUse = useItem;
        // Do something in PlayerPrefs  
        return inUse;
    }

    private void OnValidate()
    {
        code = "ability_" + abilityName.ToLower().Replace(' ', '_');
    }
}
