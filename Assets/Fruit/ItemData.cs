using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "Item Data/Generic Item Data", order = 0)]
public class ItemData : ScriptableObject
{
    [SerializeField] protected GameObject item, destroyEffect;

    [SerializeField] private int highScoreToUnlock = 0;
    
    public float spawnWeight = 1;

    public bool HasUnlocked(int highScore)
    {
        return (highScore >= highScoreToUnlock);
    }
    

}

