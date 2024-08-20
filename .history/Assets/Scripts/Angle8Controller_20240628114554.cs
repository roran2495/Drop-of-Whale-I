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
        if (wordCanvas.activeSelf)
        {
            wordCanvas.SetActive(false);
        }
        else 
        {
            if (glassWall.activeSelf && !glassWallWet)
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
