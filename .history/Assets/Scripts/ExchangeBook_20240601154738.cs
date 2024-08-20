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
    private Dictionary<string, (GameObject, int)> mappings;

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

        // 初始化字典
        mappings = new Dictionary<string, (GameObject, int)>
        {
            { "2", (place2, flagPlace2) },
            { "3", (place3, flagPlace3) },
            { "23", (place23, flagPlace23) },
            { "4", (place4, flagPlace4) },
            { "4l", (place4l, flagPlace4l) },
            { "4r", (place4r, flagPlace4r) },
            { "6", (place6, flagPlace6) },
            { "7", (place7, flagPlace7) },
            { "8", (place8, flagPlace8) },
            { "9", (place9, flagPlace9) },
            { "79", (place79, flagPlace79) },
            { "10", (place10, flagPlace10) },
        };

        selected = new List<string>();
    }

    void Update()
    {
        if (Camera.main.transform.position.x == 60 && Camera.main.transform.position.y == 0)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
            {
                Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                worldTouchPosition.z = exchangeBook.transform.position.z;

                if (!Camera.main.transform.Find("darkness").gameObject.activeSelf)
                {

                    if (temperaBook.activeSelf && !temperaBook.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition)
                        && exchangeBook.activeSelf && exchangeBook.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                    {
                        if (flagPlace3 != 0 && place3.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            selected.Add("3");
                            Debug.Log("点击3 " + selected.Count);
                        }
                        else if (flagPlace2 != 0 && place2.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            selected.Add("2");
                            Debug.Log("点击2 " + selected.Count);
                        }
                        else if (flagPlace23 != 0 && place23.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            selected.Add("23");
                            Debug.Log("点击23 " + selected.Count);
                        }
                        if (flagPlace4r != 0 && place4r.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            selected.Add("4r");
                            Debug.Log("点击4r " + selected.Count);
                        }
                        else if (flagPlace4l != 0 && place4l.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            selected.Add("4l");
                            Debug.Log("点击4l " + selected.Count);
                        }
                        else if (flagPlace4 != 0 && place4.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            selected.Add("4");
                            Debug.Log("点击4 " + selected.Count);
                        }
                        if (flagPlace6 != 0 && place6.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            selected.Add("6");
                            Debug.Log("点击6 " + selected.Count);
                        }
                        if (flagPlace9 != 0 && place9.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            selected.Add("9");
                            Debug.Log("点击9 " + selected.Count);
                        }
                        else if (flagPlace7 != 0 && place7.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            selected.Add("7");
                            Debug.Log("点击7 " + selected.Count);
                        }
                        else if (flagPlace79 != 0 && place79.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            selected.Add("79");
                            Debug.Log("点击79 " + selected.Count);
                        }
                        if (flagPlace8 != 0 && place8.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            selected.Add("8");
                            Debug.Log("点击8 " + selected.Count);
                        }
                        if (flagPlace10 != 0 && place10.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            selected.Add("10");
                            Debug.Log("点击10 " + selected.Count);
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

    // 更新 flagPlace 变量的值
    void UpdateFlagValue(string key, int newValue)
    {
        switch (key)
        {
            case "2": flagPlace2 = newValue; break;
            case "3": flagPlace3 = newValue; break;
            case "23": flagPlace23 = newValue; break;
            case "4": flagPlace4 = newValue; break;
            case "4l": flagPlace4l = newValue; break;
            case "4r": flagPlace4r = newValue; break;
            case "6": flagPlace6 = newValue; break;
            case "7": flagPlace7 = newValue; break;
            case "8": flagPlace8 = newValue; break;
            case "9": flagPlace9 = newValue; break;
            case "79": flagPlace79 = newValue; break;
            case "10": flagPlace10 = newValue; break;
        }
    }
}
