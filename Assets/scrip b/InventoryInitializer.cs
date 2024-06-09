 
using UnityEngine;

public class InventoryInitializer : MonoBehaviour
{
    public Inventory playerInventory;
    public Item[] initialItems;

    private void Start()
    {
        foreach (var item in initialItems)
        {
            playerInventory.AddItem(item);
        }
    }
}
