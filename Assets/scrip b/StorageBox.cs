using UnityEngine;
using System.Collections.Generic;

public class StorageBox : MonoBehaviour
{
    public List<Item> storedItems = new List<Item>();

    public void StoreItem(Item item)
    {
        storedItems.Add(item);
    }

    public void RemoveItem(Item item)
    {
        storedItems.Remove(item);
    }
}
