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
    public GameObject cabinet2Content;
    public GameObject cabinet2;
    public GameObject cabinet2Content;
    public GameObject cabinet3;
    public GameObject cabinet3Content;
    public GameObject cabinet4;
    public GameObject cabinet4Content;
    public GameObject cabinet5;
    public GameObject cabinet5Content;
    public GameObject cabinet6;
    public GameObject cabinet6Content;
    public GameObject cabinet7;
    public GameObject cabinet7Content;
    // Start is called before the first frame update
    void Start()
    {
        
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
}
