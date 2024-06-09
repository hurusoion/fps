using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public PlayerStats playerStats;
    public Text hungerText;

    void Update()
    {
        hungerText.text = "Hunger: " + playerStats.hunger.ToString("0");
    }
}
