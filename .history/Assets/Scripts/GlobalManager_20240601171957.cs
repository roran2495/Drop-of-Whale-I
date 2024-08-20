using System.Collections.Generic;
using UnityEngine;

public static class GlobalManager {
    public static List<Item> items = new List<Item>();

    // 添加物品到列表中
    public static void AddItem(string id, Sprite sprite) {
        items.Add(new Item(id, sprite));
    }
}
