using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public void AddItem(Item item)
    {
        if (!items.Contains(item))
        {
            items.Add(item);
        }
        else
        {
            Debug.LogWarning("Item already exists in inventory.");
        }
    }

    public void RemoveItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
        }
        else
        {
            Debug.LogWarning("Item does not exist in inventory.");
        }
    }

    public bool HasItem(Item item)
    {
        return items.Contains(item);
    }

    public List<Item> GetItems()
    {
        return new List<Item>(items);
    }
}
