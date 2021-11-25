using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Store Item", menuName = "In-Game Store/Store Item", order = 0)]
public class StoreItem : ScriptableObject
{
    public string name, code;
    public int price;

    public bool playerHasBought, isHidden, isActiveTrail;

    public TrailRenderer trail;
    public ParticleSystem particleSystem;

    public void UnHide()
    {
        isHidden = false;
    }

    public void Buy()
    {
        playerHasBought = true;
        PlayerData.instance.BuyItem(code, price);
    }


}
