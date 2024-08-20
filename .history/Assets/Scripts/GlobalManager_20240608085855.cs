using System;
using System.Collections.Generic;
using UnityEngine;

public class Item {
    public string id;
    public Sprite sprite;
    public bool isSelected;
    public Item(string id, Sprite sprite, bool isSelected) {
        this.id = id;
        this.sprite = sprite;
        this.isSelected = isSelected;
    }
}

public static class GlobalManager {
    public static List<Item> items = new List<Item>();
    public static event Action OnItemsChanged;

    // 添加物品到列表中
    public static void AddItem(string id, Sprite sprite)
    {
        items.Add(new Item(id, sprite, false));
        OnItemsChanged?.Invoke();
    }

    public static void RemoveItem(Item item)
    {
        items.Remove(item);
        OnItemsChanged?.Invoke();
    }
    public static Item FindItem(string id)
    {
        foreach(Item item in items)
        {
            if(item.id == id)
                return item;
        }
        
    }
}