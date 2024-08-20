using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle1DetailController : MonoBehaviour
{
    public GameObject curtain;
    private bool flagCurtain;    // unuse : false   ;     use: true
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
        curtain.SetActive(true);
        flagCurtain = false;
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
                worldTouchPosition.z = curtain.transform.position.z;

                // 点击窗帘 ： 打开or关闭
                if (curtain && curtain.activeSelf && curtain.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    Debug.Log("点击窗帘");
                    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                    if (!flagCurtain) // unuse -> use
                    {
                        newSprite = Resources.Load<Sprite>("Arts/angle1/angle1-2");
                    } else {
                        spriteRenderer newSprite = Resources.Load<Sprite>("Arts/angle1/angle1-1");
                    }
                    flagCurtain = !flagCurtain;
                    spriteRenderer.Sprite = newSprite; 
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
