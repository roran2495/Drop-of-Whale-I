using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle2Controller : MonoBehaviour
{
    public GameObject doorToAngle7;
    public GameObject leftArrow;
    public GameObject backArrow;
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
    // Start is called before the first frame update
    void Start()
    {
        cabinet1.SetActive(false);
        cabinet2.SetActive(false);
        cabinet3.SetActive(false);
        cabinet4.SetActive(false);
        cabinet5.SetActive(false);
        cabinet6.SetActive(false);
        cabinet7.SetActive(false);
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
            }

            // 点击柜子1 ：显示内容
            // 点击柜子1内容 ：关闭柜子1
            // 点击柜子1箱子 ： 文案交互
            // 点击柜子2 ：显示内容
            // 点击柜子2内容 ：关闭柜子2
            // 点击柜子2急救箱 ：获取道具
            // 点击柜子3 ：显示内容
            // 点击柜子3内容 ：关闭柜子3
            // 点击柜子3照片 ： 显示特写照片
            // 点击柜子4 ：显示内容
            // 点击柜子4内容 ：关闭柜子4
            // 点击柜子4箱子 : 显示特写加密箱
            // 点击柜子5 ：显示内容
            // 点击柜子5内容 ：关闭柜子5
            // 点击柜子5钥匙 ： 获取道具
            // 点击柜子6 ：显示内容
            // 点击柜子6内容 ：关闭柜子6
            // 点击柜子6说明书 ：
            // 点击柜子7 ：显示内容
            // 点击柜子7内容 ：关闭柜子7
        }
    }
}
