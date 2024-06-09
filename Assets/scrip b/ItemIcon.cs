
﻿using UnityEngine;

public class ItemIcon : MonoBehaviour
{
    public Item item;

    public void Setup(Item newItem)
    {
        item = newItem;
        // 更新图标和其他 UI 元素
    }
}
