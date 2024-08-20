using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle4Controller : MonoBehaviour
{
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject backArrow;
    public GameObject curtain;
    public GameObject curtainOpen;
    public GameObject water;
    public GameObject washer;
    public GameObject washerClothes;
    public GameObject washerOpen;
    public GameObject washerKey;
    public GameObject washerToothCup1;
    public GameObject washerToothCup2;
    public GameObject place;
    public GameObject washerToothCup3;
    public GameObject tap;
    public GameObject mark;
    public GameObject markShow;
    private Vector3 touchStartPosition; // 存储鼠标按下或开始触摸时的位置
    private Vector3 touchEndPosition; // 存储鼠标弹起或结束触摸时的位置
    // Start is called before the first frame update
    void Start()
    {
        curtainOpen.SetActive(false);
        water.SetActive(false);
        washerClothes.SetActive(false);
        washerOpen.SetActive(false);
        washerKey.SetActive(false);
        washerToothCup2.SetActive(false);
        washerToothCup3.SetActive(false);
        markShow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 保证是触发视图4跳转
        if (Camera.main.transform.position.x == -30 && Camera.main.transform.position.y == 15)
        {
            // 检测触摸开始或鼠标按下
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
            {
                // 获取触摸位置或鼠标点击位置
                Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                touchStartPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                // 将触摸位置的 Z 值设置为场景的 Z 值
                touchStartPosition.z = curtain.transform.position.z;
            }
            // 检测触摸结束或鼠标弹起
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
            {
                // 获取触摸位置或鼠标点击位置
                Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                touchEndPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                // 将触摸位置的 Z 值设置为场景的 Z 值
                touchEndPosition.z = curtain.transform.position.z;
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
        // 从视角4到视角3
        if (backArrow && backArrow.activeSelf && backArrow.GetComponent<Collider2D>().bounds.Contains(worldTouchPosition))
        {
             Debug.Log("4->3");
            Camera.main.transform.position = new Vector3(30, 0, -10);
            leftArrow.SetActive(!leftArrow.activeSelf);
            rightArrow.SetActive(!rightArrow.activeSelf);
            backArrow.SetActive(!backArrow.activeSelf);
        }
        if (!Camera.main.transform.Find("darkness").gameObject.activeSelf)
        {
            // 打开门帘
            // 关闭门帘
            if (curtainOpen.activeSelf && )
            // 显示标记
            // 获取牙杯
            // 放置牙杯
            // 拿起牙杯
            // 打开洗衣机
            // 关闭洗衣机
            // 放入衣服，出现钥匙
            // 拿起钥匙

        } else 
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
