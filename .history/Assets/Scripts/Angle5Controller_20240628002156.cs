using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Angle5Controller : MonoBehaviour
{
    public GameObject wordCanvas;
    public TMP_Text word;
    public TMP_Text owner;
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject backArrow;
    public GameObject darkness;
    public GameObject darkness2;
    public GameObject refrigerator;
    public GameObject refrigeratorOpen;
    public GameObject refrigeratorEggs;
    public GameObject cuttingTool;
    public GameObject cuttingToolScissors;
    public GameObject cuttingToolKnife;
    public GameObject peppermintOil;
    public GameObject whiteVinegar;
    public GameObject alcohol;
    private Vector3 touchStartPosition; // 存储鼠标按下或开始触摸时的位置
    private Vector3 touchEndPosition; // 存储鼠标弹起或结束触摸时的位置
    // Start is called before the first frame update
    void Start()
    {
        darkness = Camera.main.transform.Find("darkness").gameObject;
        darkness2 = Camera.main.transform.Find("darkness (1)").gameObject;
        refrigeratorOpen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 保证是触发视图5跳转
        if (Camera.main.transform.position.x == 0 && Camera.main.transform.position.y == 15)
        {
            // 检测触摸开始或鼠标按下
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
            {
                // 获取触摸位置或鼠标点击位置
                Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                touchStartPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                // 将触摸位置的 Z 值设置为场景的 Z 值
                touchStartPosition.z = refrigerator.transform.position.z;
            }
            // 检测触摸结束或鼠标弹起
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
            {
                // 获取触摸位置或鼠标点击位置
                Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                touchEndPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                // 将触摸位置的 Z 值设置为场景的 Z 值
                touchEndPosition.z = refrigerator.transform.position.z;
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
        
        // 从视角5到视角1
        if (backArrow && backArrow.activeSelf && backArrow.GetComponent<Collider2D>().bounds.Contains(worldTouchPosition))
        {
            Debug.Log("5->1");
            Camera.main.transform.position = new Vector3(0, 0, -10);
            leftArrow.SetActive(!leftArrow.activeSelf);
            rightArrow.SetActive(!rightArrow.activeSelf);
            backArrow.SetActive(!backArrow.activeSelf);
        }
        if (wordCanvas.activeSelf)
        {
            wordCanvas.SetActive(false);
        }
        if (!darkness.activeSelf)
        {
            // 获取薄荷油
            if (peppermintOil.activeSelf && peppermintOil.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                Debug.Log("获取薄荷油");
                GlobalManager.AddItem("10", Resources.Load<Sprite>("angle5/angle5-4"));
                peppermintOil.SetActive(false);
            }
            // 获取白醋
            if (whiteVinegar.activeSelf && whiteVinegar.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                Debug.Log("获取白醋");
                GlobalManager.AddItem("11", Resources.Load<Sprite>("angle5/angle5-5"));
                whiteVinegar.SetActive(false);
            }
            // 获取酒精
            if (alcohol.activeSelf && alcohol.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                Debug.Log("获取酒精");
                GlobalManager.AddItem("12", Resources.Load<Sprite>("angle5/angle5-6"));
                alcohol.SetActive(false);
            }
            // 获得鸡蛋
            if (refrigeratorEggs.activeSelf && refrigeratorEggs.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                Debug.Log("获取鸡蛋");
                Item item = GlobalManager.FindItem("09");
                if (item == null)
                {
                    GlobalManager.AddItem("09", Resources.Load<Sprite>("angle8/angle8-5-(12)-(1)"));
                }
                else
                {
                    Debug.Log("你已经获取了这个神奇的无限鸡蛋");
                }
            }
            // 关闭冰箱
            else if (refrigeratorOpen.activeSelf && refrigeratorOpen.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                refrigeratorOpen.SetActive(false);
            }
            // 打开冰箱
            else if (refrigerator.activeSelf && refrigerator.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                refrigeratorOpen.SetActive(true);
            }
            // 获取刀
            if (cuttingToolKnife.activeSelf && cuttingToolKnife.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                Debug.Log("获取刀");
                GlobalManager.AddItem("13", Resources.Load<Sprite>("others/knife"));
                cuttingToolKnife.SetActive(false);
                if (cuttingToolScissors.activeSelf)
                {
                    // 未获得剪刀
                    cuttingTool.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("angle5/angle5-3-(2)");
                } else {
                    // 已获得剪刀
                    cuttingTool.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("angle5/angle5-3-(1)");
                }
            }
            // 获取剪刀
            if (cuttingToolScissors.activeSelf && cuttingToolScissors.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                Debug.Log("获取剪刀");
                GlobalManager.AddItem("14", Resources.Load<Sprite>("others/scissor"));
                cuttingToolScissors.SetActive(false);
                if (cuttingToolKnife.activeSelf)
                {
                    // 未获取刀
                    cuttingTool.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("angle5/angle5-3-(3)");
                } else {
                    // 已获取刀
                    cuttingTool.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("angle5/angle5-3-(1)");
                }
            }
        } else if(darkness2.activeSelf)
        {
            Debug.Log("太黑了，你无法进行探索");
        }
    }
    void HandleDrag(Vector3 worldTouchPosition, bool direction)
    {

    }
    void SetWord(int n,string txt)
    {
        wordCanvas.SetActive(true);
        word.text = txt;
        if (n == 2)
        {
            owner.text = "座机:";
        }
        if (n == 3)
        {
            owner.text = "未知的男性:";
        }
        else {
            owner.text = "J:";
        }
    }
}
