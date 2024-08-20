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
    public GameObject bookcaseBook;
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
    private Vector3 touchStartPosition; // 存储鼠标按下或开始触摸时的位置
    private Vector3 touchEndPosition; // 存储鼠标弹起或结束触摸时的位置

    // Start is called before the first frame update
    void Start()
    {
        doorToBalconyClose.SetActive(false);
        bookcaseBook.SetActive(false);
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
                touchStartPosition.z = doorToAngle4.transform.position.z;
            }
            // 检测触摸结束或鼠标弹起
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
            {
                // 获取触摸位置或鼠标点击位置
                Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                touchEndPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                // 将触摸位置的 Z 值设置为场景的 Z 值
                touchEndPosition.z = doorToAngle4.transform.position.z;
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
            // 点击衣服，获取衣服
            if (clothes.activeSelf && clothes.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                Debug.Log("获取衣服");
                flagClothes = true;
                clothes.SetActive(!flagClothes);
            }
            // 点击阳台门 ： 打开阳台
            else if (doorToBalcony.activeSelf && doorToBalcony.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                doorToBalconyClose.SetActive(true);
                clothes.SetActive(!flagClothes);
            }
            // 点击阳台门，关闭
            else if (doorToBalconyClose.activeSelf && doorToBalconyClose.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                doorToBalconyClose.SetActive(false);
                clothes.SetActive(false);
            } 
            //点击玻璃窗，切换显示视角8（玻璃窗）
            else if (glassWall.activeSelf && glassWall.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                Debug.Log("玻璃窗视角");
            }
            //点击书，显示特写
            if (bookcaseBook.activeSelf && bookcaseBook.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                Camera.main.transform.Find("darkness").gameObject.SetActive(true);
                book0.SetActive(true);
                book1.SetActive(true);
                DisableOtherColliders(book0);
            } 
            //点击书架左
            else if (OpenBookcaseLeft.activeSelf && OpenBookcaseLeft.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                switch (flagBookcase)
                {
                    case 0 :
                        Debug.Log("打开书架左");
                        bookcaseBook.SetActive(true);
                        bookcase.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("angle3/angle3-2-(2)");
                        flagBookcase = 1;
                        break ;
                    case 1 :
                        Debug.Log("关闭书架左");
                        bookcaseBook.SetActive(false);
                        bookcase.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("angle3/angle3-2-(1)");
                        flagBookcase = 0;
                        break ;
                    default :
                        Debug.Log("关闭书架右，打开书架左");
                        bookcaseBook.SetActive(true);
                        bookcase.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("angle3/angle3-2-(2)");
                        flagBookcase = 1;
                        break;
                }
            }
            //点击书架右
            else if (OpenBookcaseRight.activeSelf && OpenBookcaseRight.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                switch (flagBookcase)
                {
                    case 0 :
                        Debug.Log("打开书架右");
                        bookcase.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("angle3/angle3-2-(3)");
                        flagBookcase = 2;
                        break ;
                    case 2 :
                        Debug.Log("关闭书架右");
                        bookcase.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("angle3/angle3-2-(1)");
                        flagBookcase = 0;
                        break ;
                    default :
                        Debug.Log("关闭书架左，打开书架右");
                        bookcaseBook.SetActive(false);
                        bookcase.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("angle3/angle3-2-(3)");
                        flagBookcase = 2;
                        break ;
                }
            }
            

            //点击标记，显示标记
            if (mark.activeSelf && mark.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                markShow.SetActive(true);
            }            
            
        }
        //点击书外，关闭特写
        else if (book0.activeSelf && !book0.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
        {
            Camera.main.transform.Find("darkness").gameObject.SetActive(false);
            EnableOtherColliders();
            book0.SetActive(false);
                book1.SetActive(false);
                book2.SetActive(false);
                book3.SetActive(false);
                book4.SetActive(false);
                book5.SetActive(false);
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
        else if (doorToAngle8 && doorToAngle8.activeSelf && doorToAngle8.GetComponent<BoxCollider2D>().bounds.Contains(worldTouchPosition) && !doorToAngle8.GetComponent<CircleCollider2D>().bounds.Contains(worldTouchPosition))
        {
            Debug.Log("3->8");
            Camera.main.transform.position = new Vector3(60 , 0, -10);
            leftArrow.SetActive(!leftArrow.activeSelf);
            rightArrow.SetActive(!rightArrow.activeSelf);
            backArrow.SetActive(!backArrow.activeSelf);
        }
        else if (Camera.main.transform.Find("darkness").gameObject.activeSelf && !book0.activeSelf)
        {
            Debug.Log("太黑了，你无法进行探索");
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
