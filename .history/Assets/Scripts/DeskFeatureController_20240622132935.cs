using System.Collections;
using System.Collections.Generic;
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
    private bool flagMoreEgg; // 1 :false ; more : true
    private bool flagM4;    // have already add true
    private bool flagM5;
    private bool flagM6;
    private bool flagP;
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
        // 检测触摸结束或鼠标弹起
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
        {
            // 获取触摸位置或鼠标点击位置
            Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
            Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
            // 将触摸位置的 Z 值设置为场景的 Z 值
            worldTouchPosition.z = transform.position.z;

            // 点击烧杯重新开始
            if (cup1.activeSelf && cup1.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                cup2.SetActive(true);
                cupLiquid.SetActive(false);
                cupEggs.SetActive(false);
                cupResin.SetActive(false);
                cupPigments.SetActive(false);
                cupColer.SetActive(false);
                SetColor(new Color(180, 101, 174, 255));
                SetColorFalse();
                glassRod2.SetActive(false);
                flagMoreEgg = false;
                flagM4 = false;
                flagM5 = false;
                flagM6 = false;
                flagP = false;
                eggEgg3.SetActive(true);
                eggEgg7.SetActive(true);
            }
            // 点击各个材料加入材料 点击道具色料使用色料
            else if (cup2.activeSelf)
            {
                
            } 
            if (egg.activeSelf && egg.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                if (!cupEggs.activeSelf)
                {
                    cupEggs.SetActive(true);
                }
                else
                {
                    Debug.Log("我已经放入鸡蛋了，或许可以在蛋胚和碎蛋片挑掉以后再考虑放入鸡蛋");
                }
            }
            else if (materials4.activeSelf && materials4.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                foreach(Transform child in cupColer.transform)
                {
                    if (!child.gameObject.activeSelf)
                    {
                        child.transform.gameObject.SetActive(true);
                        break;
                    }
                }
            }
            else if (glassRod2.activeSelf && glassRod2.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {

            }
            else if (cup2.activeSelf && cup2.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                Item item = GlobalManager.FindItem("24");
                if (item != null && item.isSelected)
                {
                    GlobalManager.RemoveItem(item);
                    cupPigments.SetActive(true);
                }
                else if (GlobalManager.someItemIsSelected)
                {
                    Debug.Log("没必要加入这个吧");
                }
                else
                {
                    eggFeature.SetActive(true);
                    eggEgg1.SetActive(flagMoreEgg);
                    eggEgg2.SetActive(!flagMoreEgg);
                    DisableOtherColliders(this.gameObject);
                    EnableSonColliders(egg.transform);
                }
            }
            // 点击色料选择颜色
            // 点击玻璃棒使用玻璃棒
            else if (cup2.activeSelf && glassRod1.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {

            }
            // 热水洗涤？
            
            // 摆放材料
            else if (this.GetComponent<BoxCollider2D>().bounds.Contains(worldTouchPosition) && (!egg.activeSelf || !materials4.activeSelf || !materials5.activeSelf || !materials6.activeSelf))
            {
                bool flag = false;
                Item item = GlobalManager.FindItem("09");
                if (item != null && item.isSelected)
                {
                    GlobalManager.RemoveItem(item);
                    egg.SetActive(true);
                    flag = true;
                }
                item = GlobalManager.FindItem("10");
                if (item != null && item.isSelected)
                {
                    GlobalManager.RemoveItem(item);
                    materials4.SetActive(true);
                    flag = true;
                }
                item = GlobalManager.FindItem("11");
                if (item != null && item.isSelected)
                {
                    GlobalManager.RemoveItem(item);
                    materials5.SetActive(true);
                    flag = true;
                }
                item = GlobalManager.FindItem("12");
                if (item != null && item.isSelected)
                {
                    GlobalManager.RemoveItem(item);
                    materials6.SetActive(true);
                    flag = true;
                }

                if (flag == false && GlobalManager.someItemIsSelected)
                {
                    Debug.Log("没必要摆放这个吧");
                }
            }
            // 鸡蛋特写
            else if (!eggFeature.activeSelf)
            {
                if (eggEgg3.activeSelf && eggEgg3.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    eggEgg3.SetActive(false);
                }
                else if (eggEgg7.activeSelf && eggEgg7.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    eggEgg7.SetActive(false);
                }
                else if (egg.activeSelf && egg.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    flagMoreEgg = true;
                    eggEgg1.SetActive(true);
                    eggEgg2.SetActive(false);
                    eggEgg3.SetActive(true);
                    eggEgg7.SetActive(true);
                }
                else if (!eggFeature.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    eggFeature.SetActive(false);
                }
            }
        }
    }
    void SetColor(Color color)
    {
        cupColer.transform.Find("color1").GetComponent<SpriteRenderer>().color = color;
        cupColer.transform.Find("color2").GetComponent<SpriteRenderer>().color = color;
        cupColer.transform.Find("color3").GetComponent<SpriteRenderer>().color = color;
        cupColer.transform.Find("color4").GetComponent<SpriteRenderer>().color = color;
        cupColer.transform.Find("color5").GetComponent<SpriteRenderer>().color = color;
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
}