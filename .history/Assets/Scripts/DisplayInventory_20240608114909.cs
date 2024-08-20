using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;

public class DisplayInventory : MonoBehaviour
{
    public GameObject itemPrefab; // 预制体
    public GameObject feature;
    private string[] featuresId = { "02", "15"};
    private static GameObject preSelectedGameObject;
    private static Item preSelectedItem;
    public Transform content; // Content GameObject

    void Start()
    {
        SetFetureActive();
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
            HandleClick(item, newItem);
        });

        // 添加 Entries 到 EventTrigger
        trigger.triggers.Add(pointerClickEntry);
    }

    private void HandleClick(Item item, GameObject currentGameObject)
    {
        // 一般情况只支持一个物品被选中。但是如果先后被选中的物品要组合，那也在这里处理
        // 对当前点击物品使用上一选中物品
        // 形式为：选中pre显示特写，选中cur，对pre使用cur。此时可以全为选中状态
        if (featuresId.Contains(item.id))   // 特写情况
        {
            // 切换特写，切换选中状态，更新上一选中

            // 切换选中状态
            item.isSelected = !item.isSelected;
            // 更新颜色
            UpdateItemColor(currentGameObject.GetComponent<SpriteRenderer>(), item.isSelected);
            
            // 切换特写
            Camera.main.transform.Find("darkness").gameObject.SetActive(item.isSelected);
            GameObject gameObject;
            switch(item.id)
            {
                case "02" :
                    gameObject = feature.transform.Find("02").gameObject;
                    gameObject.SetActive(item.isSelected);
                    break;
                case "15" :
                    gameObject = feature.transform.Find("15").gameObject;
                    gameObject.SetActive(item.isSelected);
                    break;
            }
            if (item.isSelected)
            {
                GlobalManager.someItemIsSelected = true;
                preSelectedGameObject = currentGameObject;
                preSelectedItem = item;
                DisableOtherCollidersableOtherColliders()
            }
            else
            {
                GlobalManager.someItemIsSelected = false;
                preSelectedGameObject = null;
                preSelectedItem = null;
            }
        }
        else if (preSelectedItem != null && featuresId.Contains(preSelectedItem.id))
        {
            // 区分组合情况（对pre使用cur）和非组合情况(1.cur=pre 2.cur!=pre)
        }
        else
        {
            if (preSelectedGameObject != null && preSelectedItem != item)
            {
                preSelectedItem.isSelected = !preSelectedItem.isSelected;
                UpdateItemColor(preSelectedGameObject.GetComponent<SpriteRenderer>(), preSelectedItem.isSelected);     
            }
            // 切换选中状态
            item.isSelected = !item.isSelected;
            // 更新颜色
            UpdateItemColor(currentGameObject.GetComponent<SpriteRenderer>(), item.isSelected);
            
            if (item.isSelected)
            {
                GlobalManager.someItemIsSelected = true;
                preSelectedGameObject = currentGameObject;
                preSelectedItem = item;
            }
            else
            {
                GlobalManager.someItemIsSelected = false;
                preSelectedGameObject = null;
                preSelectedItem = null;
            }
            // 输出日志信息以验证更改
            Debug.Log($"Item {item.id} selected state is now {item.isSelected}");
        }
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

    void SetFetureActive()
    {
        foreach (Transform child in feature.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    void DisableOtherColliders(GameObject currentGameObject)
    {
        // 获取场景中所有的Collider组件
        Collider2D[] colliders = FindObjectsOfType<Collider2D>();
        
        // 遍历所有Collider组件，禁用除了特写对象以外的所有其他对象的碰撞器
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != currentGameObject)
            {
                collider.enabled = false;
            }
        }
    }
    void EnableOtherColliders()
    {
        // 获取场景中所有的Collider组件
        Collider2D[] colliders = FindObjectsOfType<Collider2D>();

        // 遍历所有Collider组件，禁用除了特写对象以外的所有其他对象的碰撞器
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = true;
        }
    }

    // 当当前被选中物品被使用时调用
    public static void SetPreNull()
    {
        preSelectedItem = null;
        preSelectedGameObject = null;
    }
}
