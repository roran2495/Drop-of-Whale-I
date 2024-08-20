using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangeBook : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 点击蛋彩画书，进入特写
        if (temperaBook.activeSelf && temperaBook.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
        {
            temperaBookCover.SetActive(true);
            temperaBookContent.SetActive(false);
            DisableOtherColliders(temperaBookCover);
            temperaBookContent.GetComponent<PolygonCollider2D>().enabled = true;
            Camera.main.transform.Find("darkness").gameObject.SetActive(true);
        }
        // 书架
            else if (exchangeBook.activeSelf && exchangeBook.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                ExchangeBooks();
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
