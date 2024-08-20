using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
public class Item {
    public string id;
    public Sprite sprite;
    private DisplayInventory displayInventory;

    public Item(string id, Sprite sprite) {
        this.id = id;
        this.sprite = sprite;
    }
}

public static class GlobalManager {
    public static List<Item> items = new List<Item>();

    // 添加物品到列表中
    public static void AddItem(string id, Sprite sprite) {
        items.Add(new Item(id, sprite));
        disp
    }
}
