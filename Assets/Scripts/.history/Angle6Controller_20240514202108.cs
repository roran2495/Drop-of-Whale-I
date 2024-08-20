using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle6Controller : MonoBehaviour
{
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
        // 保证是触发视图6跳转
        if (mainCamera.transform.position.x == 30 && mainCamera.transform.position.y == 15)
        {
            // 检测触摸输入或鼠标点击
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
            {
                // 获取触摸位置或鼠标点击位置
                Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                // 将触摸位置的 Z 值设置为场景的 Z 值
                worldTouchPosition.z = backArrow.transform.position.z;

                // 从视角6到视角2
                if (backArrow && backArrow.activeSelf && backArrow.GetComponent<Collider2D>().bounds.Contains(worldTouchPosition))
                {
                    Debug.Log("6->2");
                    mainCamera.transform.position = new Vector3(-30, 0, -10);
                    rightArrow.SetActive(!rightArrow.activeSelf);
                    backArrow.SetActive(!backArrow.activeSelf);
                }
                // 从视角6到视角7
                if (leftArrow && leftArrow.activeSelf && leftArrow.GetComponent<Collider2D>().bounds.Contains(worldTouchPosition))
                {
                    Debug.Log("6->7");
                    mainCamera.transform.position = new Vector3(60, 15, -10);
                    leftArrow.SetActive(!leftArrow.activeSelf);
                    rightArrow.SetActive(!rightArrow.activeSelf);
                }
            }
        }
    }
}
