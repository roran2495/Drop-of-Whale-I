using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ComputerDesktopController : MonoBehaviour
{
    public GameObject wordCanvas;
    public TMP_Text word;
    public TMP_Text owner;
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

            if (wordCanvas.activeSelf)
            {
                wordCanvas.SetActive(false);
            }
            else
            {

            }
        }
    }
    void ChangeFront(bool flag) // false:txt;true:email
    {
        if (!flag)  // txt显示优先级变高，email显示优先级变低
        {
            txtInput.transform.GetComponent<SpriteRenderer>().sortingOrder = 7;
            txtInput.transform.Find("Canvas").GetComponent<Canvas>().sortingOrder = 8;
            txtContent.transform.GetComponent<SpriteRenderer>().sortingOrder = 7;
            txtContent.transform.Find("Canvas").GetComponent<Canvas>().sortingOrder = 8;
            emailInput.transform.GetComponent<SpriteRenderer>().sortingOrder = 5;
            emailInput.transform.Find("icon").GetComponent<SpriteRenderer>().sortingOrder = 6;
            emailInput.transform.Find("Canvas").GetComponent<Canvas>().sortingOrder = 6;
            emailContent.transform.GetComponent<SpriteRenderer>().sortingOrder = 5;
            emailContent.transform.Find("Canvas").GetComponent<Canvas>().sortingOrder = 6;
        }
        else
        {
            txtInput.transform.GetComponent<SpriteRenderer>().sortingOrder = 5;
            txtInput.transform.Find("Canvas").GetComponent<Canvas>().sortingOrder = 6;
            txtContent.transform.GetComponent<SpriteRenderer>().sortingOrder = 5;
            txtContent.transform.Find("Canvas").GetComponent<Canvas>().sortingOrder = 6;
            emailInput.transform.GetComponent<SpriteRenderer>().sortingOrder = 7;
            emailInput.transform.Find("icon").GetComponent<SpriteRenderer>().sortingOrder = 8;
            emailInput.transform.Find("Canvas").GetComponent<Canvas>().sortingOrder = 8;
            emailContent.transform.GetComponent<SpriteRenderer>().sortingOrder = 7;
            emailContent.transform.Find("Canvas").GetComponent<Canvas>().sortingOrder = 8;
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
    void SetWord(int n,string txt)
    {
        wordCanvas.SetActive(true);
        word.text = txt;
        owner.text = "J:";
    }
}
