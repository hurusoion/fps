 
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public Item item;

    public void Setup(Item newItem)
    {
        item = newItem;
    }
}
