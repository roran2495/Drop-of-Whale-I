using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle1Controller : MonoBehaviour
{
    public GameObject doorToAngle5;
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject backArrow;
    public GameObject darkness;
    public GameObject curtain;
    public bool flagCurtain;    // unuse : false   ;     use: true
    public GameObject switcher;   
    public bool flagSwitcher;   // turn on : false ;      turn off : true
    public GameObject pillow1;
    public GameObject pillow2;
    public GameObject broom1;
    public GameObject broom2;
    public GameObject carpet1;
    public GameObject carpet2;
    public GameObject letter1;
    public GameObject letter2;
    public GameObject phone1;
    public GameObject phone2;
    public GameObject shoeCabinet1;
    public GameObject shoeCabinet2;
    // Start is called before the first frame update
    void Start()
    {
        flagCurtain = false;
        flagSwitcher = false;
        pillow2.SetActive(false);
        broom2.SetActive(false);
        carpet2.SetActive(false);
        letter2.SetActive(false);
        phone2.SetActive(false);
        shoeCabinet2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 保证是触发视图1跳转
        if (Camera.main.transform.position.x == 0 && Camera.main.transform.position.y == 0)
        {
            // 检测触摸输入或鼠标点击
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
            {
                // 获取触摸位置或鼠标点击位置
                Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                // 将触摸位置的 Z 值设置为场景的 Z 值
                worldTouchPosition.z = doorToAngle5.transform.position.z;

                if(!darkness.activeSelf)
                {
                    // 从视角1到视角5
                    if (doorToAngle5.activeSelf && doorToAngle5.GetComponent<Collider2D>().bounds.Contains(worldTouchPosition))
                    {
                        Debug.Log("5->1");
                        Camera.main.transform.position = new Vector3(0, 15, -10);
                        leftArrow.SetActive(!leftArrow.activeSelf);
                        rightArrow.SetActive(!rightArrow.activeSelf);
                        backArrow.SetActive(!backArrow.activeSelf);
                    }

                    
                }
                
                // 点击窗帘 ： 不使用or使用
                if (curtain.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition) && !curtain.GetComponent<CircleCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    Debug.Log("点击窗帘");
                    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                    Sprite newSprite = Resources.Load<Sprite>("angle1/angle1-1-(1)");
                    if (!flagCurtain) // unuse -> use
                    {
                        newSprite = Resources.Load<Sprite>("angle1/angle1-1-(2)");
                    }
                    flagCurtain = !flagCurtain;
                    Angle2Controller angle2ControllerInstance = GameObject.Find("Angle2").GetComponent<Angle2Controller>();
                    angle2ControllerInstance.FlagCurtainTransmit(flagCurtain); 
                    spriteRenderer.sprite = newSprite; 
                }
                // 点击开关：打开or关闭
                if (switcher.activeSelf && switcher.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    Debug.Log("点击开关");
                    flagSwitcher = !flagSwitcher;
                }
                
                
            }
        }
    }
    void DisableOtherColliders(GameObject currentGameObject)
    {
        // 获取场景中所有的Collider组件
        Collider2D[] colliders = FindObjectsOfType<Collider2D>();
        
        // 遍历所有Collider组件，禁用除了特写对象以外的所有其他对象的碰撞器
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != currentGameObject)
            {
                collider.enabled = false;
            }
        }
    }
    void EnableOtherColliders(GameObject currentGameObject)
    {
        // 获取场景中所有的Collider组件
        Collider2D[] colliders = FindObjectsOfType<Collider2D>();

        // 遍历所有Collider组件，禁用除了特写对象以外的所有其他对象的碰撞器
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = true;
        }
    }
}
