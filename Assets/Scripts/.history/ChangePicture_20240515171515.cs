using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePicture : MonoBehaviour
{
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject backArrow;
    public GameObject darkness;
    private float moveDistanceX = 30f;
    // Start is called before the first frame update
    void Start()
    {
        leftArrow.SetActive(true);
        rightArrow.SetActive(true);
        backArrow.SetActive(false);
        darkness.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 限制在视角1-3的时候使用
        if (transform.position.y == 0) 
        {
            // 检测触摸输入或鼠标点击
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
            {
                // 获取触摸位置或鼠标点击位置
                Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                // 将触摸位置的 Z 值设置为箭头的 Z 值
                worldTouchPosition.z = leftArrow.transform.position.z;

                // 检测点击的是左箭头还是右箭头
                if (leftArrow && leftArrow.activeSelf && leftArrow.GetComponent<Collider2D>().bounds.Contains(worldTouchPosition))
                {
                    MoveCameraToLeft();
                    Debug.Log(leftArrow.GetComponent<Collider2D>().bounds.Contains())
                    Debug.Log(leftArrow.GetComponent<CircleCollider2D>().bounds);
                }
                else if (rightArrow && rightArrow.activeSelf && rightArrow.GetComponent<Collider2D>().bounds.Contains(worldTouchPosition))
                {
                    MoveCameraToRight();
                }
            }
        }
    }

    // 将摄像机移动到左边
    void MoveCameraToLeft()
    {
        Vector3 currentPosition = transform.position;
        float newX = currentPosition.x - moveDistanceX;
        if (newX < -30) newX += 90;
        transform.position = new Vector3(newX, currentPosition.y, currentPosition.z);
    }

    // 将摄像机移动到右边
    void MoveCameraToRight()
    {
        Vector3 currentPosition = transform.position;
        float newX = currentPosition.x + moveDistanceX;
        if (newX > 30) newX -= 90;
        transform.position = new Vector3(newX, currentPosition.y, currentPosition.z);
    }

}
