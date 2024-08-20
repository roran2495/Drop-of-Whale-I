using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangeBook : MonoBehaviour
{
    public GameObject exchangeBook;
    public GameObject temperaBook;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 保证是触发视图8跳转
        if (Camera.main.transform.position.x == 60 && Camera.main.transform.position.y == 0)
        {
            // 检测触摸开始或鼠标按下
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
            {
                // 获取触摸位置或鼠标点击位置
                Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                touchStartPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                // 将触摸位置的 Z 值设置为场景的 Z 值
                touchStartPosition.z = desk.transform.position.z;
            }
            // 检测触摸结束或鼠标弹起
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
            {
                // 获取触摸位置或鼠标点击位置
                Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                touchEndPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                // 将触摸位置的 Z 值设置为场景的 Z 值
                touchEndPosition.z = desk.transform.position.z;
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
