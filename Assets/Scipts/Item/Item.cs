using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public int id;
    public string name;
    public string description;
    public int quantity;
    public ItemType type;
    public Sprite sprite;

    public Item(int id, string name, string description, int quantity, ItemType type, Sprite sprite)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.quantity = quantity;
        this.type = type;
        this.sprite = sprite;
        //this.sprite = Resources.Load<Sprite>($"Sprites/Item/{type}/{sprite}");
    }

    public Item(Item item)
    {
        id = item.id;
        name = item.name;
        description = item.description;
        quantity = item.quantity;
        type = item.type;
        sprite = item.sprite;
        //sprite = Resources.Load<Sprite>($"Sprites/Item/{item.type}/{item.sprite}");
    }
}

public enum ItemType
{
    Weapon,
    Consumable,
    Quest,
    Misc
}
