using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    private TMP_InputField inputFieldTxt;
    private bool flagTxt; // locked:true
    private bool flagEmail; // loced:true
    private int flagfront; // 标记当前显示最前面的页面是txt(1)还是email(2),还是没有(0)
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
        txtInputSubmit = txtInput.transform.Find("submit").gameObject;
        txtContent = txt.transform.Find("content").gameObject;
        txtContentClose = txtContent.transform.Find("close").gameObject;
        email = transform.Find("email").gameObject;

        inputFieldTxt = txtInputInput.GetComponent<TMP_InputField>();
        inputFieldTxt.onEndEdit.AddListener(HandleInputEndTxt);

        txt.SetActive(false);
        email.SetActive(false);
        flagTxt = false;
        flagEmail = false;
        flagfront = 0;
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

            // 维护，当icon被遮挡时collider不启用
            if (txtContent.activeSelf)
            {
                desktopIconTxt.GetComponent<PolygonCollider2D>().enabled = false;
                desktopIconEmail.GetComponent<PolygonCollider2D>().enabled = false;
            }

            // 点击底部txt切换隐藏显示txt
            if (txtIcon.activeSelf && txtIcon.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                // 若当前显示输入密码说明flagTxt = false，则点击后隐藏，再次点击显示输入密码页
                // 若当前显示txt内容说明flagTxt = true，则点击后隐藏，再次点击显示txt内容
                bool flagTmp = !(txtInput.activeSelf || txtContent.activeSelf);
                txtInput.SetActive(flagTmp && !flagTxt);
                txtContent.SetActive(flagTmp && flagTxt);
            }

            // 点击txt打开txt
            if (desktopIconTxt.activeSelf && desktopIconTxt.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                txt.SetActive(true);
                txtInput.SetActive(!flagTxt);
                txtContent.SetActive(flagTxt);
            }
            

        }
    }
    HandleInputEndTxt
}
