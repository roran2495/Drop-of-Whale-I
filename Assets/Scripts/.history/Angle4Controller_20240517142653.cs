using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle4Controller : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject backArrow;
    public GameObject curtain;
    public GameObject curtainOpen;
    public GameObject water;
    public GameObject washer;
    public GameObject washerClothes;
    public GameObject washerOpen;
    public GameObject washerKey;
    public GameObject washerToothCup1;
    public GameObject washerToothCup2;
    public GameObject place;
    public GameObject washerToothCup3;
    public GameObject tap;
    public GameObject mark;
    public GameObject markShow;
    // Start is called before the first frame update
    void Start()
    {
        curtainOpen.SetActive(false);
        water.SetActive(false);
        washerClothes.SetActive(false);
        washerOpen.SetActive(false);
        washerKey.SetActive(false);
        washerToothCup2.SetActive(false);
        washerToothCup3.SetActive(false);
        markShow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 保证是触发视图4跳转
        if (mainCamera.transform.position.x == -30 && mainCamera.transform.position.y == 15)
        {
            // 检测触摸开始或鼠标按下
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
            {
                // 获取触摸位置或鼠标点击位置
                Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                touchStartPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                // 将触摸位置的 Z 值设置为场景的 Z 值
                touchStartPosition.z = doorToAngle4.transform.position.z;
            }
            // 检测触摸结束或鼠标弹起
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
            {
                // 获取触摸位置或鼠标点击位置
                Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                touchEndPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                // 将触摸位置的 Z 值设置为场景的 Z 值
                touchEndPosition.z = doorToAngle4.transform.position.z;
                float distance = touchStartPosition.x - touchEndPosition.x;
                if (Mathf.Abs(distance) < 1.0f)
                {
                    Debug.Log("点击");
                    HandleClick(touchStartPosition);
                } 
                else if (distance < 0)
                {
                    // 右
                    Debug.Log("向右拖动");
                    HandleDrag(touchStartPosition , true);
                }
                else
                {
                    // 坐
                    Debug.Log("向左拖动");
                    HandleDrag(touchStartPosition , false);
                }
            }
        }
    }
}
