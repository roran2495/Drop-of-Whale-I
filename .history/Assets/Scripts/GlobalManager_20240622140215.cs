using System;
using System.Collections.Generic;
using UnityEngine;

public class Item {
    public string id;
    public Sprite sprite;
    public bool isSelected;
    public int usedTimes;   // 反复使用时的标记
    public Color color; // 色料杯使用
    public Item(string id, Sprite sprite, Color color) {}
        this.id = id;
        this.sprite = sprite;
        this.isSelected = false;
        usedTimes = 0;
    }
}

public static class GlobalManager {
    public static List<Item> items = new List<Item>();
    public static bool someItemIsSelected = false;
    public static event Action OnItemsChanged;

    // 添加物品到列表中
    public static void AddItem(string id, Sprite sprite)
    {
        items.Add(new Item(id, sprite, false, null));
        OnItemsChanged?.Invoke();
    }
    public static void AddItemColor(string id, Color color)
    {
        items.Add(new Item(id, null, false, color));
        OnItemsChanged?.Invoke();
    }
    public static void RemoveItem(Item item)
    {
        if (item.id == "05")
        {
            if (item.usedTimes < 3)
            {
                item.usedTimes ++ ;
            }
            else
            {
                if (item.isSelected)
                {
                    someItemIsSelected = false ;
                    DisplayInventory.SetPreNull() ;
                }
                items.Remove(item);
                OnItemsChanged?.Invoke();
            }
        }
        else
        {
            if (item.isSelected)
            {
                someItemIsSelected = false ;
                DisplayInventory.SetPreNull() ;
            }
            items.Remove(item);
            OnItemsChanged?.Invoke();
        }
        
    }
    public static Item FindItem(string id)
    {
        foreach(Item item in items)
        {
            if(item.id == id)
                return item;
        }
        return null;
    }
}
