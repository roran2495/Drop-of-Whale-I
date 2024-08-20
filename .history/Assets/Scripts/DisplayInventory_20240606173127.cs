using UnityEngine;
using UnityEngine.UI;

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
            newItem.transform.SetParent(content, false); // false to keep local scale
            
            // 绑定 item 并添加点击事件
            AddClickHandler(newItem, item);
        }
    }

    private void AddClickHandler(GameObject newItem, Item item)
    {
        // 获取 SpriteRenderer 组件
        SpriteRenderer spriteRenderer = newItem.GetComponent<SpriteRenderer>();

        // 设置初始颜色
        UpdateItemColor(spriteRenderer, item.isSelected);

        // 获取 Button 组件
        Button button = newItem.GetComponent<Button>();
        if (button == null)
        {
            button = newItem.AddComponent<Button>();
        }

        // 添加点击事件监听器
        button.onClick.AddListener(() =>
        {
            HandleClick(item, spriteRenderer);
        });
    }

    private void HandleClick(Item item, SpriteRenderer spriteRenderer)
    {
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
            spriteRenderer.color = new Color(1f, 0f, 0f); // 红色
        }
        else
        {
            spriteRenderer.color = new Color(1f, 1f, 1f); // 白色
        }
    }
}
