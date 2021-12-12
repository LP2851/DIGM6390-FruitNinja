using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lists all of the items in the game that can be bought at the store.
/// </summary>
public class StoreData : MonoBehaviour
{
    public static StoreData instance;
    
    [Header("Store Trail Items")]
    public StoreItem[] storeItems;

    [Header("Ability Data")] public AbilityDetails[] abilities;

    private void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
    }
}
