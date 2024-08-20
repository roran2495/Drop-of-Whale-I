using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Angle1Controller : MonoBehaviour
{
    public GameObject wordCanvas;
    public TMP_Text word;
    public TMP_Text owner;
    public GameObject doorToAngle5;
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject backArrow;
    public GameObject darkness;
    public GameObject darkness2;
    public GameObject curtain;
    public bool flagCurtain;    // unuse : false   ;     use: true
    public GameObject switcher;   
    public bool flagSwitcher;   // turn on : false ;      turn off : true
    public GameObject pillow1;
    public GameObject pillow2;
    public GameObject broom1;
    public GameObject broom2;
    public GameObject carpet1;
    public GameObject carpet2;
    public GameObject letter1;
    public GameObject letter2;
    public GameObject phone1;
    public GameObject phone2;
    public GameObject shoeCabinet1;
    public GameObject shoeCabinet2;
    public GameObject shoeCabinet2Box;
    public GameObject shoeCabinet2Shoe;
    private bool flagUseKey;
    // Start is called before the first frame update
    void Start()
    {
        wordCanvas.SetActive(false);
        flagCurtain = false;
        flagSwitcher = false;
        flagUseKey = false;
        pillow2.SetActive(false);
        broom2.SetActive(false);
        carpet2.SetActive(false);
        letter2.SetActive(false);
        phone2.SetActive(false);
        shoeCabinet2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 保证是触发视图1跳转
        if (Camera.main.transform.position.x == 0 && Camera.main.transform.position.y == 0)
        {
            // 检测触摸输入或鼠标点击
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
            {
                // 获取触摸位置或鼠标点击位置
                Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                Vector3 worldTouchPositionD = worldTouchPosition;
                // 将触摸位置的 Z 值设置为场景的 Z 值
                worldTouchPosition.z = doorToAngle5.transform.position.z;
                worldTouchPositionD.z = darkness.transform.position.z;

                if (wordCanvas.activeSelf)
                {
                    wordCanvas.SetActive(false);
                }
                else if(!darkness.activeSelf)    //非黑暗状态
                {
                    // 从视角1到视角5
                    if (doorToAngle5.activeSelf && doorToAngle5.GetComponent<Collider2D>().bounds.Contains(worldTouchPosition))
                    {
                        if (!flagUseKey)
                        {
                            Item item = GlobalManager.FindItem("05");
                            if (item != null && item.isSelected)
                            {
                                flagUseKey = true;
                                GlobalManager.RemoveItem(item);
                                SetWord(1,"锁开了");
                            }
                            else
                            {
                                if (GlobalManager.someItemIsSelected)
                                {
                                    SetWord(1,"没有反应: 应该不是用这个打开的");
                                }
                                else
                                {
                                    SetWord(1,"上锁了, 我觉得我需要先找到钥匙");
                                }
                            }
                        }
                        else
                        {
                            Debug.Log("5->1");
                            Camera.main.transform.position = new Vector3(0, 15, -10);
                            leftArrow.SetActive(!leftArrow.activeSelf);
                            rightArrow.SetActive(!rightArrow.activeSelf);
                            backArrow.SetActive(!backArrow.activeSelf);
                        } 
                    }

                    if (!letter2.activeSelf && !phone2.activeSelf && !shoeCabinet2.activeSelf)
                    {
                        // 窗帘使用+开关打开：黑暗效果
                        darkness2.SetActive(flagCurtain && flagSwitcher);
                    }
                    
                    if (pillow1.activeSelf)
                    {
                        // 点击抱枕1 ：移动（显示抱枕2）
                        if (pillow1.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition) || letter1.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            pillow1.SetActive(!pillow1.activeSelf);
                            pillow2.SetActive(!pillow2.activeSelf);
                        }
                    } else {
                        // 点击信件 ：显示信件
                        if (letter1.activeSelf && letter1.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            darkness.SetActive(true);
                            letter2.SetActive(!letter2.activeSelf);
                            // 禁用其他对象的碰撞器
                            DisableOtherColliders(letter2);
                        }
                    }
                    // 点击抱枕2 ：移动（显示抱枕1）
                    if (pillow2.activeSelf && pillow2.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                    {
                        pillow1.SetActive(!pillow1.activeSelf);
                        pillow2.SetActive(!pillow2.activeSelf);
                    }
                    // 点击地毯1 ：获取钥匙（显示地毯2）
                    if(carpet1.activeSelf && carpet1.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                    {
                        GlobalManager.AddItem("01", Resources.Load<Sprite>("angle4/angle4-4-(4)"));
                        carpet1.SetActive(!carpet1.activeSelf);
                        carpet2.SetActive(!carpet2.activeSelf);
                        SetWord(1,"一把钥匙, 不知道是打开什么的,等看到被锁住的东西再说");
                    }
                    // 点击电话 ： 显示电话，实现拨号
                    if (!darkness.activeSelf && phone1.activeSelf && phone1.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                    {
                        darkness.SetActive(true);
                        phone2.SetActive(!phone2.activeSelf);
                        // 禁用其他对象的碰撞器
                        DisableOtherColliders(phone2);
                        SetWord(1,"我记得这种特定的座机按下#号会清空目前输入的号码, 并且这种座机只能接受9位数字的号码");
                    } 
                    // 点击扫把1 ：拆卸（显示扫把2）
                    if(!darkness.activeSelf && broom1.activeSelf && broom1.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                    {
                        GlobalManager.AddItem("02", Resources.Load<Sprite>("others/broom handle1"));
                        broom1.SetActive(!broom1.activeSelf);
                        broom2.SetActive(!broom2.activeSelf);
                        SetWord(1, "啊啊啊, 不知道为什么就是忍不住想要拆下来, 总有种在打游戏收集道具的感觉");
                    }
                    // 点击鞋柜 ：显示鞋柜
                    else if (!darkness.activeSelf && shoeCabinet1.activeSelf && shoeCabinet1.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition) && (broom1.activeSelf && !broom1.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition) || broom2.activeSelf && !broom2.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition)))
                    {
                        darkness.SetActive(true);
                        shoeCabinet2.SetActive(!shoeCabinet2.activeSelf);
                        // 禁用其他对象的碰撞器
                        DisableOtherColliders(shoeCabinet2);
                        shoeCabinet2Box.GetComponent<BoxCollider2D>().enabled = true;
                        shoeCabinet2Shoe.GetComponent<PolygonCollider2D>().enabled = true;
                        SetWord(1,"感觉有一股很奇怪的味道, 说不上好闻, 但是也不是难闻, 至少绝对不是这些鞋子发出的");
                    }
                    
                }
                // 点击信件外 ：取消特写
                else if (letter2.activeSelf && !letter2.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition) 
                && darkness.GetComponent<BoxCollider2D>().bounds.Contains(worldTouchPositionD))
                {
                    darkness.SetActive(false);
                    letter2.SetActive(!letter2.activeSelf);
                    // 启用其他对象的碰撞器
                    EnableOtherColliders();
                }
                
                else if (phone2.activeSelf)
                {
                    // 点击电话外 ：取消特写
                    if (!phone2.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition)
                    && darkness.GetComponent<BoxCollider2D>().bounds.Contains(worldTouchPositionD))
                    {
                        darkness.SetActive(false);
                        TextMeshPro text = phone2.transform.Find("number").GetComponent<TextMeshPro>();
                        text.text = "";
                        phone2.SetActive(!phone2.activeSelf);
                        // 启用其他对象的碰撞器
                        EnableOtherColliders();
                    }
                    // 拨号
                    else if (phone2.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                    {
                        Transform whitchClick = null;
                        TextMeshPro text = phone2.transform.Find("number").GetComponent<TextMeshPro>();
                        foreach(Transform child in phone2.transform)
                        {
                            PolygonCollider2D polygonCollider2D = child.GetComponent<PolygonCollider2D>();
                            if (polygonCollider2D != null)
                            {
                                if (!polygonCollider2D.enabled)
                                {
                                    polygonCollider2D.enabled = true;
                                }
                                if (polygonCollider2D.bounds.Contains(worldTouchPosition))
                                {
                                    whitchClick = child;
                                    break;
                                }
                            }
                            
                        }
                        if (whitchClick != null)
                        {
                            switch (whitchClick.gameObject.name)
                            {
                                case "call":
                                    if (text.text.Length < 9)
                                    {
                                        SetWord(2,"您输入的数字并非号码格式, 请重新输入");
                                        text.text = "";
                                    }
                                    else if (text.text != "828857640")
                                    {
                                        SetWord(2,"您所拨打的号码是空号, 请稍后再拨");
                                        text.text = "";
                                    }
                                    else
                                    {
                                        SetWord(3,"好久不见, 这是一条留言, 我知道只有你会打这个电话NN, 已经把房子看好了, 地址在梦想路7号, 很高兴还有继续成为邻居的这天");
                                    }
                                    break;
                                case "other1":
                                    SetWord(1,"这个键好像坏了, 点击了以后没有任何反应");
                                    break;
                                case "other2":
                                    text.text = "";
                                    break;
                                case "1":
                                case "2":  
                                case "3":
                                case "4":
                                case "5":
                                case "6":
                                case "7":
                                case "8":
                                case "9":
                                case "0":
                                    if (text.text.Length == 9)
                                    {
                                        SetWord(1,"已经到号码长度的上限了, 毕竟这种座机的电话号码都是9位的");
                                    }
                                    else 
                                    {
                                        text.text = text.text + whitchClick.gameObject.name;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                } 
                // 点击鞋柜外 ：取消特写
                else if (shoeCabinet2.activeSelf)
                { 
                    if (!shoeCabinet2.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition)
                    && darkness.GetComponent<BoxCollider2D>().bounds.Contains(worldTouchPositionD))
                    {
                        darkness.SetActive(false);
                        shoeCabinet2.SetActive(!shoeCabinet2.activeSelf);
                        // 启用其他对象的碰撞器
                        EnableOtherColliders();
                    }
                    else if (shoeCabinet2Box.GetComponent<BoxCollider2D>().bounds.Contains(worldTouchPosition))
                    {
                        SetWord(1, "原来这个味道是从快递盒这里传出来的,不知道她最近买了什么东西……咦, 这个快递怎么没有贴运单");
                    }
                    else if (shoeCabinet2Shoe.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                    {
                        SetWord(1,"总感觉这里少了一双鞋, 不过说不定这里本来就是空着的");
                    }
                }
                // 点击窗帘 ： 不使用or使用
                if (curtain.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition) && !curtain.GetComponent<CircleCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    Debug.Log("点击窗帘");
                    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                    Sprite newSprite = Resources.Load<Sprite>("angle1/angle1-1-(1)");
                    if (!flagCurtain) // unuse -> use
                    {
                        newSprite = Resources.Load<Sprite>("angle1/angle1-1-(2)");
                    }
                    flagCurtain = !flagCurtain;
                    darkness2.SetActive(flagCurtain && flagSwitcher);
                    Angle2Controller angle2ControllerInstance = GameObject.Find("Angle2").GetComponent<Angle2Controller>();
                    angle2ControllerInstance.FlagCurtainTransmit(flagCurtain); 
                    spriteRenderer.sprite = newSprite; 
                }
                // 点击开关：打开or关闭
                else if (switcher.activeSelf && switcher.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    Debug.Log("点击开关");
                    flagSwitcher = !flagSwitcher;
                    darkness2.SetActive(flagCurtain && flagSwitcher);
                    if (!flagCurtain && flagSwitcher)
                    {
                        SetWord(1, "关上了灯, 但是还是好亮, 这个房子采光真不错……可能要把窗帘也拉上才能让房间暗下来");
                    }
                    else
                    {
                        SetWord(1, "");
                    }
                } 
                else if(darkness2.activeSelf)
                {
                    SetWord(1,"太黑了, 什么都看不见, 不过说不定黑暗中也会有什么信息存在, 毕竟小说里往往是这么写的哈哈");
                }     
            }
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
