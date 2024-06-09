using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Items/Consumable")]
public class Consumable : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public int maxDurability;
    public int currentDurability;
    public int healthRestore;
    public int hungerRestore;

    public void Consume()
    {
        // 消耗逻辑，例如恢复健康或饥饿值
    }
}
