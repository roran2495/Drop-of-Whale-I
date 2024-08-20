using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Angle8Controller : MonoBehaviour
{
    public GameObject wordCanvas;
    public TMP_Text word;
    public TMP_Text owner;
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject backArrow;
    private GameObject darkness;
    private GameObject darkness2;
    public GameObject glassWall;
    public GameObject bookcase;
    public GameObject match;
    public GameObject desk;
    public GameObject deskFeature;
    public GameObject easel;
    public GameObject easelFeature;
    public GameObject bonsai;
    public GameObject bonsaiFeature;
    public GameObject bonsaiFeatureMatch;
    public GameObject painting;
    public GameObject paintingFeature1;
    public GameObject paintingFeature2;
    public GameObject paintingFeatureKey;
    public GameObject temperaBook;
    public GameObject temperaBookCover;
    public GameObject temperaBookContent;
    private bool glassWallWet; //wet : true
    private bool flagGlassWall;
    private bool flagMatch; //  already get : true
    private bool flagLocker;    // open : true
    private bool flagKey;   // already get : true
    private bool flagInformation;
    private int page;   // control tempera book
    private Vector3 touchStartPosition; // 存储鼠标按下或开始触摸时的位置
    private Vector3 touchEndPosition; // 存储鼠标弹起或结束触摸时的位置
    // Start is called before the first frame update
    void Start()
    {
        darkness = Camera.main.transform.Find("darkness").gameObject;
        darkness2 = Camera.main.transform.Find("darkness (1)").gameObject;
        glassWall.SetActive(flagGlassWall);
        deskFeature.SetActive(false);
        easelFeature.SetActive(false);
        bonsaiFeature.SetActive(false);
        paintingFeature1.SetActive(false);
        paintingFeature2.SetActive(false);
        temperaBookCover.SetActive(false);
        temperaBookContent.SetActive(false);
        flagGlassWall = false;
        glassWallWet = false;
        flagMatch = false;
        flagLocker = false;
        flagInformation = false;
        page = 0;
    }

    // Update is called once per frame
    void Update()
    {
        glassWall.SetActive(flagGlassWall);
        // 保证是触发视图8跳转
        if (Camera.main.transform.position.x == 60 && Camera.main.transform.position.y == 0)
        {
            // 检测触摸开始或鼠标按下
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
            {
                // 获取触摸位置或鼠标点击位置
                Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                touchStartPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                // 将触摸位置的 Z 值设置为场景的 Z 值
                touchStartPosition.z = desk.transform.position.z;
            }
            // 检测触摸结束或鼠标弹起
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
            {
                // 获取触摸位置或鼠标点击位置
                Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                touchEndPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                // 将触摸位置的 Z 值设置为场景的 Z 值
                touchEndPosition.z = desk.transform.position.z;
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
        // 玻璃窗下
        if (glassWall.activeSelf && !glassWallWet)
        {
            if (wordCanvas.activeSelf)
            {
                wordCanvas.SetActive(false);
            }
            else if(glassWall.GetComponent<BoxCollider2D>().bounds.Contains(worldTouchPosition) && !glassWall.GetComponent<CircleCollider2D>().bounds.Contains(worldTouchPosition))
            {
                Item item = GlobalManager.FindItem("07");
                if (item != null && item.isSelected)
                {
                    if (Angle4Controller.GetFlagWater() != 0)
                    {
                        glassWall.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("angle8/angle8-2-(2)");
                        glassWallWet = true;
                        GlobalManager.RemoveItem(item);
                        GlobalManager.RemoveItem(item);
                        GlobalManager.RemoveItem(item);
                        GlobalManager.RemoveItem(item);
                        GlobalManager.RemoveItem(item);
                        Angle4Controller.SetFlagWater(0);
                    }
                    else
                    {
                        SetWord(1, "杯子里没有水, 嘿, 我泼的不是空气, 是寂寞");
                    }
                }
                else
                {
                    if (GlobalManager.someItemIsSelected)
                    {
                        SetWord(1, "我拿这个做什么? 把墙壁砸开么");
                    }
                    else
                    {
                        SetWord(1, "好厚的玻璃墙，应该无法砸开。不过总感觉上面涂了什么，可惜看不出来");
                    }
                }
            }
        } 
        // 从视角8到视角3
        if (backArrow && backArrow.activeSelf && backArrow.GetComponent<Collider2D>().bounds.Contains(worldTouchPosition))
        {
            Debug.Log("8->3");
            Camera.main.transform.position = new Vector3(30, 0, -10);
            leftArrow.SetActive(!leftArrow.activeSelf);
            rightArrow.SetActive(!rightArrow.activeSelf);
            backArrow.SetActive(!backArrow.activeSelf);
            if (flagGlassWall)
            {
                flagGlassWall = false;
                EnableOtherColliders();
                darkness.SetActive(false);
            }
        }
        else if (wordCanvas.activeSelf)
        {
            wordCanvas.SetActive(false);
        }
        else if (!darkness.activeSelf)
        {
            // 点击盆景，进入特写
            if (bonsai.activeSelf && bonsai.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                bonsaiFeature.SetActive(true);
                bonsaiFeatureMatch.SetActive(!flagMatch);
                DisableOtherColliders(bonsaiFeature);
                bonsaiFeatureMatch.GetComponent<PolygonCollider2D>().enabled = true;
                darkness.SetActive(true);
                if (!flagMatch)
                {
                    SetWord(1, "有点深, 这个火柴盒没办法直接取到");
                }
            }
            // 点击柜门打开
            if (!flagLocker && bookcase.activeSelf && bookcase.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                Item item = GlobalManager.FindItem("08");
                if (item != null && item.isSelected)
                {
                    flagLocker = true;
                    bookcase.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("angle8/angle8-3-(2)");
                }
                else if (GlobalManager.someItemIsSelected)
                {
                    SetWord(1, "用这个可没办法打开");
                }
                else
                {
                    SetWord(1, "很奇怪的玻璃柜, 居然也有锁. 一幅画而已, 还能藏着什么……不过画框里确实能藏东西");
                }
                
            }
            // 点击画，进入特写
            else if (flagLocker && painting.activeSelf && painting.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                paintingFeature1.SetActive(true);
                paintingFeature2.SetActive(false);
                paintingFeatureKey.SetActive(false);
                DisableOtherColliders(paintingFeature1);
                paintingFeature2.GetComponent<PolygonCollider2D>().enabled = true;
                darkness.SetActive(true);
                SetWord(1, "这个画框居然真的能打开");
            }
            // 点击画架，进入特写
            else if (easel.activeSelf && easel.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                easelFeature.SetActive(true);
                DisableOtherColliders(easelFeature);
                easelFeature.transform.Find("information").gameObject.SetActive(flagInformation);
                darkness.SetActive(true);
                SetWord(1, "手感怪怪的, 但是怎么是空白的. 总不至于说要我在上面画画……我可不会");
            }
            // 点击桌面，进入特写
            if (desk.activeSelf && desk.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                deskFeature.SetActive(true);
                EnableDeskFeatureColliders(deskFeature.transform);
                darkness.SetActive(true);
                SetWord(1, "很奇怪的桌子, 是要做什么. 桌子上的水果? 她打算画那个?");
            }
            // 点击蛋彩画书，进入特写
            if (temperaBook.activeSelf && temperaBook.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                temperaBookCover.SetActive(true);
                temperaBookContent.SetActive(false);
                DisableOtherColliders(temperaBookCover);
                temperaBookContent.GetComponent<PolygonCollider2D>().enabled = true;
                darkness.SetActive(true);
                SetWord(1, "虽然有点高,但是还是能够得着的. 蛋彩画? 好像听过一点");
            }
        }
        // 盆景特写
        else if (bonsaiFeature.activeSelf)
        {
            // 退出特写
            if (!bonsaiFeature.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition)
            && darkness.GetComponent<BoxCollider2D>().bounds.Contains(worldTouchPositionD))
            {
                bonsaiFeature.SetActive(false);
                EnableOtherColliders();
                darkness.SetActive(false);
            }
            // 获得火柴
            if (bonsaiFeatureMatch.activeSelf && bonsaiFeatureMatch.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                Item item = GlobalManager.FindItem("21");
                if (item != null && item.isSelected)
                {
                    SetWord(1, "总算拿到了!");
                    GlobalManager.RemoveItem(item);
                    GlobalManager.AddItem("19", Resources.Load<Sprite>("others/matchbox"));
                    flagMatch = true;
                    bonsaiFeatureMatch.SetActive(!flagMatch);
                }
                else
                {
                    if (GlobalManager.someItemIsSelected)
                    {
                        SetWord(1, "用这个可够不到它");
                    }
                    else
                    {
                        SetWord(1, "太深了, 不适用什么工具我没办法把它弄出来");
                    }
                }  
            }
        }
        // 画特写
        else if (paintingFeature1.activeSelf || paintingFeature2.activeSelf)
        {
            if (paintingFeature1.activeSelf)
            {
                // 取消特写
                if (!paintingFeature1.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition)
                && darkness.GetComponent<BoxCollider2D>().bounds.Contains(worldTouchPositionD))
                {
                    paintingFeature1.SetActive(false);
                    EnableOtherColliders();
                    darkness.SetActive(false);
                }
                // 打开移动画
                else 
                {
                    paintingFeature1.SetActive(false);
                    paintingFeature2.SetActive(true);
                    paintingFeatureKey.SetActive(!flagKey);
                    if (!flagKey)
                    {
                        SetWord(1, "居然又是钥匙. 真会藏啊NN");
                    }
                }
            } 
            else
            {
                // 取消特写
                if (!paintingFeature2.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition)
                && darkness.GetComponent<BoxCollider2D>().bounds.Contains(worldTouchPositionD))
                {
                    paintingFeature2.SetActive(false);
                    EnableOtherColliders();
                    darkness.SetActive(false);
                }
                // 获得钥匙
                if (paintingFeatureKey.activeSelf && paintingFeatureKey.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    GlobalManager.AddItem("20", Resources.Load<Sprite>("others/key"));
                    flagKey = true;
                    paintingFeatureKey.SetActive(false);
                }
            }
        }
        // 画架特写
        else if (easelFeature.activeSelf)
        {
            // 取消特写
            if (!easelFeature.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition)
            && darkness.GetComponent<BoxCollider2D>().bounds.Contains(worldTouchPositionD))
            {
                easelFeature.SetActive(false);
                EnableOtherColliders();
                darkness.SetActive(false);
            }
            // 其他操作
        } 
        // 桌面特写
        else if (deskFeature.activeSelf)
        {
            GameObject back = deskFeature.transform.Find("back").gameObject;
            // 退出特写
            if (back.activeSelf && back.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition)
            && darkness.GetComponent<BoxCollider2D>().bounds.Contains(worldTouchPositionD))
            {
                deskFeature.SetActive(false);
                EnableOtherColliders();
                darkness.SetActive(false);
            }
            
        }
        // 蛋彩画特写 取消特写
        else if ((temperaBookCover.activeSelf && !temperaBookCover.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition) || temperaBookContent.activeSelf && !temperaBookContent.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
        && darkness.GetComponent<BoxCollider2D>().bounds.Contains(worldTouchPositionD))
        {
            temperaBookCover.SetActive(false);
            temperaBookContent.SetActive(false);
            EnableOtherColliders();
            darkness.SetActive(false);
            SetWord(1, "感觉没有把调制过程讲的很清楚，嗯，如果需要用到的话就干脆试试看把所有材料都放在一起再混合均匀");
        }
        else if(darkness2.activeSelf)
        {
            SetWord(1,"太黑了, 什么都看不见, 不过说不定黑暗中也会有什么信息存在, 毕竟小说里往往是这么写的哈哈");
        }
    }
    void HandleDrag(Vector3 worldTouchPosition , bool direction)
    {
        // 蛋彩画特写切换特写
        if (temperaBookCover.activeSelf || temperaBookContent.activeSelf)
        {
            if (temperaBookCover.activeSelf)
            {
                temperaBookCover.SetActive(false);
                temperaBookContent.SetActive(true);
                for (int i = 1 ; i <= 5 ; i++)
                {
                    temperaBookContent.transform.Find("word" + i).gameObject.SetActive(false);
                }
            }
            else 
            {
                temperaBookContent.transform.Find("word" + page).gameObject.SetActive(false);
            }
            if (direction)  // 从左往右，向前翻
            {
                page = (page + 5) % 6;
            }
            else
            {
                page = (page + 1) % 6;
            }
            if (page == 0)
            {
                temperaBookCover.SetActive(true);
                temperaBookContent.SetActive(false);
            }
            else
            {
                temperaBookContent.transform.Find("word" + page).gameObject.SetActive(true);
            }
        }
    }
    public void FlagGlassWall(bool flag)
    {
        flagGlassWall = flag;
        glassWall.SetActive(flag);
        DisableOtherColliders(glassWall);  
        backArrow.GetComponent<Collider2D>().enabled = true;
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
    void EnableDeskFeatureColliders(Transform parent)
    {
        // 检查父对象是否有PolygonCollider2D组件，如果有则启用
        PolygonCollider2D collider = parent.GetComponent<PolygonCollider2D>();
        if (collider != null)
        {
            collider.enabled = true;
        }

        // 递归遍历每个子对象
        foreach (Transform child in parent)
        {
            EnableDeskFeatureColliders(child);
        }
    }
    public void SetInformationActive()
    {
        flagInformation = true;
    }
    void SetWord(int n,string txt)
    {
        wordCanvas.SetActive(true);
        word.text = txt;
        owner.text = "J:";
    }
}
