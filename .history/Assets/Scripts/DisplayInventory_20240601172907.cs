using UnityEngine;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour {
    public GameObject itemPrefab;
    public Transform content;

    void Start() {
        foreach (var item in GlobalManager.items) {
            // 实例化物品的 UI 元素
            GameObject newItem = Instantiate(itemPrefab, content);
            // 设置 UI 元素的 Sprite 图片
            newItem.GetComponent<Image>().sprite = item.sprite;
            // 可以根据需要设置其他信息，如点击事件等
        }
    }
}
