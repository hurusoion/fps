using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    public static InventoryUIManager instance;

    public RectTransform playerInventoryPanel;
    public RectTransform chestInventoryPanel;
    public RectTransform equipPanel;
    public GameObject itemPrefab;
    public GameObject itemPanel;
    public Text itemPanelTitle;
    public Inventory playerInventory;
    public Inventory currentInventory;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HideItemPanel();
        }
    }

    public void ShowItemPanel(Item item, Vector3 position)
    {
        itemPanel.SetActive(true);
        itemPanel.transform.position = position;
        itemPanelTitle.text = item.itemName;
    }

    public void HideItemPanel()
    {
        itemPanel.SetActive(false);
    }

    public void CloseChest()
    {
        currentInventory = null;
        chestInventoryPanel.gameObject.SetActive(false);
    }

    public void OpenChest(Inventory chestInventory)
    {
        currentInventory = chestInventory;
        chestInventoryPanel.gameObject.SetActive(true);
        UpdateUI();
    }

    public void UpdateUI()
    {
        foreach (Transform child in chestInventoryPanel)
        {
            Destroy(child.gameObject);
        }

        if (currentInventory != null)
        {
            foreach (Item item in currentInventory.GetItems())
            {
                GameObject itemObj = Instantiate(itemPrefab, chestInventoryPanel);
                itemObj.GetComponent<ItemIcon>().Setup(item);
            }
        }

        foreach (Transform child in playerInventoryPanel)
        {
            Destroy(child.gameObject);
        }

        foreach (Item item in playerInventory.GetItems())
        {
            GameObject itemObj = Instantiate(itemPrefab, playerInventoryPanel);
            itemObj.GetComponent<ItemIcon>().Setup(item);
        }
    }
}
