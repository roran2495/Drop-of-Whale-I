using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle7Controller : MonoBehaviour
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
        // 保证是触发视图7跳转
        if (mainCamera.transform.position.x == 61.3 && mainCamera.transform.position.y == 15)
        {
            // 检测触摸输入或鼠标点击
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
        {
            // 获取触摸位置或鼠标点击位置
            Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
            Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
            // 将触摸位置的 Z 值设置为场景的 Z 值
            worldTouchPosition.z = backArrow.transform.position.z;

            // 从视角7到视角2
            if (backArrow && backArrow.activeSelf && backArrow.GetComponent<Collider2D>().bounds.Contains(worldTouchPosition))
            {
                Debug.Log("7->2");
                mainCamera.transform.position = new Vector3(-28.7f, 0, -10);
                leftArrow.SetActive(!leftArrow.activeSelf);
                backArrow.SetActive(!backArrow.activeSelf);
            }
            // 从视角7到视角6
            if (rightArrow && rightArrow.activeSelf && rightArrow.GetComponent<Collider2D>().bounds.Contains(worldTouchPosition))
            {
                Debug.Log("7->6");
                mainCamera.transform.position = new Vector3(31.3f, 15, -10);
                leftArrow.SetActive(!leftArrow.activeSelf);
                rightArrow.SetActive(!rightArrow.activeSelf);
            }
        }
        }
    }
}
