using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class DeskFeatureController : MonoBehaviour
{
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
    private bool flagM2;    // have already add true
    private bool flagM4;
    private bool flagM5;
    private bool flagM6;
    private bool flagM11;
    private bool flagM12;
    private bool flagP;
    private Color[] colorsPigments = {new Color(0, 0, 0),new Color(0, 0, 0),new Color(0, 0, 0),new Color(0, 0, 0),new Color(0, 0, 0) };
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
        rgb2 = pigmentsFeature.transform.Find("Canvas").Find("rgb2").GetComponent<TMP_InputField>();
        rgb3 = pigmentsFeature.transform.Find("Canvas").Find("rgb3").GetComponent<TMP_InputField>();

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
        if (glassRod2.activeSelf)
        {

        }
        else
        {
            
        }
        
    }
    void SetColor(Color color)
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
    void DisableOtherColliders(GameObject currentGameObject)
    {
        // 获取场景中所有的Collider组件
        Collider2D[] colliders = FindObjectsOfType<Collider2D>();
        
        // 遍历所有Collider组件，禁用除了特写对象以外的所有其他对象的碰撞器
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != currentGameObject)
            {
                collider.enabled = false;
            }
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
            GlobalManager.AddItemColor("24", new Color(float.Parse(rgb1.text), float.Parse(rgb2.text), float.Parse(rgb3.text)));
        }
    }
    public void HandleRGBChange()
    {
        pigmentsFeature.transform.Find("color").GetComponent<SpriteRenderer>().color = new Color(float.Parse(rgb1.text), float.Parse(rgb2.text), float.Parse(rgb3.text)); 
    }
}
