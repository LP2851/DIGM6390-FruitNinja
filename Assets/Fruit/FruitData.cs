using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFruitData", menuName = "Item Data/Fruit Data", order = 1)]
public class FruitData : ItemData
{
    [ColorUsage(true, false)] public Color splatColor = Color.white;
}
