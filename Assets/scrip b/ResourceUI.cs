
using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour
{
    public Text foodText;
    public Text waterText;

    private ResourceManager resourceManager;

    void Start()
    {
        resourceManager = FindObjectOfType<ResourceManager>();
        if (resourceManager == null)
        {
            Debug.LogError("ResourceManager not found in the scene at Start.");
        }
        else
        {
            Debug.Log("ResourceManager found in the scene at Start.");
        }

        if (foodText == null)
        {
            Debug.LogError("foodText is not assigned in the Inspector.");
        }
        if (waterText == null)
        {
            Debug.LogError("waterText is not assigned in the Inspector.");
        }

        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (resourceManager != null)
        {
            if (foodText != null && waterText != null)
            {
                foodText.text = "Food: " + resourceManager.food;
                waterText.text = "Water: " + resourceManager.water;
            }
            else
            {
                Debug.LogError("Text components not assigned.");
            }
        }
        else
        {
            Debug.LogError("ResourceManager not found during UpdateUI.");
        }
    }
}
