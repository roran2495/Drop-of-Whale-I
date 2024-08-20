using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
#if UNITY_EDITOR
    using UnityEditor;
#endif

public class DisplayInventory : MonoBehaviour
{
    public GameObject itemPrefab; // 预制体
    public GameObject itemPrefab24;
    public GameObject itemPrefab25;
    public GameObject feature;
    private GameObject darkness;
    private GameObject F22;
     // 移动的目标位置
    public Vector2 targetPosition;
    // 移动的速度
    public float speed = 5f;
    private bool flagResolve22;
    private bool flagOpen;
    private string[] featuresId = { "02", "15", "19"};
    private static GameObject preSelectedGameObject;
    private static Item preSelectedItem;
    public Transform content; // Content GameObject

    void Start()
    {
        darkness = Camera.main.transform.Find("darkness").gameObject;
        flagResolve22 = false;
        flagOpen = false;
        F22 = feature.transform.Find("22").gameObject;
        // 设置目标位置
        targetPosition = new Vector2(-0.64f, 0.67f);

        SetFetureActive();
        // 在游戏开始时显示初始的物品
        ShowObject();
        // 监听GlobalManager.items的变化
        GlobalManager.OnItemsChanged += ShowObject;
    }
    void Update()
    {
        if (flagResolve22)
        {
            // 检测触摸开始或鼠标按下
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
            {
                // 获取触摸位置或鼠标点击位置
                Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                // 将触摸位置的 Z 值设置为场景的 Z 值
                worldTouchPosition.z = feature.transform.position.z;

                GameObject F19 = feature.transform.Find("19").gameObject;
                GameObject open = F19.transform.Find("open").gameObject;
                GameObject get = F19.transform.Find("get").gameObject;
                if (flagOpen && get.activeSelf && get.GetComponent<Collider2D>().bounds.Contains(worldTouchPosition))
                {
                    Debug.Log("get");
                    Item item = GlobalManager.FindItem("22");
                    if (item == null)
                    {
                        GlobalManager.AddItem("22", Resources.Load<Sprite>("others/match"));
                    }
                    else
                    {
                        Debug.Log("不用反复拿取火柴");
                    }
                } 
                else if (open.activeSelf && open.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    Debug.Log("open");
                    flagOpen = true;
                    F19.transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("others/matchbox 2");
                }
            }
        }
        // 实现移动
        if (F22.activeSelf)
        {   if (Vector2.Distance(F22.transform.localPosition, targetPosition) > 0.1f)
            {
                Debug.Log(Vector2.Distance(F22.transform.localPosition, targetPosition));
                F22.transform.localPosition = Vector2.MoveTowards(F22.transform.localPosition, targetPosition, speed * Time.deltaTime);
            }
            // 到达目标位置以后 19 + 22 =》23 去2合1，且取消特写，状态重置
            else
            {
                Debug.Log(Vector2.Distance(F22.transform.localPosition, targetPosition));
                GlobalManager.RemoveItem(preSelectedItem);
                GlobalManager.RemoveItem(GlobalManager.FindItem("22"));
                GlobalManager.AddItem("23", Resources.Load<Sprite>("others/match 2"));

                // 取消特写
                darkness.SetActive(false);
                feature.transform.Find("19").gameObject.SetActive(false);
                F22.SetActive(false);
                EnableOtherColliders();

                // 状态重置
                GlobalManager.someItemIsSelected = false;
                preSelectedGameObject = null;
                preSelectedItem = null;
            }
        }
    }
    void OnDestroy()
    {
        // 移除监听器以避免内存泄漏
        GlobalManager.OnItemsChanged -= ShowObject;
    }

    public void ShowObject()
    {
        // 缓存当前选中物品的状态
        var selectedItem = preSelectedItem;

        // 清空当前显示的内容
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in GlobalManager.items)
        {
            GameObject newItem;

            if (item.id == "24")
            {
                newItem = Instantiate(itemPrefab24, content);
                newItem.transform.Find("pigments").gameObject.GetComponent<SpriteRenderer>().color = item.color;
            }
            else if (item.id == "25")
            {
                newItem = Instantiate(itemPrefab25, content);
                newItem.transform.Find("color").gameObject.GetComponent<SpriteRenderer>().color = item.color;
            }
            else 
            {
                newItem = Instantiate(itemPrefab, content);
                newItem.transform.Find("object").gameObject.GetComponent<SpriteRenderer>().sprite = item.sprite;
            }

            newItem.transform.SetParent(content);
            UpdateItemColor(newItem.transform.GetComponent<SpriteRenderer>(), item.isSelected);
            AddClickHandler(newItem, item);

            // 恢复选中物品的状态
            if (selectedItem != null && selectedItem == item)
            {
                preSelectedGameObject = newItem;
                preSelectedItem = item;
            }
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
            // 非道具特写需要提示退出特写状态。道具特写直接切换

            if (preSelectedItem != null)
            {
                if (preSelectedItem.id != item.id)
                {
                    preSelectedItem.isSelected = !preSelectedItem.isSelected;
                    UpdateItemColor(preSelectedGameObject.GetComponent<SpriteRenderer>(), preSelectedItem.isSelected);
                }
                if (featuresId.Contains(preSelectedItem.id))
                {
                    darkness.SetActive(false);
                }
            }
            if (!darkness.activeSelf)
            {
                // 切换特写，切换选中状态，更新上一选中

                // 切换选中状态
                item.isSelected = !item.isSelected;
                // 更新颜色
                UpdateItemColor(currentGameObject.GetComponent<SpriteRenderer>(), item.isSelected);
                
                // 切换特写
                darkness.SetActive(item.isSelected);
                GameObject gameObject;
                switch(item.id)
                {
                    case "02" :
                        gameObject = feature.transform.Find("02").gameObject;
                        break;
                    case "15" :
                        gameObject = feature.transform.Find("15").gameObject;
                        break;
                    case "19" :
                        gameObject = feature.transform.Find("19").gameObject;
                        break;
                    default:    // 不会发生
                        gameObject = null;
                        break;
                }
                gameObject.SetActive(item.isSelected);
                // 对单一特写处理
                if (item.id == "19")
                {
                    // 获得火柴22
                    flagResolve22 = true;
                }

                // 更新上一选中状态
                if (item.isSelected)
                {
                    GlobalManager.someItemIsSelected = true;
                    preSelectedGameObject = currentGameObject;
                    preSelectedItem = item;
                    DisableOtherColliders(gameObject);
                    if (item.id == "19")
                    {
                        gameObject.transform.Find("open").GetComponent<PolygonCollider2D>().enabled = true;
                        gameObject.transform.Find("get").GetComponent<PolygonCollider2D>().enabled = true;
                    }
                }
                else
                {
                    GlobalManager.someItemIsSelected = false;
                    preSelectedGameObject = null;
                    preSelectedItem = null;
                    EnableOtherColliders();
                }
            }
            else
            {
                Debug.Log("需要先退出当前特写");
            }
        }
        else if (preSelectedItem != null && featuresId.Contains(preSelectedItem.id))
        {
            // 此时一定满足 pre!=cur
            // 区分组合情况（对pre使用cur）和非组合情况
            // 切换选中状态 自动取消选择
            item.isSelected = !item.isSelected;
            // 更新颜色
            UpdateItemColor(currentGameObject.GetComponent<SpriteRenderer>(), item.isSelected);
            
            if (item.isSelected) // 选择
            {
                // 组合
                if (preSelectedItem.id == "02" && item.id == "16")
                {
                    Item item1 = GlobalManager.FindItem("14");
                    if (item1 != null)
                    {
                        // 02 + 16 =》21 去2合1，且取消特写，状态重置
                        GlobalManager.RemoveItem(preSelectedItem);
                        GlobalManager.RemoveItem(item);
                        GlobalManager.AddItem("21", Resources.Load<Sprite>("others/broom handle2"));

                        // 取消特写
                        darkness.SetActive(false);
                        feature.transform.Find("02").gameObject.SetActive(false);
                        EnableOtherColliders();

                        // 状态重置
                        GlobalManager.someItemIsSelected = false;
                        preSelectedGameObject = null;
                        preSelectedItem = null;
                    }
                    else
                    {
                        Debug.Log("有没有什么东西可以把这条该死的项链剪短，不把它剪断我没办法牢牢绑住zheliangg");
                    }
                    
                }
                else if (preSelectedItem.id == "19" && item.id == "22")
                {
                    // 显示22并开始移动
                    F22.SetActive(true);
                    flagResolve22 = false;
                }
                else if (preSelectedItem.id == "15" && item.id == "23")
                {
                    GlobalManager.RemoveItem(item);
                    feature.transform.Find("15").transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("others/aromatherapy 2");
                    
                    // 启动协程等待3秒
                    StartCoroutine(Next());
                    
                }
                else // 非组合情况
                {
                    // 切换选中状态 自动取消选择
                    item.isSelected = !item.isSelected;
                    // 更新颜色
                    UpdateItemColor(currentGameObject.GetComponent<SpriteRenderer>(), item.isSelected);
                    Debug.Log("不应该使用这个吧");
                }
            }
        }
        else
        {
            Debug.Log(preSelectedGameObject != null);
            Debug.Log(preSelectedItem != item);
            if (preSelectedGameObject != null && preSelectedItem != item)
            {
                preSelectedItem.isSelected = !preSelectedItem.isSelected;
                UpdateItemColor(preSelectedGameObject.GetComponent<SpriteRenderer>(), preSelectedItem.isSelected);     
                Debug.Log($"Item {preSelectedItem.id} selected state is now {preSelectedItem.isSelected}");
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

    IEnumerator Next()
    {
        // 等待3秒
        yield return new WaitForSeconds(3f);
        // 等待结束后的操作
        Debug.Log("3 seconds have passed.");
        
        // 退出游戏
        #if UNITY_EDITOR
        // 在编辑模式下停止播放
        EditorApplication.isPlaying = false;
        #else
        // 在构建版本中退出游戏
        Application.Quit();
        #endif
    }
}
