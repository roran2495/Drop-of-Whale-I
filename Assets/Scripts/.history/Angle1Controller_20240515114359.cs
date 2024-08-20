using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle1Controller : MonoBehaviour
{
    public GameObject doorToAngle5;
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject backArrow;
    public GameObject darkness;
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
    public GameObject letter;
    public GameObject phone;
    public GameObject shoeCabinet;
    // Start is called before the first frame update
    void Start()
    {
        flagCurtain = false;
        flagSwitcher = false;
        pillow1.SetActive(true);
        pillow2.SetActive(false);
        broom1.SetActive(true);
        broom2.SetActive(false);
        carpet1.SetActive(true);
        carpet2.SetActive(false);
        letter.SetActive(false);
        phone.SetActive(false);
        shoeCabinet.SetActive(false);
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
                // 将触摸位置的 Z 值设置为场景的 Z 值
                worldTouchPosition.z = doorToAngle5.transform.position.z;

                // 从视角1到视角5
                if (doorToAngle5 && doorToAngle5.activeSelf && doorToAngle5.GetComponent<Collider2D>().bounds.Contains(worldTouchPosition))
                {
                    Debug.Log("5->1");
                    Camera.main.transform.position = new Vector3(0, 15, -10);
                    leftArrow.SetActive(!leftArrow.activeSelf);
                    rightArrow.SetActive(!rightArrow.activeSelf);
                    backArrow.SetActive(!backArrow.activeSelf);
                }
                // 点击窗帘 ： 不使用or使用
                if (curtain && curtain.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition) && !leftArrow.GetComponent<CircleCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    Debug.Log("点击窗帘");
                    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                    Sprite newSprite = Resources.Load<Sprite>("angle1/angle1-1");;
                    if (!flagCurtain) // unuse -> use
                    {
                        newSprite = Resources.Load<Sprite>("angle1/angle1-2");
                    }
                    flagCurtain = !flagCurtain;
                    spriteRenderer.sprite = newSprite; 
                }
                // 点击开关：打开or关闭
                if (switcher && switcher.activeSelf && switcher.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    Debug.Log("点击开关");
                    flagSwitcher = !flagSwitcher;
                }
                // 窗帘使用+开关打开：黑暗效果
                if (flagCurtain && flagSwitcher)
                {
                    darkness.SetActive(true);
                } else {
                    darkness.SetActive(false);
                }
                // 点击抱枕1 ：移动（显示抱枕2）
                // 点击抱枕2 ：移动（显示抱枕1）
                // 点击扫把1 ：拆卸（显示扫把2）
                // 点击地毯1 ：获取钥匙（显示地毯2）
                // 点击信件 ：显示信件
                // 点击信件外 ：取消特写
                // 点击电话 ： 显示电话，实现拨号
                // 点击电话外 ：取消特写
                // 点击鞋柜 ：显示鞋柜
                // 点击鞋柜外 ：取消特写
            }
        }
    }
}
