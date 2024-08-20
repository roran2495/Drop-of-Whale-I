using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DisplayInventory : MonoBehaviour
{
    public GameObject itemPrefab; // 预制体
    private GameObject preSelectedGameObject;
    private Item preSelectedItem;
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

            // 添加点击事件监听器
            AddClickHandler(newItem, item);
        }
    }

    private void AddClickHandler(GameObject newItem, Item item)
    {
        // 获取 SpriteRenderer 组件
        SpriteRenderer spriteRenderer = newItem.GetComponent<SpriteRenderer>();

        // 确保有一个 Image 组件来接收事件
        Image image = newItem.GetComponent<Image>();
        if (image == null)
        {
            image = newItem.AddComponent<Image>();
            image.color = new Color(1, 1, 1, 0); // 透明色，不影响显示
        }

        // 创建一个 EventTrigger 组件
        EventTrigger trigger = newItem.AddComponent<EventTrigger>();


        // 创建并配置 PointerClick Entry
        EventTrigger.Entry pointerClickEntry = new EventTrigger.Entry();
        pointerClickEntry.eventID = EventTriggerType.PointerClick;
        pointerClickEntry.callback.AddListener((eventData) =>
        {
            HandleClick(item, spriteRenderer);
        });

        // 添加 Entries 到 EventTrigger
        trigger.triggers.Add(pointerClickEntry);
    }

    private void HandleClick(Item item, SpriteRenderer spriteRenderer)
    {
        if (preSelectedGameObject != null)
        {
            UpdateItemColor(preSelectedGameObject.GetComponent<SpriteRenderer>(),);
        }
        // 切换选中状态
        item.isSelected = !item.isSelected;
        // 更新颜色
        UpdateItemColor(spriteRenderer, item.isSelected);
        // 输出日志信息以验证更改
        Debug.Log($"Item {item} selected state is now {item.isSelected}");
    }

    private void UpdateItemColor(SpriteRenderer spriteRenderer, bool isSelected)
    {
        if (isSelected)
        {
            spriteRenderer.color = new Color(0.9f, 0.9f, 0.9f); 
        }
        else
        {
            spriteRenderer.color = new Color(1f, 1f, 1f); // 白色
        }
    }
}
