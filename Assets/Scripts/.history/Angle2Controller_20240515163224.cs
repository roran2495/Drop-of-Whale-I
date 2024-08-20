using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle2Controller : MonoBehaviour
{
    public GameObject doorToAngle7;
    public GameObject leftArrow;
    public GameObject backArrow;
    public GameObject darkness;
    public GameObject television;
    public GameObject cabinet1;
    public GameObject cabinet1Content;
    public GameObject cabinet1Box;
    public GameObject cabinet2;
    public GameObject cabinet2Content;
    public GameObject cabinet2FirstAndKit;
    public GameObject cabinet3;
    public GameObject cabinet3Content;
    public GameObject cabinet3Pictures;
    public GameObject cabinet4;
    public GameObject cabinet4Content;
    public GameObject cabinet4Box;
    public GameObject cabinet5;
    public GameObject cabinet5Content;
    public GameObject cabinet5Keys;
    public GameObject cabinet6;
    public GameObject cabinet6Content;
    public GameObject cabinet6Instructions;
    public GameObject cabinet7;
    public GameObject cabinet7Content;
    public GameObject cabinet7Screwdriver;
    public GameObject picture0;
    public GameObject picture1;
    public GameObject picture2;
    public GameObject picture3;
    public GameObject instructions0;
    public GameObject instructions1;
    public GameObject instructions2;
    public GameObject instructions3;
    public GameObject instructions4;
    public GameObject instructions5;
    public GameObject box0;
    public GameObject box1;
    public GameObject box2;
    public GameObject box3;
    private bool flagCabinet2;
    private bool flagCabinet4;
    private bool flagCabinet5;
    private bool flagCabinet7;

    // Start is called before the first frame update
    void Start()
    {
        DisableSomeGameObject();
        // 判断是否处于黑暗中
        darkness.SetActive(Angle1Controller.flagCurtain && Angle1Controller.flagSwitcher);
    }

    // Update is called once per frame
    void Update()
    {
        // 保证是触发视图2跳转
        if (Camera.main.transform.position.x == -30 && Camera.main.transform.position.y == 0)
        {
            // 检测触摸输入或鼠标点击
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
            {
                // 获取触摸位置或鼠标点击位置
                Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                // 将触摸位置的 Z 值设置为场景的 Z 值
                worldTouchPosition.z = doorToAngle7.transform.position.z;

                // 从视角2到视角7
                if (doorToAngle7 && doorToAngle7.activeSelf && doorToAngle7.GetComponent<BoxCollider2D>().bounds.Contains(worldTouchPosition) && !doorToAngle7.GetComponent<CircleCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    Debug.Log("2->7");
                    Camera.main.transform.position = new Vector3(60, 15, -10);
                    leftArrow.SetActive(!leftArrow.activeSelf);
                    backArrow.SetActive(!backArrow.activeSelf);
                }

                // 点击柜子1 ：显示内容
                if (cabinet1.activeSelf && cabinet1.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    cabinet1Content.SetActive(true);
                    cabinet1Box.SetActive(true);
                }
                // 点击柜子1内容 ：关闭柜子1
                if (cabinet1Content.activeSelf && cabinet1Content.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    cabinet1Content.SetActive(false);
                    cabinet1Box.SetActive(false);
                }
                // 点击柜子1箱子 ： 文案交互
                if (cabinet1Box.activeSelf && cabinet1Box.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    Debug.Log("柜子1箱子");
                }
                // 点击柜子2 ：显示内容
                if (cabinet2.activeSelf && cabinet2.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    cabinet2Content.SetActive(true);
                    cabinet2FirstAndKit.SetActive(true);
                }
                // 点击柜子2内容 ：关闭柜子2
                if (cabinet2Content.activeSelf && cabinet2Content.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    cabinet2Content.SetActive(false);
                    cabinet2FirstAndKit.SetActive(false);
                }
                // 点击柜子2急救箱 ：获取道具
                if (cabinet2FirstAndKit.activeSelf && cabinet2FirstAndKit.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    cabinet2FirstAndKit.SetActive(false);
                }
                // 点击柜子3 ：显示内容
                if (cabinet3.activeSelf && cabinet3.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    cabinet3Content.SetActive(true);
                    cabinet3Pictures.SetActive(true);
                }
                // 点击柜子3内容 ：关闭柜子3
                if (cabinet3Content.activeSelf && cabinet3Content.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    cabinet3Content.SetActive(false);
                    cabinet3Pictures.SetActive(false);
                }
                // 点击柜子3照片 ： 显示特写照片
                if (cabinet3Pictures.activeSelf && cabinet3Pictures.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    Debug.Log("柜子3照片");
                    // darkness.SetActive(true);
                    // picture0.SetActive(true);
                    // picture1.SetActive(true);
                }
                // 点击柜子4 ：显示内容
                if (cabinet4.activeSelf && cabinet3.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    cabinet3Content.SetActive(true);
                    cabinet3Pictures.SetActive(true);
                }
                // 点击柜子4内容 ：关闭柜子4
                // 点击柜子4箱子 : 显示特写加密箱
                // 点击柜子5 ：显示内容
                // 点击柜子5内容 ：关闭柜子5
                // 点击柜子5钥匙 ： 获取道具
                // 点击柜子6 ：显示内容
                // 点击柜子6内容 ：关闭柜子6
                // 点击柜子6说明书 ：显示特写说明书
                // 点击柜子7 ：显示内容
                // 点击柜子7内容 ：关闭柜子7
                // 点击柜子7螺丝刀 ：获取道具
                // 滑动切换照片
                // 点击空白处退出照片特写
            }
        }
    }
    void DisableSomeGameObject()
    {
        cabinet1Content.SetActive(false);
        cabinet2Content.SetActive(false);
        cabinet3Content.SetActive(false);
        cabinet4Content.SetActive(false);
        cabinet5Content.SetActive(false);
        cabinet6Content.SetActive(false);
        cabinet7Content.SetActive(false);
        cabinet1Box.SetActive(false);
        cabinet2FirstAndKit.SetActive(false);
        cabinet3Pictures.SetActive(false);
        cabinet4Box.SetActive(false);
        cabinet5Keys.SetActive(false);
        cabinet6Instructions.SetActive(false);
        cabinet7Screwdriver.SetActive(false);
        picture0.SetActive(false);
        picture1.SetActive(false);
        picture2.SetActive(false);
        picture3.SetActive(false);
        instructions0.SetActive(false);
        instructions1.SetActive(false); 
        instructions2.SetActive(false);
        instructions3.SetActive(false);
        instructions4.SetActive(false);
        instructions5.SetActive(false);
        box0.SetActive(false);
        box1.SetActive(false);
        box2.SetActive(false);
        box3.SetActive(false);
        television.SetActive(false);
    }
}
