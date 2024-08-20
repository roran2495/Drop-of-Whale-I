using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangeBook : MonoBehaviour
{
    public GameObject exchangeBook;
    public GameObject temperaBook;
    private GameObject place2;
    private GameObject place3;
    private Game
    private GameObject place4;
    private GameObject place6;
    private GameObject place7;
    private GameObject place8;
    private GameObject place9;
    private GameObject place10;
    private Vector3 touchStartPosition; // 存储鼠标按下或开始触摸时的位置
    private Vector3 touchEndPosition; // 存储鼠标弹起或结束触摸时的位置
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.transform.position.x == 60 && Camera.main.transform.position.y == 0)
        {
            // 检测触摸开始或鼠标按下
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
            {
                // 获取触摸位置或鼠标点击位置
                Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                // 将触摸位置的 Z 值设置为场景的 Z 值
                worldTouchPosition.z = exchangeBook.transform.position.z;

                if (!Camera.main.transform.Find("darkness").gameObject.activeSelf)
                {
                    if (temperaBook.activeSelf && !temperaBook.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition) || temperaBookContent.activeSelf && !temperaBookContent.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition)
                        && exchangeBook.activeSelf && exchangeBook.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition) || temperaBookContent.activeSelf && !temperaBookContent.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                    {

                    } 
                }
                else
                {
                    Debug.Log("太黑了，你无法进行探索");
                }

            }
        }
    }
        // 交换书
    void ExchangeBooks()
    {
        GameObject place2 = exchangeBook.transform.Find("book2").gameObject;
        GameObject place3 = exchangeBook.transform.Find("book3").gameObject;
        GameObject place23 = exchangeBook.transform.Find("book2 3").gameObject;
        GameObject place4 = exchangeBook.transform.Find("book4").gameObject;
        GameObject place4l = exchangeBook.transform.Find("book4 left").gameObject;
        GameObject place4r = exchangeBook.transform.Find("book4 right").gameObject;
        GameObject place6 = exchangeBook.transform.Find("book6").gameObject;
        GameObject place7 = exchangeBook.transform.Find("book7").gameObject;
        GameObject place8 = exchangeBook.transform.Find("book8").gameObject;
        GameObject place9 = exchangeBook.transform.Find("book9").gameObject;
        GameObject place79 = exchangeBook.transform.Find("book7 9").gameObject;
        GameObject place10 = exchangeBook.transform.Find("book10").gameObject; 

        // 点击collider，判断是否在正确位置上，否则调用交换书，交换书本的sprite。
    }
}
