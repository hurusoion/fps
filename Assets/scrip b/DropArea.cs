 
using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler
{
    public Inventory fromInventory;
    public Inventory toInventory;
    public InventoryUIManager inventoryUIManager;

    public void OnDrop(PointerEventData eventData)
    {
        var droppedItem = eventData.pointerDrag.GetComponent<DraggableItem>();
        if (droppedItem == null) return;

        var itemIcon = droppedItem.GetComponent<ItemIcon>();
        if (itemIcon == null)
        {
            Debug.LogError("ItemIcon does not have a valid item.");
            return;
        }

        var item = itemIcon.item;
        if (item == null)
        {
            Debug.LogError("ItemIcon does not have a valid item.");
            return;
        }

        if (toInventory == null)
        {
            Debug.LogError("ToInventory is not set for the drop area.");
            droppedItem.transform.position = droppedItem.originalPosition;
            return;
        }

        if (fromInventory.items.Contains(item))
        {
            fromInventory.RemoveItem(item);
            toInventory.AddItem(item);
            inventoryUIManager.UpdateUI();
            Debug.Log($"Item {item.itemName} moved from {fromInventory.name} to {toInventory.name}.");
        }
        else
        {
            Debug.LogError("Item does not exist in the from inventory.");
            droppedItem.transform.position = droppedItem.originalPosition;
        }
    }
}
