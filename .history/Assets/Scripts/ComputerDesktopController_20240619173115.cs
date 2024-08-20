using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D.IK;

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
    private GameObject emailIcon;
    private GameObject emailInput;
    private GameObject emailInputClose;
    private GameObject emailInputIcon;
    private GameObject emailInputAccount;
    private GameObject emailInputPassword;
    private GameObject emailInputSubmit;
    private GameObject emailContent;
    private GameObject emailContentClose;
    public GameObject emailContentContent;

    private TMP_InputField inputFieldTxt;
    private string inputTxtText;
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
        emailIcon = email.transform.Find("icon").gameObject;
        emailInput = email.transform.Find("input").gameObject;
        emailInputIcon = emailInput.transform.Find("icon").gameObject;
        emailInputClose = emailInput.transform.Find("close").gameObject;
        emailInputAccount = emailInput.transform.Find("account").gameObject;
        emailInputPassword = emailInput.transform.Find("password").gameObject;
        emailInputSubmit = emailInput.transform.Find("submit").gameObject;
        emailContent = email.transform.Find("content").gameObject;
        emailContentClose = emailContent.transform.Find("close").gameObject;

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
            if (txtContent.activeSelf || emailInput.activeSelf || emailContent.activeSelf)
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
            // 点击底部txt切换隐藏显示txt
            if (emailIcon.activeSelf && emailIcon.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
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
            // 关闭txt
            if (txtInputClose.activeSelf && txtInputClose.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                txtInput.SetActive(false);
            }
            if (txtContentClose.activeSelf && txtContentClose.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                txtContent.SetActive(false);
            }
            // txt密码提交
            if (txtInputSubmit.activeSelf && txtInputSubmit.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                if (inputTxtText == "MX07" || inputTxtText == "mx07")
                {
                    flagTxt = true;
                    txtInput.SetActive(false);
                    txtContent.SetActive(true);
                }
                else
                {
                    txtInputInput.GetComponent<TMP_Text>().text = "";
                    Debug.Log("没有反应，看来是密码错误了，我记得这种加密文件似乎有次数限制，不知道次数到了会怎么样");
                }
            }

        }
    }
    void HandleInputEndTxt(string txt)
    {
        inputTxtText = txt;
    }
}
