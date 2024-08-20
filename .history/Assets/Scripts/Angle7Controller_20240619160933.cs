using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle7Controller : MonoBehaviour
{
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject backArrow;
    private GameObject darkness;
    private GameObject darkness2;
    public GameObject seat1;
    public GameObject seat2;
    public GameObject cabinet1;
    public GameObject cabinet1Open;
    public GameObject cabinet1Diary;
    public GameObject cabinet2;
    public GameObject cabinet2Open;
    public GameObject cabinet2Book;
    public GameObject diaryLock;
    public GameObject diaryLockNumber1;
    public GameObject diaryLockNumber2;
    public GameObject diaryLockNumber3;
    public GameObject diaryLockNumber4;
    public GameObject diaryContent;
    public GameObject bookClose;
    public GameObject bookOpen;
    public GameObject bookContent;
    public GameObject computer;
    public GameObject computerButton;
    public GameObject computerBody;
    public GameObject computerOpen;
    public GameObject computerDesktop;
    public GameObject computerDesktopIcon1;
    public GameObject computerDesktopIcon2;
    private int psw1, psw2, psw3, psw4;
    private int diaryPage;
    private bool flagDiaryUnlock; //unlock : true
    private bool flagComputerOpen;  // open ： true
    private Vector3 touchStartPosition; // 存储鼠标按下或开始触摸时的位置
    private Vector3 touchEndPosition; // 存储鼠标弹起或结束触摸时的位置

    // Start is called before the first frame update
    void Start()
    {
        darkness = Camera.main.transform.Find("darkness").gameObject;
        darkness2 = Camera.main.transform.Find("darkness (1)").gameObject;
        seat2.SetActive(false);
        cabinet1Open.SetActive(false);
        cabinet2Open.SetActive(false);
        diaryContent.SetActive(false);
        diaryLock.SetActive(false);
        bookOpen.SetActive(false);
        bookClose.SetActive(false);
        computerBody.SetActive(false);
        flagDiaryUnlock = false;
        flagComputerOpen = false;
        psw1 = psw2 = psw3 = psw4 = 0;
        diaryPage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // 保证是触发视图7跳转
        if (Camera.main.transform.position.x == 60 && Camera.main.transform.position.y == 15)
        {
            // 检测触摸开始或鼠标按下
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
            {
                // 获取触摸位置或鼠标点击位置
                Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                touchStartPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                // 将触摸位置的 Z 值设置为场景的 Z 值
                touchStartPosition.z = seat1.transform.position.z;
            }
            // 检测触摸结束或鼠标弹起
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
            {
                // 获取触摸位置或鼠标点击位置
                Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                touchEndPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                // 将触摸位置的 Z 值设置为场景的 Z 值
                touchEndPosition.z = seat1.transform.position.z;
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
        // 从视角7到视角2
        if (backArrow && backArrow.activeSelf && backArrow.GetComponent<Collider2D>().bounds.Contains(worldTouchPosition))
        {
            Debug.Log("7->2");
            Camera.main.transform.position = new Vector3(-30, 0, -10);
            leftArrow.SetActive(!leftArrow.activeSelf);
            backArrow.SetActive(!backArrow.activeSelf);
        }
        // 从视角7到视角6
        if (rightArrow && rightArrow.activeSelf && rightArrow.GetComponent<Collider2D>().bounds.Contains(worldTouchPosition))
        {
            Debug.Log("7->6");
            Camera.main.transform.position = new Vector3(30, 15, -10);
            leftArrow.SetActive(!leftArrow.activeSelf);
            rightArrow.SetActive(!rightArrow.activeSelf);
        }
        if (!darkness.activeSelf)
        {
            // 点击座椅，拆卸 获得座椅脚
            if (seat1.activeSelf && seat1.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                Item item = GlobalManager.FindItem("04");
                if (item != null && item.isSelected)
                {
                    Debug.Log("获得座椅脚");
                    GlobalManager.RemoveItem(item);
                    GlobalManager.AddItem("18", Resources.Load<Sprite>("others/leg of chair"));
                    seat2.SetActive(true);
                    seat1.SetActive(false);
                }
                else
                {
                    if (GlobalManager.someItemIsSelected)
                    {
                        Debug.Log("不应该使用这个");
                    }
                    else
                    {
                        Debug.Log("可以用工具把它拆开");
                    }
                }
            }
            // 点击电脑进入特写
            else if (computer.activeSelf && computer.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                computerBody.SetActive(true);
                computerOpen.SetActive(flagComputerOpen);
                computerDesktop.SetActive(flagComputerOpen);
                darkness.SetActive(true);
                DisableOtherColliders(computerBody);
                // 取消禁用其他的电脑桌面触发器
                computerButton.GetComponent<CircleCollider2D>().enabled = true;
            }
            // 点击日记进入特写
            if (cabinet1Diary.activeSelf && cabinet1Diary.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                diaryLock.SetActive(true);
                darkness.SetActive(true);
                DisableOtherColliders(diaryLock);
                diaryLockNumber1.GetComponent<PolygonCollider2D>().enabled = true;
                diaryLockNumber2.GetComponent<PolygonCollider2D>().enabled = true;
                diaryLockNumber3.GetComponent<PolygonCollider2D>().enabled = true;
                diaryLockNumber4.GetComponent<PolygonCollider2D>().enabled = true;
                
                // 若未解开，显示默认密码0000,解开则不显示密码
                if(!flagDiaryUnlock)
                {
                    Sprite newSprite = Resources.Load<Sprite>("others/number-0");
                    diaryLockNumber1.GetComponent<SpriteRenderer>().sprite = newSprite; 
                    diaryLockNumber2.GetComponent<SpriteRenderer>().sprite = newSprite; 
                    diaryLockNumber3.GetComponent<SpriteRenderer>().sprite = newSprite; 
                    diaryLockNumber4.GetComponent<SpriteRenderer>().sprite = newSprite; 
                    psw1 = psw2 = psw3 = psw4 = 0;
                } 
                else
                {
                    diaryLockNumber1.SetActive(false);
                    diaryLockNumber2.SetActive(false);
                    diaryLockNumber3.SetActive(false);
                    diaryLockNumber4.SetActive(false);
                }
            }
            // 点击密码本进入特写
            if (cabinet2Book.activeSelf && cabinet2Book.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                bookClose.SetActive(true);
                darkness.SetActive(true);
                DisableOtherColliders(bookClose);
                bookOpen.GetComponent<PolygonCollider2D>().enabled = true;
                bookContent.GetComponent<PolygonCollider2D>().enabled = true;
            }
            // 关闭床头柜1
            if (cabinet1Open.activeSelf && cabinet1Open.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                cabinet1Open.SetActive(false);
            }
            // 打开床头柜1
            else if (cabinet1.activeSelf && cabinet1.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                cabinet1Open.SetActive(true);
            }
            // 关闭床头柜2
            else if (cabinet2Open.activeSelf && cabinet2Open.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                cabinet2Open.SetActive(false);
            }
            // 打开床头柜2
            else if (cabinet2.activeSelf && cabinet2.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                cabinet2Open.SetActive(true);
            }
        }
        // 日记本特写
        else if (diaryLock.activeSelf || diaryContent.activeSelf)
        {
            // 点击外部取消特写
            if (diaryLock.activeSelf && !diaryLock.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition)
            && darkness.GetComponent<BoxCollider2D>().bounds.Contains(worldTouchPositionD))
            {
                diaryLock.SetActive(false);
                EnableOtherColliders();
                darkness.SetActive(false);
            }
            // 解密码锁 diaryLock active
            else if (!flagDiaryUnlock)
            {
                if (diaryLockNumber1.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    psw1 = (psw1 + 1) % 10;
                    diaryLockNumber1.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("others/number-" + psw1);
                }
                if (diaryLockNumber2.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    psw2 = (psw2 + 1) % 10;
                    diaryLockNumber2.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("others/number-" + psw2);
                }
                if (diaryLockNumber3.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    psw3 = (psw3 + 1) % 10;
                    diaryLockNumber3.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("others/number-" + psw3);
                }
                if (diaryLockNumber4.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    psw4 = (psw4 + 1) % 10;
                    diaryLockNumber4.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("others/number-" + psw4);
                }
                if (psw1 == 0 && psw2 == 5 && psw3 == 2 && psw4 == 7)
                {
                    flagDiaryUnlock = true;
                    diaryLockNumber1.GetComponent<PolygonCollider2D>().enabled = false;
                    diaryLockNumber2.GetComponent<PolygonCollider2D>().enabled = false;
                    diaryLockNumber3.GetComponent<PolygonCollider2D>().enabled = false;
                    diaryLockNumber4.GetComponent<PolygonCollider2D>().enabled = false;
                }
            }
            if (diaryContent.activeSelf && !diaryContent.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition)
            && darkness.GetComponent<BoxCollider2D>().bounds.Contains(worldTouchPositionD))
            {
                diaryContent.SetActive(false);
                EnableOtherColliders();
                darkness.SetActive(false);
            }
        }
        // 密码本特写
        else if (bookClose.activeSelf || bookOpen.activeSelf)
        {
            // 点击外部取消特写
            if ((bookClose.activeSelf && !bookClose.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            || (bookOpen.activeSelf && !bookOpen.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition)))
            {
                
            }
            // 点击内部翻页
        }
        // 电脑特写
        else if (computer.activeSelf)
        {
            // 点击外，取消特写
            if (computerBody.activeSelf && !computerBody.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition)
            && darkness.GetComponent<BoxCollider2D>().bounds.Contains(worldTouchPositionD))
            {
                computerBody.SetActive(false);
                EnableOtherColliders();
                darkness.SetActive(false);
            }
            // 点击开关
            else if (computerButton.activeSelf && computerButton.GetComponent<CircleCollider2D>().bounds.Contains(worldTouchPosition))
            {
                // 打开电脑
                if (!flagComputerOpen)
                {
                    flagComputerOpen = true;
                    StartCoroutine(ComputerOpening());
                } else
                {
                    Debug.Log("没有必要关闭电脑吧");
                }
            }
            // 与电脑的交互

        }
        else if(darkness2.activeSelf)
        {
            Debug.Log("太黑了，你无法进行探索");
        }
    }
    void HandleDrag(Vector3 worldTouchPosition, bool direction)
    {
        // 日记本
        if (flagDiaryUnlock)
        {
            if (diaryLock.activeSelf && diaryLock.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                diaryContent.SetActive(true);
                diaryLock.SetActive(false);
                if (direction) // 从左往右
                {          
                    diaryPage = 11;
                } else {
                    diaryPage = 1;
                }
                diaryContent.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("angle7/angle7-4-(" + (diaryPage + 1) + ")");
            }
            else if (diaryContent.activeSelf && diaryContent.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                if (direction) // 从左往右 倒序
                {
                    diaryPage --;
                }
                else    // 从右往左 正序
                {
                    diaryPage = (diaryPage + 1) % 12;
                }
                if (diaryPage == 0) // 日记封面
                {
                    diaryLock.SetActive(true);
                    diaryContent.SetActive(false);
                }
                else {
                    diaryContent.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("angle7/angle7-4-(" + (diaryPage + 1) + ")");
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
    IEnumerator ComputerOpening()
    {
        computerOpen.SetActive(true);

        // 等待3秒
        yield return new WaitForSeconds(1f);

        computerOpen.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("angle7/angle7-7-(3)");

        //等待3秒
        yield return new WaitForSeconds(1f);

        computerDesktop.SetActive(true);
    }
}
