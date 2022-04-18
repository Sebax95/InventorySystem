using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour, IPickeable
{
    public int id;
    public string name;
    public string description;
    public int quantity;
    public ItemType type;
    public Sprite sprite;
    
    public Item Item => new Item(id, name, description, quantity, type, sprite);

    public void Pick(Inventory inv)
    {
        inv.AddItem(Item);
        Destroy(gameObject);
    }
}
