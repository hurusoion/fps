
using UnityEngine;
using UnityEngine.EventSystems;

public class ChestDropArea : MonoBehaviour, IDropHandler
{
    public Inventory fromInventory;
    public InventoryUIManager inventoryUIManager;

    public void OnDrop(PointerEventData eventData)
    {
        DraggableItem droppedItem = eventData.pointerDrag.GetComponent<DraggableItem>();
        if (droppedItem == null)
        {
            Debug.LogError("Dropped item does not have DraggableItem component.");
            return;
        }

        Item item = droppedItem.GetComponent<ItemIcon>().item;
        if (item == null)
        {
            Debug.LogError("Dropped item does not have a valid item.");
            return;
        }

        if (InventoryUIManager.instance.currentInventory != null)
        {
            fromInventory.RemoveItem(item);
            InventoryUIManager.instance.currentInventory.AddItem(item);
            InventoryUIManager.instance.UpdateUI();
        }
        else
        {
            Debug.LogError("Current inventory is not set.");
        }
    }
}
