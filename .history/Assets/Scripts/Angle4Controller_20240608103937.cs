using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle4Controller : MonoBehaviour
{
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject backArrow;
    public GameObject curtain;
    public GameObject curtainOpen;
    public GameObject water;
    public GameObject washer;
    public GameObject washerClothes;
    public GameObject washerOpen;
    public GameObject washerKey;
    public GameObject toothCup1;
    public GameObject toothCup2;
    public GameObject place;
    public GameObject toothCup3;
    public GameObject tap;
    public GameObject mark;
    public GameObject markShow;
    private bool flagMark;  // true: show
    private bool flagClothes;   // true : have put it
    private int flagWater;  // 0 : no water ; 1: cold water ; 2: hot water
    private Vector3 touchStartPosition; // 存储鼠标按下或开始触摸时的位置
    private Vector3 touchEndPosition; // 存储鼠标弹起或结束触摸时的位置
    // Start is called before the first frame update
    void Start()
    {
        curtainOpen.SetActive(false);
        water.SetActive(false);
        washerClothes.SetActive(false);
        washerOpen.SetActive(false);
        washerKey.SetActive(false);
        toothCup2.SetActive(false);
        toothCup3.SetActive(false);
        markShow.SetActive(false);
        flagClothes = false;
        flagWater = 0;
        flagMark = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 保证是触发视图4跳转
        if (Camera.main.transform.position.x == -30 && Camera.main.transform.position.y == 15)
        {
            // 检测触摸开始或鼠标按下
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
            {
                // 获取触摸位置或鼠标点击位置
                Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                touchStartPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                // 将触摸位置的 Z 值设置为场景的 Z 值
                touchStartPosition.z = curtain.transform.position.z;
            }
            // 检测触摸结束或鼠标弹起
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
            {
                // 获取触摸位置或鼠标点击位置
                Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                touchEndPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                // 将触摸位置的 Z 值设置为场景的 Z 值
                touchEndPosition.z = curtain.transform.position.z;
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
    void HandleClick(Vector3 worldTouchPosition)
    {
        // 从视角4到视角3
        if (backArrow && backArrow.activeSelf && backArrow.GetComponent<Collider2D>().bounds.Contains(worldTouchPosition))
        {
             Debug.Log("4->3");
            Camera.main.transform.position = new Vector3(30, 0, -10);
            leftArrow.SetActive(!leftArrow.activeSelf);
            rightArrow.SetActive(!rightArrow.activeSelf);
            backArrow.SetActive(!backArrow.activeSelf);
        }
        if (!Camera.main.transform.Find("darkness").gameObject.activeSelf)
        {
            // 显示标记
            if (!flagMark && mark.activeSelf && mark.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                markShow.SetActive(true);
                flagMark = true;
            } 
            // 关闭门帘 可以看到标记
            else if (curtainOpen.activeSelf && curtainOpen.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                curtainOpen.SetActive(false);
                markShow.SetActive(flagMark);
            } 
            // 打开门帘
            else if (curtain.activeSelf && curtain.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                curtainOpen.SetActive(true);
                mark.SetActive(true);
                markShow.SetActive(false);
            }
            
            // 获取牙杯
            if (toothCup1.activeSelf && toothCup1.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                GlobalManager.AddItem("07", Resources.Load<Sprite>("others/cup without water"));
                toothCup1.SetActive(false);
                toothCup2.SetActive(true);
            } 
            // 拿起牙杯
            if (toothCup3.activeSelf && toothCup3.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                if (flagWater == 0)
                {
                    GlobalManager.AddItem("07", Resources.Load<Sprite>("others/cup without water"));
                }
                else
                {
                    GlobalManager.AddItem("07", Resources.Load<Sprite>("others/cup with water"));
                }
                toothCup3.SetActive(false);
            }
            // 放置牙杯
            else if (place.activeSelf && place.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                Item item = GlobalManager.FindItem("07");
                if (item != null && item.isSelected)
                {
                    toothCup3.SetActive(true);
                    toothCup3.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(flagWater == 0 ? "angle4/angle4-5-(3)":"angle4/angle4-5-(4)");
                    GlobalManager.RemoveItem(item);
                }
                else
                {
                    if (GlobalManager.someItemIsSelected)
                    {
                        Debug.Log("似乎不是用这个接水的");
                    }
                }
            }   

            if (washer.activeSelf && washer.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                // 放入衣服，出现钥匙
                if (washerOpen.activeSelf)
                {
                    if(!flagClothes)
                    {
                        washerClothes.SetActive(true);
                        flagClothes = true;
                        washerKey.SetActive(true);
                    }
                    else
                    {
                        Debug.Log("似乎没有必要把衣服取出来");
                    }
                    
                }
                // 打开洗衣机
                else
                {
                    washerOpen.SetActive(true);
                    if (flagClothes)
                    {
                        washerClothes.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("angle4/angle4-4-(2)");
                    }
                }
                
            }
            // 关闭洗衣机
            if (washerOpen.activeSelf && washerOpen.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                washerOpen.SetActive(false);
                if (flagClothes)
                {
                    washerClothes.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("angle4/angle4-4-(3)");
                }
            }
            
            // 拿起钥匙
            if (washerKey.activeSelf && washerKey.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                Debug.Log("获取钥匙");
                GlobalManager.AddItem("08", Resources.Load<Sprite>("angle4/angle4-4-(4)"));
                washerKey.SetActive(false);
            }
        } else 
        {
            Debug.Log("太黑了，你无法进行探索");
        }
    }
    void HandleDrag(Vector3 worldTouchPosition , bool direction)
    {
        if (!Camera.main.transform.Find("darkness").gameObject.activeSelf){
            if (tap.activeSelf && tap.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                if (toothCup3.activeSelf)
                {
                    if (direction) //左 -> 右
                    {
                        flagWater = 2;
                    }
                    else
                    {
                        flagWater = 1;
                    }
                    toothCup3.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("angle4/angle4-5-(4)");
                }
                water.SetActive(true);
            }
        } else {
            Debug.Log("太黑了，你无法进行探索");
        }
    }

    public int GetFlagWater()
    {
        return flagWater;
    }
}
