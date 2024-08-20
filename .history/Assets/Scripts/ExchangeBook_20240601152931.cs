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
        // 创造字典便于管理和访问
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
                                (GameObject obj, int value) pair;
                                (GameObject obj, int value) pairPre;
                                if (mappings.TryGetValue(bigBook, out pair) && mappings.TryGetValue(selected[0], out pairPre))
                                {
                                    pair.obj.SetActive(true);
                                    pair.obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("angle8/angle8-9-(" + pairPre.value + ")-(" + bigBook + ")");
                                    pair.value = pairPre.value;
                                    pairPre.obj.SetActive(false);
                                    pairPre.value = 0;
                                }
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
                                if (mappings.TryGetValue(smallBook1, out pair) && mappings.TryGetValue(selected[1], out pairPre))
                                {
                                    pair.obj.SetActive(true);
                                    pairPre.value = pairPre.value==3 ? 2 : pairPre.value;
                                    pair.obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("angle8/angle8-9-(" + pairPre.value + ")-(" + smallBook1 + ")");
                                    pair.value = pairPre.value;
                                    pairPre.obj.SetActive(false);
                                    pairPre.value = 0;
                                }
                                if (mappings.TryGetValue(smallBook2, out pair) && mappings.TryGetValue(selected[2], out pairPre))
                                {
                                    pair.obj.SetActive(true);
                                    pair.value = pairPre.value==3 ? 2 : pairPre.value;
                                    pair.obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("angle8/angle8-9-(" + pairPre.value + ")-(" + smallBook2 + ")");
                                    pair.value = pairPre.value;
                                    pairPre.obj.SetActive(false);
                                    pairPre.value = 0;
                                }
                            }
                        }
                        Debug.Log("1: " +selected.Count);
                        selected.Clear();
                        Debug.Log("1.1 : "+selected.Count);
                    }
                    else if (selected.Count == 2)
                    {
                        // 两大换(不存在)，两小换，一大一小将大的放在0号位
                        if (!bigBooks.Contains(selected[0]) && !bigBooks.Contains(selected[1]))
                        {
                            (GameObject obj, int value) pair0;
                            (GameObject obj, int value) pair1;
                            if (mappings.TryGetValue(selected[0], out pair0) && mappings.TryGetValue(selected[1], out pair1))
                            {
                                Debug.Log("pair0.value+"   "+pair1.value);

                                int intValue0 = pair0.value;
                                pair1.value = pair1.value==3? 2 : pair1.value;
                                pair0.obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("angle8/angle8-9-(" + pair1.value + ")-(" + selected[0] + ")");
                                pair0.value = pair1.value;
                                intValue0 = intValue0==3 ? 2 : intValue0;
                                pair1.obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("angle8/angle8-9-(" + intValue0 + ")-(" + selected[1] + ")");
                                pair1.value = intValue0;
                            }
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
}