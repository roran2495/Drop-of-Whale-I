using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle8Controller : MonoBehaviour
{
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject backArrow;
    public GameObject glassWall;
    public GameObject match;
    public GameObject desk;
    public GameObject deskFeature;
    public GameObject easel;
    public GameObject easelFeature;
    public GameObject bonsal;
    public GameObject bonsalFeature;
    public GameObject bonsalFeatureMatch;
    public GameObject painting;
    public GameObject paintingFeature1;
    public GameObject paintingFeature2;
    public GameObject paintingFeatureKey;
    public GameObject temperaBook;
    public GameObject temperBookCover;
    public GameObject temperBookContent;
    public GameObject exchangeBook;
    private bool glassWallWet; //wet : true
    private bool flagGlassWall;
    private Vector3 touchStartPosition; // 存储鼠标按下或开始触摸时的位置
    private Vector3 touchEndPosition; // 存储鼠标弹起或结束触摸时的位置
    // Start is called before the first frame update
    void Start()
    {
        glassWall.SetActive(flagGlassWall);
        deskFeature.SetActive(false);
        easelFeature.SetActive(false);
        bonsalFeature.SetActive(false);
        paintingFeature1.SetActive(false);
        paintingFeature2.SetActive(false);
        temperaBookCover.SetActive(false);
        temperBookContent.SetActive(false);
        flagGlassWall = false;
        glassWallWet = false;
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
        // 玻璃窗下
        if (glassWall.activeSelf && !glassWallWet)
        {
            if(glassWall.GetComponent<BoxCollider2D>().bounds.Contains(worldTouchPosition) && !glassWall.GetComponent<CircleCollider2D>().bounds.Contains(worldTouchPosition))
            {
                glassWall.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("angle8/angle8-2-(2)");
                glassWallWet = true;
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
            }
        }
    }
    void HandleDrag(Vector3 worldTouchPosition , bool direction)
    {

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
}
