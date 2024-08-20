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

    // Start is called before the first frame update
    void Start()
    {
        DisableSomeGameObject();
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
    }
}
