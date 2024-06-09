
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory playerInventory = other.GetComponent<Inventory>();
            if (playerInventory != null)
            {
                playerInventory.AddItem(item);
                InventoryUIManager.instance.UpdateUI();
                Destroy(gameObject); // ²¾°£ª««~
            }
        }
    }
}
