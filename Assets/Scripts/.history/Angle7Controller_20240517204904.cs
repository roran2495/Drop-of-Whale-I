using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle7Controller : MonoBehaviour
{
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject backArrow;
    public GameObject seat1;
    public GameObject seat2;
    public GameObject cabinet;
    public GameObject cabinetOpen;
    public GameObject cabinetDiary;
    public GameObject DiaryLock;
    public GameObject DiaryLockNumber1;
    public GameObject DiaryLockNumber2;
    public GameObject DiaryLockNumber3;
    public GameObject DiaryLockNumber4;
    public GameObject DiaryContent;
    public GameObject computer;
    public GameObject computerButton;
    public GameObject computerBody;
    public GameObject computerOpen;
    public GameObject computerDesktop;
    public GameObject computerDesktopIcon1;
    public GameObject computerDesktopIcon2;
    private int psw1, psw2, psw3, psw4;
    private bool flagDiaryUnlock; //unlock : true
    private bool flagComputerOpen;  // open ： true
    private Vector3 touchStartPosition; // 存储鼠标按下或开始触摸时的位置
    private Vector3 touchEndPosition; // 存储鼠标弹起或结束触摸时的位置

    // Start is called before the first frame update
    void Start()
    {
        seat2.SetActive(false);
        DiaryContent.SetActive(false);
        DiaryLock.SetActive(false);
        computer.SetActive(false);
        flagDiaryUnlock = false
        flagComputerOpen = false;
        psw1 = psw2 = psw3 = psw4 = 0;
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
        if (!Camera.main.transform.Find("darkness").gameObject.activeSelf)
        {

        }
        // 日记本特写
        else if (DiaryLock.activeSelf || DiaryContent.activeSelf)
        {
            // 点击外部取消特写
            if (DiaryLock.activeSelf && DiaryLock.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                DiaryLock.SetActive(false);
                EnableOtherColliders();
                Camera.main.transform.Find("darkness").gameObject.SetActive(false);
            }
            // 解密码锁 DiaryLock active
            if (!flagDiaryUnlock)
            {
                if (password1.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    psw1 = (psw1 + 1) % 10;
                    password1.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("others/number-" + psw1);
                }
                if (password2.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    psw2 = (psw2 + 1) % 10;
                    password2.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("others/number-" + psw2);
                }
                if (password3.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    psw3 = (psw3 + 1) % 10;
                    password3.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("others/number-" + psw3);
                }
                if (password4.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    psw4 = (psw4 + 1) % 10;
                    password4.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("others/number-" + psw4);
                }
                if (psw1 == 9 && psw2 == 1 && psw3 == 7 && psw4 == 4)
                {
                    flagCabinet4 = true;
                    password1.GetComponent<PolygonCollider2D>().enabled = false;
                    password2.GetComponent<PolygonCollider2D>().enabled = false;
                    password3.GetComponent<PolygonCollider2D>().enabled = false;                        password4.GetComponent<PolygonCollider2D>().enabled = false;
                        }
            }
            
        }
        // 电脑特写
        else if (computer.activeSelf)
        {
            // 点击外，取消特写
            if (computerBody.activeSelf && computerBody.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                computerBody.SetActive(false);
                EnableOtherColliders();
                Camera.main.transform.Find("darkness").gameObject.SetActive(false);
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
        else
        {
            Debug.Log("太黑了，你无法进行探索");
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
        yield return new WaitForSeconds(3f);

        computerOpen.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("angle7/angle7-5-(3)");

        //等待3秒
        yield return new WaitForSeconds(3f);

        computerDesktop.SetActive(true);
    }
}
