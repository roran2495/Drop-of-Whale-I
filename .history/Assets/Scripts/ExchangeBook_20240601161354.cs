using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private string[] bigBooks = {"23", "4", "79"};
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
        place23.SetActive(false);
        place4.SetActive(false);
        place7.SetActive(false);
        place9.SetActive(false);
        flagPlace2 = 8;flagPlace3 = 10;flagPlace23 = 0;flagPlace4 = 0;flagPlace4l = 3;flagPlace4r =7;
        flagPlace6 = 2;flagPlace7 = 0;flagPlace8 = 6;flagPlace9 = 0;flagPlace79 = 4;flagPlace10 = 9;

        selected = new List<string>();
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
                    if (temperaBook.activeSelf && !temperaBook.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition)
                        && exchangeBook.activeSelf && exchangeBook.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                    {
                        if (flagPlace3 !=0 && place3.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            selected.Add("3");
                            Debug.Log("点击3 " + selected.Count);
                        }
                        else if (flagPlace2 !=0 && place2.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            selected.Add("2");
                            Debug.Log("点击2 " + selected.Count);
                        }
                        else if (flagPlace23 !=0 && place23.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            selected.Add("23");
                            Debug.Log("点击23 " + selected.Count);
                        }
                        if (flagPlace4r !=0 && place4r.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            selected.Add("4r");
                            Debug.Log("点击4r " + selected.Count);
                        }
                        else if (flagPlace4l !=0 && place4l.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            selected.Add("4l");
                            Debug.Log("点击4l " + selected.Count);
                        }
                        else if (flagPlace4 !=0 && place4.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            selected.Add("4");
                            Debug.Log("点击4 " + selected.Count);
                        }
                        if (flagPlace6 !=0 && place6.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            selected.Add("6");
                            Debug.Log("点击6 " + selected.Count);
                        }
                        if (flagPlace9 !=0 && place9.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            selected.Add("9");
                            Debug.Log("点击9 " + selected.Count);
                        }
                        else if (flagPlace7 !=0 && place7.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            selected.Add("7");
                            Debug.Log("点击7 " + selected.Count);
                        }
                        else if (flagPlace79 !=0 && place79.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            selected.Add("79");
                            Debug.Log("点击79 " + selected.Count);
                        }
                        if (flagPlace8 !=0 && place8.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            selected.Add("8");
                            Debug.Log("点击8 " + selected.Count);
                        }
                        if (flagPlace10 !=0 && place10.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            selected.Add("10");
                            Debug.Log("点击10 " + selected.Count);
                        }
                    } 

                    if (selected.Count == 3)
                    {
                        // 1. 大小大 重置
                        // 2. 大小小 大和小小且符合小小相邻条件换
                        // 3. 其他 重置
                        if (!bigBooks.Contains(selected[2]))
                        {
                            // 对于小书，先判断是否相邻，若相邻获得合并的大书位置标记
                            string bigBook = "";
                            string smallBook1 = "", smallBook2 = "";
                            if (selected[1] == "2" && selected [2] == "3" || selected[1] == "3" && selected [2] == "2")
                            {
                                bigBook = "23";
                            }
                            if (selected[1] == "4l" && selected [2] == "4r" || selected[1] == "4r" && selected [2] == "4l")
                            {
                                bigBook = "4";
                            }
                            if (selected[1] == "7" && selected [2] == "9" || selected[1] == "9" && selected [2] == "7")
                            {
                                bigBook = "79";
                            }
                            // 对于大书，若小书相邻，则大书直接给合并的大书，并获取拆分的小书位置标记
                            if (bigBook != "")
                            {
                                ExchangeBooks(bigBook, selected[0]);
                                FindAndToggleActive(bigBook);
                                if (selected[0] == "23")
                                {
                                    smallBook1 = "2";
                                    smallBook2 = "3";
                                }
                                if (selected[0] == "4")
                                {
                                    smallBook1 = "4l";
                                    smallBook2 = "4r";
                                }
                                if (selected[0] == "79")
                                {
                                    smallBook1 = "7";
                                    smallBook2 = "9";
                                }
                                // 小书给拆分的小书位置标记
                                ExchangeBooks(smallBook1, selected[1]);
                                FindAndToggleActive(smallBook1);
                                FindAndToggleActive(selected[1]);

                                ExchangeBooks(smallBook2, selected[2]);
                                FindAndToggleActive(smallBook2);
                                FindAndToggleActive(selected[2]);
                            }
                        }
                        selected.Clear();
                    }
                    else if (selected.Count == 2)
                    {
                        // 两大换(不存在)，两小换，一大一小将大的放在0号位
                        if (!bigBooks.Contains(selected[0]) && !bigBooks.Contains(selected[1]))
                        {
                            ExchangeBooks(selected[0], selected[1]);

                            selected.Clear();
                        }
                        else
                        {
                            if (bigBooks.Contains(selected[1]))
                            {
                                string tmp = selected[0];
                                selected[0] = selected[1];
                                selected[1] = tmp;
                            }
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

    void ExchangeBooks(string book1, string book2)
    {
        GameObject bookPlace1,bookPlace2;  //待放置的位置
        int bookNum1, bookNum2; //待放置位置对应的书（目标书）
        bookPlace1 = FindBookPlace(book1);
        bookPlace2 = FindBookPlace(book2);

        bookNum1 = FindBookNum(book1);
        bookNum1 = FindBookNum(book2);
        ExchangeBooksByNum(bookNum1, bookNum2);
        
    }
    int FindBookNum(string str)
    {
        switch (str)
        {
            case "2": return flagPlace2;
            case "3": return flagPlace3;
            case "23": return flagPlace23;
            case "4": return flagPlace4;
            case "4l": return flagPlace4l;
            case "4r": return flagPlace4r;
            case "6": return flagPlace6;
            case "7": return flagPlace7;
            case "8": return flagPlace8;
            case "9": return flagPlace9;
            case "79": return flagPlace79;
            case "10": return flagPlace10;
            default: return -1;
        }
    }
    void ExchangeBooksByNum(int bookNum1, int bookNum2)
    {
        switch(bookNum1)
        {
            case 2:
            case 3:
            case 4:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
        }
    }
    GameObject FindBookPlace(string str)
    {
        switch (str)
        {
            case "2": return place2;
            case "3": return place3;
            case "23": return place23;
            case "4": return place4;
            case "4l": return place4l;
            case "4r": return place4r;
            case "6": return place6;
            case "7": return place7;
            case "8": return place8;
            case "9": return place9;
            case "79": return place79;
            case "10": return place10;
            default: return null;
        }
    }
    void FindAndToggleActive(string str)
    {
        switch (str)
        {
            case "2": place2.SetActive(!place2.activeSelf);break;
            case "3": place3.SetActive(!place3.activeSelf); break;
            case "23": place23.SetActive(!place23.activeSelf); break;
            case "4": place4.SetActive(!place4.activeSelf); break;
            case "4l": place4l.SetActive(!place4l.activeSelf); break;
            case "4r": place4r.SetActive(!place4r.activeSelf); break;
            case "6": place6.SetActive(!place6.activeSelf); break;
            case "7": place7.SetActive(!place7.activeSelf); break;
            case "8": place8.SetActive(!place8.activeSelf); break;
            case "9": place9.SetActive(!place9.activeSelf); break;
            case "79": place79.SetActive(!place79.activeSelf); break;
            case "10": place10.SetActive(!place10.activeSelf); break;
        }
    }
}