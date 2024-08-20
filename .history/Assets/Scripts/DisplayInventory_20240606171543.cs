using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DisplayInventory : MonoBehaviour
{
    public GameObject itemPrefab; // 预制体
    public Transform content; // Content GameObject

    void Start()
    {
        // 在游戏开始时显示初始的物品
        ShowObject();
        // 监听GlobalManager.items的变化
        GlobalManager.OnItemsChanged += ShowObject;
    }

    void OnDestroy()
    {
        // 移除监听器以避免内存泄漏
        GlobalManager.OnItemsChanged -= ShowObject;
    }

    public void ShowObject()
    {
        // 清空当前显示的内容
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in GlobalManager.items)
        {
            // 实例化物品的 UI 元素
            GameObject newItem = Instantiate(itemPrefab, content);
            // 设置 UI 元素的 Sprite 图片
            newItem.transform.Find("object").gameObject.GetComponent<SpriteRenderer>().sprite = item.sprite;
            // 设置 UI 元素的父对象为 Content
            newItem.transform.SetParent(content);

            // 添加点击事件
            AddClickHandler(newItem);
        }
    }

    private void AddClickHandler(GameObject newItem)
    {
        // 获取 SpriteRenderer 组件
        SpriteRenderer spriteRenderer = newItem.GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        bool isSelected = false;

        // 创建一个 EventTrigger 组件
        EventTrigger trigger = newItem.AddComponent<EventTrigger>();

        // // 创建并配置 PointerUp Entry
        // EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry();
        // pointerUpEntry.eventID = EventTriggerType.PointerUp;
        // pointerUpEntry.callback.AddListener((eventData) =>
        // {
        //     HandleClick(ref isSelected, spriteRenderer, originalColor);
        // });

        // 创建并配置 PointerClick Entry
        EventTrigger.Entry pointerClickEntry = new EventTrigger.Entry();
        pointerClickEntry.eventID = EventTriggerType.PointerClick;
        pointerClickEntry.callback.AddListener((eventData) =>
        {
            HandleClick(ref isSelected, spriteRenderer, originalColor);
        });

        // 添加 Entries 到 EventTrigger
        // trigger.triggers.Add(pointerUpEntry);
        trigger.triggers.Add(pointerClickEntry);
    }

    private void HandleClick(ref bool isSelected, SpriteRenderer spriteRenderer, Color originalColor)
    {
        if (isSelected)
        {
            // 取消选中
            spriteRenderer.color = originalColor;
        }
        else
        {
            // 选中
            spriteRenderer.color = new Color(1f, 0f, 0f); // 根据需要更改颜色
        }
        isSelected = !isSelected;
    }
}
