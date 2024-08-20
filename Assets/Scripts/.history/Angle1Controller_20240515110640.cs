using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle1Controller : MonoBehaviour
{
    public GameObject doorToAngle5;
    public GameObject mainCamera;
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject backArrow;
        public GameObject darkness;
    public GameObject curtain;
    public bool flagCurtain;    // unuse : false   ;     use: true
    public GameObject switcher;   
    public bool flagSwitcher;   // turn on : false ;      turn off : true
    public GameObject pillow1;
    public GameObject pillow2;
    public GameObject broom1;
    public GameObject broom2;
    public GameObject carpet1;
    public GameObject carpet2;
    public GameObject letter;
    public GameObject phone;
    public GameObject shoeCabinet;
    // Start is called before the first frame update
    void Start()
    {
        curtain.SetActive(true);
        flagCurtain = false;
        flagSwitcher = false;
        pillow1.SetActive(true);
        pillow2.SetActive(false);
        broom1.SetActive(true);
        broom2.SetActive(false);
        carpet1.SetActive(true);
        carpet2.SetActive(false);
        letter.SetActive(false);
        phone.SetActive(false);
        shoeCabinet.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 保证是触发视图1跳转
        if (mainCamera.transform.position.x == 0 && mainCamera.transform.position.y == 0)
        {
            // 检测触摸输入或鼠标点击
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
            {
                // 获取触摸位置或鼠标点击位置
                Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                // 将触摸位置的 Z 值设置为场景的 Z 值
                worldTouchPosition.z = doorToAngle5.transform.position.z;

                // 从视角1到视角5
                if (doorToAngle5 && doorToAngle5.activeSelf && doorToAngle5.GetComponent<Collider2D>().bounds.Contains(worldTouchPosition))
                {
                    Debug.Log("5->1");
                    mainCamera.transform.position = new Vector3(0, 15, -10);
                    leftArrow.SetActive(!leftArrow.activeSelf);
                    rightArrow.SetActive(!rightArrow.activeSelf);
                    backArrow.SetActive(!backArrow.activeSelf);
                }
            }
        }
    }
}
