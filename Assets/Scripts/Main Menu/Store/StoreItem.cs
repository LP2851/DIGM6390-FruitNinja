using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// StoreItem scriptable object that is used to define something sold in the store.
/// </summary>
[CreateAssetMenu(fileName = "Store Item", menuName = "In-Game Store/Store Item", order = 0)]
public class StoreItem : ScriptableObject
{
    public string itemName, code;
    public int price;

    public bool playerHasBought, isHidden, isActiveTrail;

    public GameObject trailOrParticleSystem;

    /// <summary>
    /// Code to un-hide an item in the store (possible feature expansion). 
    /// </summary>
    public void UnHide()
    {
        isHidden = false;
    }

    /// <summary>
    /// Buys this item using the players money.
    /// </summary>
    public void Buy()
    {
        playerHasBought = true;
        PlayerDataImporter.instance.BuyItem(code, price);
    }

    /// <summary>
    /// Sets the item as active (in use).
    /// </summary>
    public void SetActive()
    {
        PlayerDataImporter.instance.ChangeTrailTo(this);
    }


}
