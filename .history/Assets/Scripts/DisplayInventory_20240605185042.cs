using UnityEngine;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    public GameObject itemPrefab; // 预制体
    public Transform content; // Content GameObject

    void Start()
    {
        
    }
    private void Update() {
        foreach (var item in GlobalManager.items)
        {
            // 实例化物品的 UI 元素
            GameObject newItem = Instantiate(itemPrefab, content);
            // 设置 UI 元素的 Sprite 图片
            newItem.transform.Find("").GetComponent<Image>().sprite = item.sprite;
            // 设置 UI 元素的父对象为 Content
            newItem.transform.SetParent(content);
            // 重置 UI 元素的缩放和位置
            newItem.transform.localScale = Vector3.one;
            newItem.transform.localPosition = Vector3.zero;
            // 可以根据需要设置其他信息，如点击事件等
        }
    }
}