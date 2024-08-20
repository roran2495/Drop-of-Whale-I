using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Angle6Controller : MonoBehaviour
{
    public GameObject wordCanvas;
    public TMP_Text word;
    public TMP_Text owner;
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject backArrow;
    private GameObject darkness;
    private GameObject darkness2;
    public GameObject wardrobe;
    public GameObject wardrobeOpen;
    public GameObject wardrobeOpenedBox;
    public GameObject wardrobeUnopenedBox;
    public GameObject wardrobeBoxAromatheropy;
    public GameObject plushToy;
    public GameObject plushToyBed;
    public GameObject plushToyOpen;
    public GameObject plushToyKey;
    public GameObject glassCase;
    public GameObject necklace;
    private bool flagAromatheropy; // donnet get : false ; already get : true
    private bool flagKey;   // donnet get : false ; already get : true
    private bool flagNecklace;  // donnet get : false ; already get : true
    private bool flagToy;   // open : true;
    private bool flagUseKeyW;   // 打开衣柜解锁
    private bool flagUseKeyB;   // 打开香薰盒子解锁
    private Vector3 touchStartPosition; // 存储鼠标按下或开始触摸时的位置
    private Vector3 touchEndPosition; // 存储鼠标弹起或结束触摸时的位置
    // Start is called before the first frame update
    void Start()
    {
        darkness = Camera.main.transform.Find("darkness").gameObject;
        darkness2 = Camera.main.transform.Find("darkness (1)").gameObject;
        wardrobeOpen.SetActive(false);
        plushToy.SetActive(false);
        necklace.SetActive(false);
        flagAromatheropy = false;
        flagKey = false;
        flagNecklace = false;
        flagToy = false;
        flagUseKeyW = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 保证是触发视图6跳转
        if (Camera.main.transform.position.x == 30 && Camera.main.transform.position.y == 15)
        {
            // 检测触摸开始或鼠标按下
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
            {
                // 获取触摸位置或鼠标点击位置
                Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                touchStartPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                // 将触摸位置的 Z 值设置为场景的 Z 值
                touchStartPosition.z = wardrobe.transform.position.z;
            }
            // 检测触摸结束或鼠标弹起
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
            {
                // 获取触摸位置或鼠标点击位置
                Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                touchEndPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                // 将触摸位置的 Z 值设置为场景的 Z 值
                touchEndPosition.z = wardrobe.transform.position.z;
                float distance = touchStartPosition.x - touchEndPosition.x;
                if (Mathf.Abs(distance) < 1.0f)
                {
                    Debug.Log("点击");
                    HandleClick(touchStartPosition);
                } 
                else if (distance < 0)
                {
                    // 右
                    Debug.Log("向右拖动");
                    HandleDrag(touchStartPosition , true);
                }
                else
                {
                    // 左
                    Debug.Log("向左拖动");
                    HandleDrag(touchStartPosition , false);
                }
            }
        }
    }
    void HandleClick(Vector3 worldTouchPosition)
    {
        Vector3 worldTouchPositionD = worldTouchPosition;
        worldTouchPositionD.z = darkness.transform.position.z;
        // 从视角6到视角2
        if (backArrow && backArrow.activeSelf && backArrow.GetComponent<Collider2D>().bounds.Contains(worldTouchPosition))
        {
            Debug.Log("6->2");
            Camera.main.transform.position = new Vector3(-30, 0, -10);
            rightArrow.SetActive(!rightArrow.activeSelf);
            backArrow.SetActive(!backArrow.activeSelf);
        }
        // 从视角6到视角7
        if (leftArrow && leftArrow.activeSelf && leftArrow.GetComponent<Collider2D>().bounds.Contains(worldTouchPosition))
        {
            Debug.Log("6->7");
            Camera.main.transform.position = new Vector3(60, 15, -10);
            leftArrow.SetActive(!leftArrow.activeSelf);
            rightArrow.SetActive(!rightArrow.activeSelf);
        }
        if (wordCanvas.activeSelf)
        {
            wordCanvas.SetActive(false);
        }
        else if (!darkness.activeSelf)
        {
            // 点击鲸鱼，开启特写
            if (plushToyBed.activeSelf && plushToyBed.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                plushToy.SetActive(true);
                plushToyKey.SetActive(flagToy && !flagKey);
                darkness.SetActive(true);
                DisableOtherColliders(plushToy);
                plushToyOpen.GetComponent<PolygonCollider2D>().enabled = !flagToy;
                plushToyKey.GetComponent<PolygonCollider2D>().enabled = !flagKey;
                SetWord(1, "这只鲸鱼玩偶的肚子上居然有拉链, 不过好像是为了方便清洗, 总不会有人往这里面藏东西吧");
            }
            // 获得香薰
            if (!flagAromatheropy && wardrobeBoxAromatheropy.activeSelf && wardrobeBoxAromatheropy.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                flagAromatheropy = true;
                GlobalManager.AddItem("15", Resources.Load<Sprite>("others/aromatherapy"));
                wardrobeBoxAromatheropy.SetActive(false);
            }
            // 打开香薰盒
            else if (wardrobeUnopenedBox.activeSelf && wardrobeUnopenedBox.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                if (!flagUseKeyB)
                {
                    Item item = GlobalManager.FindItem("20");
                    if (item != null && item.isSelected)
                    {
                        flagUseKeyB = true;
                        GlobalManager.RemoveItem(item);
                    }
                    else
                    {
                        if (GlobalManager.someItemIsSelected)
                        {
                            SetWord(1, "这个可没有办法把这把锁打开");
                        }
                        else
                        {
                            SetWord(1, "我觉得我需要钥匙");
                        }
                    }
                }
                else
                {
                    wardrobeUnopenedBox.SetActive(false);
                    wardrobeOpenedBox.SetActive(true);
                    wardrobeBoxAromatheropy.SetActive(!flagAromatheropy);
                    if (!flagAromatheropy)
                    {
                        SetWord(1, "那股气味非常浓郁, 看来是从这里传出的……香薰!");
                    }
                    else
                    {
                        SetWord(1, "虽然香薰被取走了, 但是气味还是很浓郁");
                    }
                }
            }
            // 打开衣柜
            else if (wardrobe.activeSelf && wardrobe.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                if (!flagUseKeyW)
                {
                    Item item = GlobalManager.FindItem("17");
                    if (item != null && item.isSelected)
                    {
                        flagUseKeyW = true;
                        GlobalManager.RemoveItem(item);
                        SetWord(1, "可以打开衣柜了");
                    }
                    else
                    {
                        if (GlobalManager.someItemIsSelected)
                        {
                            SetWord(1, "用这个无法打开哦");
                        }
                        else
                        {
                            SetWord(1, "衣柜都锁? 是有什么贵重物品么");
                        }
                    }
                }
                else
                {
                    wardrobeOpen.SetActive(true);
                    wardrobeOpenedBox.SetActive(flagAromatheropy);
                    wardrobeUnopenedBox.SetActive(!flagAromatheropy);
                    wardrobeBoxAromatheropy.SetActive(wardrobeOpenedBox.activeSelf && !flagAromatheropy);
                    SetWord(1, "我又闻到了那股气味, 好像在这个盒子里")
                }
            }
            // 关闭衣柜
            if (wardrobeOpen.activeSelf && wardrobeOpen.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                wardrobeOpen.SetActive(false);
            }
            else if (!flagNecklace)
            {
                // 获取项链
                if (necklace.activeSelf && necklace.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    Debug.Log("获取项链");
                    GlobalManager.AddItem("16", Resources.Load<Sprite>("others/necklace"));
                    transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("angle6/angle6-1-(3)");
                    SetWord(1, "拿到项链了, 就是它的链条居然只是一根普通的绳子, 但是上面的宝石看着感觉不便宜的样子");
                }
                // 打碎玻璃柜
                else if (glassCase.activeSelf && glassCase.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    Item item = GlobalManager.FindItem("18");
                    if (item != null && item.isSelected)
                    {
                        necklace.SetActive(true);
                        transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("angle6/angle6-1-(2)");
                        GlobalManager.RemoveItem(item);
                        SetWord(1, "现在可以拿到这根项链了");
                    }
                    else
                    {
                        if (GlobalManager.someItemIsSelected)
                        {
                            SetWord(1, "用这个无法砸开");
                        }
                        else
                        {
                            SetWord(1, "没有找到锁, 不过如果真的需要拿到里面的东西的话,可以考虑拿个东西直接杂碎玻璃. 反正有老板兜底");
                        }
                    }
                }
            }
        } 
        //特写鲸鱼处理
        else if (plushToy.activeSelf)
        {
            // 点击空白处，退出特写
            if (!plushToy.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition)
            && darkness.GetComponent<BoxCollider2D>().bounds.Contains(worldTouchPositionD))
            {
                plushToy.SetActive(false);
                darkness.SetActive(false);
                EnableOtherColliders();
            }
            else 
            {
                // 获取钥匙
                if (flagToy && !flagKey && plushToyKey.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    flagKey = true;
                    GlobalManager.AddItem("17", Resources.Load<Sprite>("angle4/angle4-4-(4)"));
                    plushToyKey.SetActive(false);
                }
                // 点击拉链打开
                else if (!flagToy && plushToyOpen.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    flagToy = true;
                    plushToyKey.SetActive(!flagKey);
                    plushToy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("angle6/angle6-4-(2)");
                    SetWord(1, "居然藏了钥匙, NN是有什么奇怪爱好的奇怪家伙么? 去哪儿开什么都搞个锁");
                }
            }
        } else if(darkness2.activeSelf)
        {
            SetWord(1,"太黑了, 什么都看不见, 不过说不定黑暗中也会有什么信息存在, 毕竟小说里往往是这么写的哈哈");
        }
    }
    void HandleDrag(Vector3 worldTouchPosition, bool direction)
    {

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
        darkness.GetComponent<BoxCollider2D>().enabled = true;
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
    void SetWord(int n,string txt)
    {
        wordCanvas.SetActive(true);
        word.text = txt;
        if (n == 2)
        {
            owner.text = "座机:";
        }
        if (n == 3)
        {
            owner.text = "未知的男性:";
        }
        else {
            owner.text = "J:";
        }
    }
}
