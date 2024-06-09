using UnityEngine;

public class Item : ScriptableObject
{
    public string itemName; // 确保只有一个 itemName 属性
    public Sprite icon;
    public int maxDurability;
    internal string item;
}
