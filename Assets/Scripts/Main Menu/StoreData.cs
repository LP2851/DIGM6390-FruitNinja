using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreData : MonoBehaviour
{
    public static StoreData instance;
    public StoreItem[] storeItems;

    private void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
    }
}
