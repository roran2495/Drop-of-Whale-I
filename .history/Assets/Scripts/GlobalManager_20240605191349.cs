using System;
using System.Collections.Generic;
using UnityEngine;

public class Item {
    public string id;
    public Sprite sprite;

    public Item(string id, Sprite sprite) {
        this.id = id;
        this.sprite = sprite;
    }
}

public static class GlobalManager {
    public static List<Item> items = new List<Item>();
    public static event Action OnItemsChanged;

    // 添加物品到列表中
    public static void AddItem(Item item)
    {
        items.Add(item);
        OnItemsChanged?.Invoke();
    }

    public static void RemoveItem(Item item)
    {
        items.Remove(item);
        OnItemsChanged?.Invoke();
    }
}
