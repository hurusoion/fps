 
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Inventory chestInventory;
    private void Awake()
    {
        if (chestInventory == null)
        {
            chestInventory = GetComponent<Inventory>();
        }
    }
    // Add an item to the chest
    public void AddItem(Item item)
    {
        chestInventory.AddItem(item);
    }

    // Remove an item from the chest
    public void RemoveItem(Item item)
    {
        chestInventory.RemoveItem(item);
    }

    // Get the list of items in the chest
    public List<Item> GetItems()
    {
        return chestInventory.items;
    }
}
