using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    private TMP_InputField inputFieldEmailAccount;
    private TMP_InputField inputFieldEmailPassword;
    private bool flagTxt; // locked:true
    private bool flagEmail; // loced:true
    // Start is called before the first frame update
    void Start()
    {
        desktopIconTxt = transform.Find("icon1").gameObject;
        desktopIconEmail = transform.Find("icon2").gameObject;
        txt = transform.Find("txt").gameObject;
        txtIcon = txt.transform.Find("icon").gameObject;
        txtInput = txt.transform.Find("input").gameObject;
        txtInputClose = txtInput.transform.Find("close").gameObject;
        txtInputInput = txtInput.transform.Find("Canvas").Find("input").gameObject;
        txtInputSubmit = txtInput.transform.Find("submit").gameObject;
        txtContent = txt.transform.Find("content").gameObject;
        txtContentClose = txtContent.transform.Find("close").gameObject;
        email = transform.Find("email").gameObject;
        emailIcon = email.transform.Find("icon").gameObject;
        emailInput = email.transform.Find("input").gameObject;
        emailInputIcon = emailInput.transform.Find("icon").gameObject;
        emailInputClose = emailInput.transform.Find("close").gameObject;
        emailInputAccount = emailInput.transform.Find("Canvas").Find("account").gameObject;
        emailInputPassword = emailInput.transform.Find("Canvas").Find("password").gameObject;
        emailInputSubmit = emailInput.transform.Find("submit").gameObject;
        emailContent = email.transform.Find("content").gameObject;
        emailContentClose = emailContent.transform.Find("close").gameObject;

        inputFieldTxt = txtInputInput.GetComponent<TMP_InputField>();
        inputFieldEmailAccount = emailInputAccount.GetComponent<TMP_InputField>();
        inputFieldEmailPassword = emailInputPassword.GetComponent<TMP_InputField>();

        txt.SetActive(false);
        txtContent.SetActive(false);
        txtInput.SetActive(false);
        txtIcon.SetActive(false);
        email.SetActive(false);
        emailContent.SetActive(false);
        emailInput.SetActive(false);
        emailIcon.SetActive(false);
        flagTxt = false;
        flagEmail = false;
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
            else
            {
                desktopIconTxt.GetComponent<PolygonCollider2D>().enabled = true;
                desktopIconEmail.GetComponent<PolygonCollider2D>().enabled = true;
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
            // 点击底部email切换隐藏显示email
            if (emailIcon.activeSelf && emailIcon.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                bool flagTmp = !(emailInput.activeSelf || emailContent.activeSelf);
                emailInput.SetActive(flagTmp && !flagEmail);
                emailContent.SetActive(flagTmp && flagEmail);
            }

            // 点击txt打开txt
            if (desktopIconTxt.activeSelf && desktopIconTxt.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                Debug.Log("1");
                txt.SetActive(true);
                txtInput.SetActive(!flagTxt);
                txtContent.SetActive(flagTxt);
                // 处理Icon
                txtIcon.SetActive(true);
                if (emailIcon.activeSelf)
                {
                    txtIcon.transform.localPosition = new Vector3(-5.583002f, txtIcon.transform.localPosition.y, txtIcon.transform.localPosition.z);
                }
                else
                {
                    txtIcon.transform.localPosition = new Vector3(-6.18f, txtIcon.transform.localPosition.y, txtIcon.transform.localPosition.z);
                }
            }
            // 关闭txt
            if (txtInputClose.activeSelf && txtInputClose.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                txtInput.SetActive(false);
                txtIcon.SetActive(false);
            }
            if (txtContentClose.activeSelf && txtContentClose.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                txtContent.SetActive(false);
                txtIcon.SetActive(false);
            }
            // 处理Icon
            if (!txtIcon.activeSelf && emailIcon.activeSelf)
            {
                emailIcon.transform.localPosition = new Vector3(-6.18f, emailIcon.transform.localPosition.y, emailIcon.transform.localPosition.z);
            }
            // txt密码提交
            if (txtInputSubmit.activeSelf && txtInputSubmit.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                if (inputFieldTxt.text == "MX07" || inputFieldTxt.text == "mx07")
                {
                    flagTxt = true;
                    txtInput.SetActive(false);
                    txtContent.SetActive(true);
                }
                else
                {
                    inputFieldTxt.text = "";
                    Debug.Log("没有反应，看来是密码错误了，我记得这种加密文件似乎有次数限制，不知道次数到了会怎么样");
                }
            }

            // 点击email打开email
            if (desktopIconEmail.activeSelf && desktopIconEmail.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                email.SetActive(true);
                emailInput.SetActive(!flagEmail);
                emailContent.SetActive(flagEmail);
                // 处理Icon
                emailIcon.SetActive(true);
                if (txtIcon.activeSelf)
                {
                    Debug.Log("222222222222");
                    emailIcon.transform.localPosition = new Vector3(-5.583002f, emailIcon.transform.localPosition.y, emailIcon.transform.localPosition.z);
                }
                else
                {
                    Debug.Log("111111111111");
                    emailIcon.transform.localPosition = new Vector3(-6.18f, emailIcon.transform.localPosition.y, emailIcon.transform.localPosition.z);
                }
            }
            // 关闭email
            if (emailInputClose.activeSelf && emailInputClose.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                emailInput.SetActive(false);
                emailIcon.SetActive(false);
            }
            if (emailContentClose.activeSelf && emailContentClose.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                emailContent.SetActive(false);
                emailIcon.SetActive(false);
            }
            // 处理Icon
            if (txtIcon.activeSelf && !emailIcon.activeSelf)
            {
                txtIcon.transform.localPosition = new Vector3(-6.18f, txtIcon.transform.localPosition.y, txtIcon.transform.localPosition.z);
            }
            // email登录
            if (emailInputSubmit.activeSelf && emailInputSubmit.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                if (inputFieldEmailAccount.text == "STP93514@my" && inputFieldEmailPassword.text == "Nn0514")
                {
                    flagEmail = true;
                    StartCoroutine(EmailLoginning());
                }
                else
                {
                    inputFieldEmailAccount.text = "";
                    inputFieldEmailPassword.text = "";
                    Debug.Log("没有反应，看来是账号或者错误了，再仔细检查一下怎么样");
                }
            }
        }
    }
    void ChangeFront(bool flag) // false:txt;true:email
    {
        if (!flag)  // txt显示优先级变高，email显示优先级变低
        {
            txtInput.transform.GetComponent<SpriteRenderer>().sortingOrder = 6;
            txtContent.transform.GetComponent<SpriteRenderer>().sortingOrder = 6;
            emailInput.transform.
        }
        else
        {

        }
    }
    IEnumerator EmailLoginning()
    {
        emailInputIcon.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("angle7/angle7-10-(3)");
        Debug.Log("登录成功了！");
        yield return new WaitForSeconds(0.5f);

        emailInput.SetActive(false);
        emailContent.SetActive(true);
    }

    public void HandleButton1Click()
    {
        emailContentContent.GetComponent<Image>().sprite = Resources.Load<Sprite>("angle7/angle7-10-(6)-(1)");
    }
    public void HandleButton2Click()
    {
        emailContentContent.GetComponent<Image>().sprite = Resources.Load<Sprite>("angle7/angle7-10-(6)-(2)");
    }
    public void HandleButton3Click()
    {
        emailContentContent.GetComponent<Image>().sprite = Resources.Load<Sprite>("angle7/angle7-10-(6)-(3)");
    }
    public void HandleButton4Click()
    {
        emailContentContent.GetComponent<Image>().sprite = Resources.Load<Sprite>("angle7/angle7-10-(6)-(4)");
    }
    public void HandleInputEndAccount(string txt)
    {
        Debug.Log(txt);
        string txtTmp = inputFieldEmailAccount.text + "@myemail.com";
        inputFieldEmailAccount.text = txtTmp.Substring(0,11);
    }
}
