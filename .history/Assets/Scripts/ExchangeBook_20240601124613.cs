using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangeBook : MonoBehaviour
{
    public GameObject exchangeBook;
    public GameObject temperaBook;
    private GameObject place2;
    private GameObject place3;
    private GameObject place23;
    private GameObject place4;
    private GameObject place4l;
    private GameObject place4r;
    private GameObject place6;
    private GameObject place7;
    private GameObject place8;
    private GameObject place9;
    private GameObject place79;
    private GameObject place10;
    private int flagPlace2, flagPlace3, flagPlace23, flagPlace4, flagPlace4l, flagPlace4r, flagPlace6, flagPlace7, flagPlace8, flagPlace9, flagPlace79, flagPlace10; 
    private List<string> selected;
    // Start is called before the first frame update
    void Start()
    {
        place2 = exchangeBook.transform.Find("book2").gameObject;
        place3 = exchangeBook.transform.Find("book3").gameObject;
        place23 = exchangeBook.transform.Find("book2 3").gameObject;
        place4 = exchangeBook.transform.Find("book4").gameObject;
        place4l = exchangeBook.transform.Find("book4 left").gameObject;
        place4r = exchangeBook.transform.Find("book4 right").gameObject;
        place6 = exchangeBook.transform.Find("book6").gameObject;
        place7 = exchangeBook.transform.Find("book7").gameObject;
        place8 = exchangeBook.transform.Find("book8").gameObject;
        place9 = exchangeBook.transform.Find("book9").gameObject;
        place79 = exchangeBook.transform.Find("book7 9").gameObject;
        place10 = exchangeBook.transform.Find("book10").gameObject; 
        flagPlace2 = 8;flagPlace3 = 10;flagPlace23 = 0;flagPlace4 = 0;flagPlace4l = 3;flagPlace4r =7;
        flagPlace6 = 2;flagPlace7 = 0;flagPlace8 = 6;flagPlace9 = 0;flagPlace79 = 4;flagPlace10 = 9;
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
                    if (selected.Count >= 2)
                    {
                        
                    }
                    if (temperaBook.activeSelf && !temperaBook.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition)
                        && exchangeBook.activeSelf && exchangeBook.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                    {
                        if (flagPlace2 !=0 && place2.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            selected.Add("2");
                        }
                        if (flagPlace3 !=0 && place3.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {

                        }
                        if (flagPlace23 !=0 && place23.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {

                        }
                        if (flagPlace4 !=0 && place4.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {

                        }
                        if (flagPlace4l !=0 && place4l.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {

                        }
                        if (flagPlace4r !=0 && place4r.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {

                        }
                        if (flagPlace6 !=0 && place6.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {

                        }
                        if (flagPlace7 !=0 && place7.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {

                        }
                        if (flagPlace8 !=0 && place8.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {

                        }
                        if (flagPlace9 !=0 && place9.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {

                        }
                        if (flagPlace79 !=0 && place79.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {

                        }
                        if (flagPlace10 !=0 && place10.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {

                        }
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
    void ExchangeBooks(GameObject gameObject1, GameObject gameObject2)
    {
        
        // 点击collider，判断是否在正确位置上，否则调用交换书，交换书本的sprite。
    }
}
