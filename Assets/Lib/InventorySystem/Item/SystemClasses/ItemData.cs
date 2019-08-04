using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public enum Rarity
{
    Common,
    Magic,
    Rare,
    Legendary,
    Unique
}

public enum ItemType
{
    Consumable,
    Weapon,
    Armor,
    Accessory,
    Quest
}
public abstract class ItemData : ScriptableObject
{
    public string myName;
    // Icon to show in inventory etc.
    public Sprite myInventoryIcon;
    public Sprite myWorldSprite;
    public Rarity itemRarity;
    public ItemType itemType;
    public int itemLevel;


    public abstract void Use();
}
