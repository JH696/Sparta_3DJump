using System;
using UnityEngine;

public enum ItemType
{
    CupCake,
    Banana,
    Apple
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public ItemType type;
    public string _name;
    public string description;
    public Sprite icon;
}
