using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Store Item", menuName = "In-Game Store/Store Item", order = 0)]
public class StoreItem : ScriptableObject
{
    public string itemName, code;
    public int price;

    public bool playerHasBought, isHidden, isActiveTrail;

    public GameObject trailOrParticleSystem;

    public void UnHide()
    {
        isHidden = false;
    }

    public void Buy()
    {
        playerHasBought = true;
        PlayerDataImporter.instance.BuyItem(code, price);
    }

    public void SetActive()
    {
        PlayerDataImporter.instance.ChangeTrailTo(this);
    }


}
