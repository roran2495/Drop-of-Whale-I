using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerDesktopController : MonoBehaviour
{
    private GameObject desktopIconTxt;
    private GameObject desktopIconEmail;
    private GameObject txt;
    private GameObject email;
    // Start is called before the first frame update
    void Start()
    {
        desktopIconTxt = transform.Find("icon1").gameObject;
        desktopIconEmail = transform.Find("icon2").gameObject;
        txt = transform.Find("txt").gameObject;
        email = transform.Find("email").gameObject;
        txt.SetActive(false);
        email.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 检测触摸开始或鼠标按下
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
        {
            // 获取触摸位置或鼠标点击位置
            Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
            Vector3 Position = Camera.main.ScreenToWorldPoint(touchPosition);
            // 将触摸位置的 Z 值设置为场景的 Z 值
            touchStartPosition.z = seat1.transform.position.z;
        }
        // 检测触摸结束或鼠标弹起
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
        {
            // 获取触摸位置或鼠标点击位置
            Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
            touchEndPosition = Camera.main.ScreenToWorldPoint(touchPosition);
            // 将触摸位置的 Z 值设置为场景的 Z 值
            touchEndPosition.z = seat1.transform.position.z;
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
                // 左
                Debug.Log("向左拖动");
                HandleDrag(touchStartPosition , false);
            }
        }
    }
}
