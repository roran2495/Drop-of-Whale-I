using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class DeskFeatureController : MonoBehaviour
{
    public GameObject wordCanvas;
    public TMP_Text word;
    public TMP_Text owner;
    private GameObject materials11;
    private GameObject materials12;
    private GameObject materials2;
    private GameObject cup1;
    private GameObject glassRod1;
    private GameObject materials4;
    private GameObject materials5;
    private GameObject materials6;
    private GameObject egg;
    private GameObject pigments;
    private GameObject cup2;
    private GameObject cupPigments;
    private GameObject cupColer;
    private GameObject cupLiquid;
    private GameObject cupEggs;
    private GameObject cupResin;
    private GameObject glassRod2;
    private GameObject eggFeature;
    private GameObject eggEgg1;
    private GameObject eggEgg2;
    private GameObject eggEgg3;
    private GameObject eggEgg6;
    private GameObject eggEgg7;
    private GameObject pigmentsFeature;
    private TMP_InputField rgb1;
    private TMP_InputField rgb2;
    private TMP_InputField rgb3;
    private bool flagMoreEgg; // 1 :false ; more : true
    private int flagEggManage;  // 0:处理完 1：鸡蛋壳处理了 2：蛋胚处理了 3 都处理了
    private bool flagM2;    // have already add true
    private bool flagM4;
    private bool flagM5;
    private bool flagM6;
    private bool flagM11;
    private bool flagM12;
    private bool flagP;
    private bool flagW;
    private bool flagG; // 搅拌完成 true
    private Color[] colorsPigments = {new Color(157f/255f, 41f/255f, 51f/255f), new Color(231/255f, 213/255f, 160/255f), new Color(5f/255f, 119f/255f, 72f/255f), new Color(220f/255f, 48f/255f, 35f/255f), new Color(186f/255f, 44f/255f, 192f/255f) };
    private RectTransform inputFieldRect1;
    private Vector3 initialPosition1;
    private RectTransform inputFieldRect3;
    private Vector3 initialPosition3;
    // Start is called before the first frame update
    void Start()
    {
        materials11 = transform.Find("materials1").Find("materials11").gameObject;
        materials12 = transform.Find("materials1").Find("materials12").gameObject;
        materials2 = transform.Find("materials2").gameObject;
        cup1 = transform.Find("materials3").Find("cup1").gameObject;
        glassRod1 = transform.Find("materials3").Find("glass rod1").gameObject;
        materials4 = transform.Find("materials4").gameObject;
        materials5 = transform.Find("materials5").gameObject;
        materials6 = transform.Find("materials6").gameObject;
        egg = transform.Find("egg").gameObject;
        pigments = transform.Find("pigments").gameObject;
        cup2 = transform.Find("cup2").gameObject;
        cupLiquid = cup2.transform.Find("liquid").gameObject;
        cupEggs = cup2.transform.Find("eggs").gameObject;
        cupResin = cup2.transform.Find("resin").gameObject;
        cupPigments = cup2.transform.Find("pigments").gameObject;
        cupColer = cup2.transform.Find("color").gameObject;
        glassRod2 = cup2.transform.Find("glass rod2").gameObject;
        eggFeature = egg.transform.Find("egg feature").gameObject;
        eggEgg1 = eggFeature.transform.Find("egg1").gameObject;
        eggEgg2 = eggFeature.transform.Find("egg2").gameObject;
        eggEgg3 = eggFeature.transform.Find("egg3").gameObject;
        eggEgg6 = eggEgg3.transform.Find("egg6").gameObject;
        eggEgg7 = eggFeature.transform.Find("egg7").gameObject;
        pigmentsFeature = pigments.transform.Find("pigments feature").gameObject;
        rgb1 = pigmentsFeature.transform.Find("Canvas").Find("rgb1").GetComponent<TMP_InputField>();
        inputFieldRect1 = pigmentsFeature.transform.Find("Canvas").Find("rgb1").GetComponent<RectTransform>();
        initialPosition1 = inputFieldRect1.localPosition;
        rgb2 = pigmentsFeature.transform.Find("Canvas").Find("rgb2").GetComponent<TMP_InputField>();
        rgb3 = pigmentsFeature.transform.Find("Canvas").Find("rgb3").GetComponent<TMP_InputField>();
        inputFieldRect3 = pigmentsFeature.transform.Find("Canvas").Find("rgb3").GetComponent<RectTransform>();
        initialPosition3 = inputFieldRect3.localPosition;

        materials4.SetActive(false);
        materials5.SetActive(false);
        materials6.SetActive(false);
        egg.SetActive(false);
        cup2.SetActive(false);
        eggFeature.SetActive(false);
        eggEgg1.SetActive(false);
        eggEgg2.SetActive(false);
        eggEgg3.SetActive(false);
        eggEgg6.SetActive(false);
        eggEgg7.SetActive(false);
        pigmentsFeature.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 强制保持初始位置
        inputFieldRect1.localPosition = initialPosition1;
        inputFieldRect3.localPosition = initialPosition3;
        // 检测触摸结束或鼠标弹起
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
        {
            // 获取触摸位置或鼠标点击位置
            Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
            Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
            // 将触摸位置的 Z 值设置为场景的 Z 值
            worldTouchPosition.z = transform.position.z;

            if (wordCanvas.activeSelf)
            {
                wordCanvas.SetActive(false);
            }
            else
            {

            }
        }
    }
    public void SetColor(Color color)
    {
        cupColer.transform.Find("color1").GetComponent<SpriteRenderer>().color = color;
        cupColer.transform.Find("color2").GetComponent<SpriteRenderer>().color = color;
        cupColer.transform.Find("color3").GetComponent<SpriteRenderer>().color = color;
        cupColer.transform.Find("color4").GetComponent<SpriteRenderer>().color = color;
        cupColer.transform.Find("color5").GetComponent<SpriteRenderer>().color = color;
        cupColer.transform.Find("color6").GetComponent<SpriteRenderer>().color = color;
    }
    void SetColorFalse()
    {
        foreach(Transform child in cupColer.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
    void DisableSonColliders(Transform parent)
    {
        PolygonCollider2D polygonCollider2D = parent.GetComponent<PolygonCollider2D>(); 
        if (polygonCollider2D != null)
        {
            polygonCollider2D.enabled = false;
        }
        foreach (Transform child in parent)
        {
            DisableSonColliders(child);
        }
    }
    void EnableSonColliders(Transform parent)
    {
        PolygonCollider2D polygonCollider2D = parent.GetComponent<PolygonCollider2D>(); 
        if (polygonCollider2D != null)
        {
            polygonCollider2D.enabled = true;
        }
        foreach (Transform child in parent)
        {
            EnableSonColliders(child);
        }
    }
    public void HandleRGBChoose()
    {
        Item item = GlobalManager.FindItem("24");
        if (item != null)
        {
            Debug.Log("一次只能取一个色料");
        }
        else
        {
            if (rgb1.text == "")
            {
                rgb1.text = "0";
            }
            if (rgb2.text == "")
            {
                rgb2.text = "0";
            }
            if (rgb3.text == "")
            {
                rgb3.text = "0";
            }
            GlobalManager.AddItemColor("24", new Color(float.Parse(rgb1.text)/255f, float.Parse(rgb2.text)/255f, float.Parse(rgb3.text)/255f));
        }
    }
    public void HandleRGBChange()
    {
        if (rgb1.text == "")
        {
            rgb1.text = "0";
        }
        if (rgb2.text == "")
        {
            rgb2.text = "0";
        }
        if (rgb3.text == "")
        {
            rgb3.text = "0";
        }
        pigmentsFeature.transform.Find("color").GetComponent<SpriteRenderer>().color = new Color(float.Parse(rgb1.text)/255f, float.Parse(rgb2.text)/255f, float.Parse(rgb3.text)/255f); 
    }
    public void FinishUseGladdRod()
    {
        flagG = true;
    }
}
