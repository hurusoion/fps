
﻿using UnityEngine;
public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 1f;
    public LayerMask interactableLayer;
    private Inventory currentChestInventory;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Chest chest = other.GetComponent<Chest>();
        if (chest != null)
        {
            InventoryUIManager.instance.OpenChest(chest.GetComponent<Inventory>());
        }
    }

    public void OpenChest(Chest chest)
    {
        currentChestInventory = chest.GetComponent<Inventory>();
        InventoryUIManager.instance.OpenChest(currentChestInventory);
    }

    public void CloseChest()
    {
        currentChestInventory = null;
        InventoryUIManager.instance.CloseChest();
    }
    private void Interact()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRange, interactableLayer);
        foreach (var hitCollider in hitColliders)
        {
            Chest chest = hitCollider.GetComponent<Chest>();
            if (chest != null)
            {
                OpenChest(chest);
                return;
            }
        }
    }
}
