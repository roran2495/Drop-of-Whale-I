using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle3Controller : MonoBehaviour
{
    public GameObject doorToAngle4;
    CircleCollider2D circleCollider;
    public GameObject doorToAngle8;
    public GameObject mainCamera;
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject backArrow;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 保证是触发视图3跳转
        if (mainCamera.transform.position.x == 30 && mainCamera.transform.position.y == 0)
        {
            // 检测触摸输入或鼠标点击
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
            {
                // 获取触摸位置或鼠标点击位置
                Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                // 将触摸位置的 Z 值设置为场景的 Z 值
                worldTouchPosition.z = doorToAngle4.transform.position.z;

                // 从视角3到视角4
                if (doorToAngle4 && doorToAngle4.activeSelf && doorToAngle4.GetComponent<BoxCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    mainCamera.transform.position = new Vector3(-30, 15, -10);
                    leftArrow.SetActive(!leftArrow.activeSelf);
                    rightArrow.SetActive(!rightArrow.activeSelf);
                    backArrow.SetActive(!backArrow.activeSelf);
                }
                // 从视角3到视角8
                if (doorToAngle8 && doorToAngle8.activeSelf && doorToAngle8.GetComponent<Collider2D>().bounds.Contains(worldTouchPosition))
                {
                    mainCamera.transform.position = new Vector3(60 , 0, -10);
                    leftArrow.SetActive(!leftArrow.activeSelf);
                    rightArrow.SetActive(!rightArrow.activeSelf);
                    backArrow.SetActive(!backArrow.activeSelf);
                }
            }
        }
    }
}
