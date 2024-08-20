using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle3Controller : MonoBehaviour
{
    public GameObject doorToAngle4;
    public GameObject doorToAngle8;
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject backArrow;
    public GameObject doorToBalcony;
    public GameObject doorToBalconyClose;
    public GameObject clothes;
    public GameObject bookcase;
    public GameObject OpenBookcaseLeft;
    public GameObject OpenBookcaseRight;
    public GameObject mark;
    public GameObject markShow;
    public GameObject book0;
    public GameObject book1;
    public GameObject book2;
    public GameObject book3;
    public GameObject book4;
    public GameObject book5;
    public GameObject glassWall;
    private bool flagClothes;   // donnot get : false ; already get : true
    private int flagBookcase;  // 0 : close ; 1: open left ; 2: open right
    // Start is called before the first frame update
    void Start()
    {
        doorToBalconyClose.SetActive(false);
        clothes.SetActive(false);
        markShow.SetActive(false);
        book0.SetActive(false);
        book1.SetActive(false);
        book2.SetActive(false);
        book3.SetActive(false);
        book4.SetActive(false);
        book5.SetActive(false);
        flagClothes = false;
        flagBookcase = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // 保证是触发视图3跳转
        if (Camera.main.transform.position.x == 30 && Camera.main.transform.position.y == 0)
        {
            // 检测触摸开始或鼠标按下
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
            {
                // 获取触摸位置或鼠标点击位置
                Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                touchStartPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                // 将触摸位置的 Z 值设置为场景的 Z 值
                touchStartPosition.z = doorToAngle7.transform.position.z;
            }
            // 检测触摸结束或鼠标弹起
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
            {
                // 获取触摸位置或鼠标点击位置
                Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                touchEndPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                // 将触摸位置的 Z 值设置为场景的 Z 值
                touchEndPosition.z = doorToAngle7.transform.position.z;
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
                    // 坐
                    Debug.Log("向左拖动");
                    HandleDrag(touchStartPosition , false);
                }
            }
        }
    }
    void HandleClick(Vector3 worldTouchPosition)
    {
        if (!Camera.main.transform.Find("darkness").gameObject.activeSelf)  //非黑暗状态
        {
            // 点击阳台门 ： 打开阳台
            if (doorToBalcony.activeSelf && doorToBalcony.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                doorToBalconyClose.SetActive(true);
                clothes.SetActive(!flagClothes);
            }
            // 点击衣服，获取衣服
            if (clothes.activeSelf && clothes.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                Debug.Log("获取衣服");
                flagClothes = true;
                clothes.SetActive(!flagClothes);
            }
            // 点击阳台门，关闭
            if (doorToBalconyClose.activeSelf && doorToBalconyClose.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                doorToBalconyClose.SetActive(false);
                clothes.SetActive(false);
            }
            //点击书架左
            if (OpenBookcaseLeft.activeSelf && OpenBookcaseLeft.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                switch (flagBookcase)
                {
                    case 0 :
                        Debug.Log("打开书架左");
                        break;
                    case 1 :
                        Debug.Log("关闭书架左");
                    default :
                        L
                }
            }
            //点击书架右
            //点击书，显示特写
            //点击书外，关闭特写
            
            //点击标记，显示标记

            //点击玻璃窗，切换显示视角8（玻璃窗）
        }
        // 从视角3到视角4
        if (doorToAngle4 && doorToAngle4.activeSelf && doorToAngle4.GetComponent<BoxCollider2D>().bounds.Contains(worldTouchPosition) && !doorToAngle4.GetComponent<CircleCollider2D>().bounds.Contains(worldTouchPosition))
        {
            Debug.Log("3->4");
            Camera.main.transform.position = new Vector3(-30, 15, -10);
            leftArrow.SetActive(!leftArrow.activeSelf);
            rightArrow.SetActive(!rightArrow.activeSelf);
            backArrow.SetActive(!backArrow.activeSelf);
        }
        // 从视角3到视角8
        if (doorToAngle8 && doorToAngle8.activeSelf && doorToAngle8.GetComponent<BoxCollider2D>().bounds.Contains(worldTouchPosition) && !doorToAngle8.GetComponent<CircleCollider2D>().bounds.Contains(worldTouchPosition))
        {
            Debug.Log("3->8");
            Camera.main.transform.position = new Vector3(60 , 0, -10);
            leftArrow.SetActive(!leftArrow.activeSelf);
            rightArrow.SetActive(!rightArrow.activeSelf);
            backArrow.SetActive(!backArrow.activeSelf);
        }
    }

    void HandleDrag(Vector3 worldTouchPosition , bool direction)
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
}
