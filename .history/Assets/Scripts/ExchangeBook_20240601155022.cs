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
                    if (selected.Count == 3)
                    {
                        if (!bigBooks.Contains(selected[2]))
                        {
                            string bigBook = "";
                            if (selected[1] == "2" && selected[2] == "3" || selected[1] == "3" && selected[2] == "2")
                            {
                                bigBook = "23";
                            }
                            if (selected[1] == "4l" && selected[2] == "4r" || selected[1] == "4r" && selected[2] == "4l")
                            {
                                bigBook = "4";
                            }
                            if (selected[1] == "7" && selected[2] == "9" || selected[1] == "9" && selected[2] == "7")
                            {
                                bigBook = "79";
                            }
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
                                    UpdateFlagValue(bigBook, pair.value);
                                    UpdateFlagValue(selected[0], pairPre.value);
                                    UpdateMapping(bigBook, pair.value);
                                    UpdateMapping(selected[0], pairPre.value);
                                }

                                string smallBook1 = "", smallBook2 = "";
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

                                if (mappings.TryGetValue(smallBook1, out pair) && mappings.TryGetValue(selected[1], out pairPre))
                                {
                                    pair.obj.SetActive(true);
                                    pair.obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("angle8/angle8-9-(" + pairPre.value + ")-(" + smallBook1 + ")");
                                    pair.value = pairPre.value;
                                    pairPre.obj.SetActive(false);
                                    pairPre.value = 0;
                                    UpdateFlagValue(smallBook1, pair.value);
                                    UpdateFlagValue(selected[1], pairPre.value);
                                    UpdateMapping(smallBook1, pair.value);
                                    UpdateMapping(selected[1], pairPre.value);
                                }
                                if (mappings.TryGetValue(smallBook2, out pair) && mappings.TryGetValue(selected[2], out pairPre))
                                {
                                    pair.obj.SetActive(true);
                                    pair.obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("angle8/angle8-9-(" + pairPre.value + ")-(" + smallBook2 + ")");
                                    pair.value = pairPre.value;
                                    pairPre.obj.SetActive(false);
                                    pairPre.value = 0;
                                    UpdateFlagValue(smallBook2, pair.value);
                                    UpdateFlagValue(selected[2], pairPre.value);
                                    UpdateMapping(smallBook2, pair.value);
                                    UpdateMapping(selected[2], pairPre.value);
                                }
                            }
                            else
                            {
                                selected.Clear();
                            }
                        }
                        else
                        {
                            selected.Clear();
                        }
                    }
                    else if (selected.Count == 2)
                    {
                        if (bigBooks.Contains(selected[0]) && bigBooks.Contains(selected[1]) 
                            || !bigBooks.Contains(selected[0]) && !bigBooks.Contains(selected[1]))
                        {
                            (GameObject obj, int value) pair0;
                            (GameObject obj, int value) pair1;
                            if (mappings.TryGetValue(selected[0], out pair0) && mappings.TryGetValue(selected[1], out pair1))
                            {
                                int intValue0 = pair0.value;
                                pair0.obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("angle8/angle8-9-(" + pair1.value + ")-(" + selected[0] + ")");
                                pair0.value = pair1.value;
                                pair1.obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("angle8/angle8-9-(" + intValue0 + ")-(" + selected[1] + ")");
                                pair1.value = intValue0;
                                UpdateFlagValue(selected[0], pair0.value);
                                UpdateFlagValue(selected[1], pair1.value);
                                UpdateMapping(selected[0], pair0.value);
                                UpdateMapping(selected[1], pair1.value);
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
                    if (temperaBook.active
