using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle6Controller : MonoBehaviour
{
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject backArrow;
    public GameObject wardrobe;
    public GameObject wardrobeOpen;
    public GameObject wardrobeBox;
    public GameObject wardrobeBoxAromatheropy;
    public GameObject plushToy;
    public GameObject plushToyBed;
    public GameObject plushToyOpen;
    public GameObject plushToyKey;
    private bool flagAromatheropy; // donnet get : false ; already get : true
    private bool flagKey;   // donnet get : false ; already get : true
    private bool flagToy;   // open : true;
    private Vector3 touchStartPosition; // 存储鼠标按下或开始触摸时的位置
    private Vector3 touchEndPosition; // 存储鼠标弹起或结束触摸时的位置
    // Start is called before the first frame update
    void Start()
    {
        wardrobeOpen.SetActive(false);
        plushToy.SetActive(false);
        flagAromatheropy = false;
        flagKey = false;
        flagToy = false;
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
                touchStartPosition.z = refrigerator.transform.position.z;
            }
            // 检测触摸结束或鼠标弹起
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
            {
                // 获取触摸位置或鼠标点击位置
                Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                touchEndPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                // 将触摸位置的 Z 值设置为场景的 Z 值
                touchEndPosition.z = refrigerator.transform.position.z;
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
        if (!Camera.main.transform.Find("darkness").gameObject.activeSelf)
        {
            // 点击鲸鱼，开启特写
            if (plushToyBed.activeSelf && plushToyBed.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                plushToy.SetActive(true);
                plushToyKey.SetActive(flagKey);
                

            }
        } 
        //特写鲸鱼处理
        else if (plushToy.activeSelf)
        {
            // 点击空白处，退出特写
            if (!plushToy.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                plushToy.SetActive(false);
                Camera.main.Find("darkness").gameObject.SetActive(false);
                EnableOtherColliders();
            }
            else 
            {
                // 获取钥匙
                if (flagToy && !flagKey && plushToyKey.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    flagKey = true;
                    plushToyKey.SetActive(false);
                }
                // 点击拉链打开
                else if (!flagToy && plushToyOpen.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    flagToy = true;
                    plushToy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("angle6/angle6-4-(2)");
                }
            }
        } else {
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
}
