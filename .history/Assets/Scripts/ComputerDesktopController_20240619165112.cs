using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ComputerDesktopController : MonoBehaviour
{
    private GameObject desktopIconTxt;
    private GameObject desktopIconEmail;
    private GameObject txt;
    private GameObject txtIcon;
    private GameObject txtInput;
    private GameObject txtInputClose;
    private GameObject txtInputInput;
    private GameObject txtInputSubmit;
    private GameObject txtContent;
    private GameObject txtContentClose;
    private GameObject email;
    // Start is called before the first frame update
    void Start()
    {
        desktopIconTxt = transform.Find("icon1").gameObject;
        desktopIconEmail = transform.Find("icon2").gameObject;
        txt = transform.Find("txt").gameObject;
        txtIcon = txt.transform.Find("icon").gameObject;
        txtInput = txt.transform.Find("input").gameObject;
        txtInputClose = txtInput.transform.Find("close").gameObject;
        txtInputInput = txtInput.transform.Find("input").gameObject;
        txtInputSubmit = txt.transform.Find("input").transform.Find("submit").gameObject;
        txtContentClose = txt.transform.Find("content").transform.Find("close").gameObject;
        email = transform.Find("email").gameObject;
        txt.SetActive(false);
        email.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 检测触摸结束或鼠标弹起
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
        {
            // 获取触摸位置或鼠标点击位置
            Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
            Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
            // 将触摸位置的 Z 值设置为场景的 Z 值
            worldTouchPosition.z = transform.position.z;

            if (desktopIconEmail.activeSelf && desktopIconEmail.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                
            }
        }
    }
}
