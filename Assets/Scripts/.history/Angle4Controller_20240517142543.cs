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
    }

    // Update is called once per frame
    void Update()
    {
        // 保证是触发视图4跳转
        if (mainCamera.transform.position.x == -30 && mainCamera.transform.position.y == 15)
        {
            // 检测触摸输入或鼠标点击
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
            {
                // 获取触摸位置或鼠标点击位置
                Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                // 将触摸位置的 Z 值设置为场景的 Z 值
                worldTouchPosition.z = backArrow.transform.position.z;

                // 从视角4到视角3
                if (backArrow && backArrow.activeSelf && backArrow.GetComponent<Collider2D>().bounds.Contains(worldTouchPosition))
                {
                    Debug.Log("4->3");
                    mainCamera.transform.position = new Vector3(30, 0, -10);
                    leftArrow.SetActive(!leftArrow.activeSelf);
                    rightArrow.SetActive(!rightArrow.activeSelf);
                    backArrow.SetActive(!backArrow.activeSelf);
                }
            }
        }
    }
}
