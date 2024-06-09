using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject combatMenu;
    public GameObject prepareMenu;
    public GameObject chestMenu;
    public Text playerInfoText;
    public Text enemyCountText;
    public Text playerHealthText;
    public Transform combatLogContent;
    public GameObject combatLogEntryPrefab;
    public ScrollRect combatLogScrollRect;
    public Button startCombatButton;
    public Button endCombatButton;
    public Button closeChestButton;
    public Player player;
    public Enemy[] enemies;

    // 新增变量定义
    public GameObject chestInventoryPanel;
    public Inventory currentInventory;
    public Inventory playerInventory; // 添加 playerInventory 变量
    public GameObject itemPrefab;
    public Transform playerInventoryPanel;
    public GameObject itemPanel;
    public Text itemPanelTitle;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 确保 UIManager 不会在场景切换时被销毁
        }
    }

    private void Start()
    {
        if (combatMenu == null || playerInfoText == null || enemyCountText == null || playerHealthText == null || combatLogContent == null || combatLogEntryPrefab == null || combatLogScrollRect == null || startCombatButton == null || endCombatButton == null || chestMenu == null)
        {
            Debug.LogError("UI elements are not assigned.");
            return;
        }

        playerHealthText.text = $"Health: {player.health}";
        combatMenu.SetActive(false);
        chestMenu.SetActive(false);
        endCombatButton.gameObject.SetActive(false);

        startCombatButton.onClick.AddListener(OnStartCombatButtonClicked);
        endCombatButton.onClick.AddListener(OnEndCombatButtonClicked);
        closeChestButton.onClick.AddListener(OnCloseChestButtonClicked);
        GameManager.instance.OnStateChange += HandleGameStateChange;
    }

    private void HandleGameStateChange(GameState newState)
    {
        switch (newState)
        {
            case GameState.Combat:
                ShowCombatMenu();
                break;
            case GameState.Exploration:
                HideCombatMenu();
                HideChestMenu();
                break;
            case GameState.PrepareCombat:
                ShowPrepareMenu();
                break;
            case GameState.OpenChest:
                ShowChestMenu();
                break;
        }
    }

    public void ShowChestMenu()
    {
        chestMenu.SetActive(true);
    }

    public void HideChestMenu()
    {
        chestMenu.SetActive(false);
    }

    public void UpdatePlayerHealthText()
    {
        playerHealthText.text = $"Health: {player.health}";
    }

    private void ShowCombatMenu()
    {
        playerHealthText.text = $"Health: {player.health}";
        enemyCountText.text = $"Enemies: {enemies.Length}";
        combatMenu.SetActive(true);
    }

    private void HideCombatMenu()
    {
        combatMenu.SetActive(false);
    }

    private void ShowPrepareMenu()
    {
        playerHealthText.text = $"Health: {player.health}";
        enemyCountText.text = $"Enemies: {enemies.Length}";
        prepareMenu.SetActive(true);
        startCombatButton.gameObject.SetActive(true);
        endCombatButton.gameObject.SetActive(false);
    }

    private void OnStartCombatButtonClicked()
    {
        GameManager.instance.StartCombat();
        startCombatButton.gameObject.SetActive(false);
        endCombatButton.gameObject.SetActive(false);
    }

    private void OnCloseChestButtonClicked()
    {
        HideChestMenu();
        GameManager.instance.EndCombat();
    }

    private void OnEndCombatButtonClicked()
    {
        foreach (var enemy in GameManager.instance.enemies)
        {
            if (enemy != null && enemy.health <= 0)
            {
                Destroy(enemy.gameObject);
            }
        }
        GameManager.instance.EndCombat();
        endCombatButton.gameObject.SetActive(false);
    }

    public void UpdateCombatLog(string message)
    {
        var newLogEntry = Instantiate(combatLogEntryPrefab, combatLogContent);
        var textComponent = newLogEntry.GetComponent<Text>();
        if (textComponent != null)
        {
            textComponent.text = message;
        }
        Canvas.ForceUpdateCanvases();
        combatLogScrollRect.verticalNormalizedPosition = 0f;
    }

    public void ShowEndCombatButton()
    {
        endCombatButton.gameObject.SetActive(true);
    }

    public void StartCombat()
    {
        GameManager.instance.StartCombat();
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
        foreach (Transform child in chestInventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }

        if (currentInventory != null)
        {
            foreach (Item item in currentInventory.GetItems())
            {
                GameObject itemObj = Instantiate(itemPrefab, chestInventoryPanel.transform);
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

    public void HandleStateChange(GameState newState)
    {
        prepareMenu.SetActive(newState == GameState.PrepareCombat);
        combatMenu.SetActive(newState == GameState.Combat);
        chestMenu.SetActive(newState == GameState.OpenChest);
    }
}
