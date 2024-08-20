using System;
using System.Collections.Generic;
using UnityEngine;

public class Item {
    public string id;
    public Sprite sprite;
    public bool isSelected;
    public int usedTimes;   // 反复使用时的标记
    public Color color; // 色料杯使用
    public Item(string id, Sprite sprite, Color color, int usedTimes)
    {
        this.id = id;
        this.sprite = sprite;
        this.color = color;
        isSelected = false;
        this.usedTimes = usedTimes;
    }
}

public static class GlobalManager {
    public static List<Item> items = new List<Item>();
    public static bool someItemIsSelected = false;
    public static event Action OnItemsChanged;

    // 添加物品到列表中
    public static void AddItem(string id, Sprite sprite)
    {
        items.Add(new Item(id, sprite, Color.white, 0));
        OnItemsChanged?.Invoke();
    }
    public static void AddItemColor(string id, Color color)
    {
        items.Add(new Item(id, null, color, 0));
        OnItemsChanged?.Invoke();
    }
    public static void AddItemCup(string id, Sprite sprite, int usedTimes)
    {
        // 只有牙杯07显示的是剩余使用次数，且0是剩余使用次数0，1-5是剩余冷水使用次数，5-10是剩余热水使用次数
        items.Add(new Item(id, sprite, Color.white, usedTimes)); 
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
        else if (item.id == "07")
        {

        }
        else {
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
