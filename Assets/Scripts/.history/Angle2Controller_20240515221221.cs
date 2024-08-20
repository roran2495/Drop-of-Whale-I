using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle2Controller : MonoBehaviour
{
    public GameObject doorToAngle7;
    public GameObject leftArrow;
    public GameObject backArrow;
    public GameObject television;
    public GameObject cabinet1;
    public GameObject cabinet1Content;
    public GameObject cabinet1Box;
    public GameObject cabinet2;
    public GameObject cabinet2Content;
    public GameObject cabinet2FirstAndKit;
    public GameObject cabinet3;
    public GameObject cabinet3Content;
    public GameObject cabinet3Pictures;
    public GameObject cabinet4;
    public GameObject cabinet4Content;
    public GameObject cabinet4Box;
    public GameObject cabinet5;
    public GameObject cabinet5Content;
    public GameObject cabinet5Keys;
    public GameObject cabinet6;
    public GameObject cabinet6Content;
    public GameObject cabinet6Instructions;
    public GameObject cabinet7;
    public GameObject cabinet7Content;
    public GameObject cabinet7Screwdriver;
    public GameObject picture0;
    public GameObject picture1;
    public GameObject picture2;
    public GameObject picture3;
    public GameObject instructions0;
    public GameObject instructions1;
    public GameObject instructions2;
    public GameObject instructions3;
    public GameObject instructions4;
    public GameObject instructions5;
    public GameObject box0;
    public GameObject box1;
    public GameObject box2;
    public GameObject box3;
    private bool flagCabinet2;  // donnot get : false  ;   already get : true
    private bool flagCabinet4;  // donnot open : false  ;   already open : true
    private bool flagCabinet5;
    private bool flagCabinet7;

    // Start is called before the first frame update
    void Start()
    {
        DisableSomeGameObject();
        SetFlag();
    }

    // Update is called once per frame
    void Update()
    {
        television.SetActive(Camera.main.transform.Find("darkness").gameObject.activeSelf);
        // 保证是触发视图2跳转
        if (Camera.main.transform.position.x == -30 && Camera.main.transform.position.y == 0)
        {
            // 检测触摸输入或鼠标点击
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
            {
                // 获取触摸位置或鼠标点击位置
                Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                // 将触摸位置的 Z 值设置为场景的 Z 值
                worldTouchPosition.z = doorToAngle7.transform.position.z;

                if(!television.activeSelf)  //黑暗状态
                {
                    // 点击柜子3照片 ： 显示特写照片
                    if (cabinet3Pictures.activeSelf && cabinet3Pictures.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                    {
                        Debug.Log("柜子3照片");
                        Camera.main.transform.Find("darkness").gameObject.SetActive(false);
                        picture0.SetActive(true);
                        picture1.SetActive(true);
                        DisableOtherColliders(picture0);
                    }
                    // 点击柜子3内容 ：关闭柜子3
                    else if (cabinet3Content.activeSelf && cabinet3Content.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                    {
                        cabinet3Content.SetActive(false);
                        cabinet3Pictures.SetActive(false);
                    } else {
                        // 点击柜子2急救箱 ：获取道具
                        if (cabinet2FirstAndKit.activeSelf && cabinet2FirstAndKit.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            cabinet2FirstAndKit.SetActive(false);
                            flagCabinet2 = true;
                        }
                        // 点击柜子2内容 ：关闭柜子2
                        else if (cabinet2Content.activeSelf && cabinet2Content.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            cabinet2Content.SetActive(false);
                            cabinet2FirstAndKit.SetActive(false);
                        } else {
                            // 点击柜子2 ：显示内容
                            if (cabinet2.activeSelf && cabinet2.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                            {
                                cabinet2Content.SetActive(true);
                                cabinet2FirstAndKit.SetActive(!flagCabinet2);
                            }
                            // 点击柜子3 ：显示内容
                            else if (cabinet3.activeSelf && cabinet3.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                            {
                                cabinet3Content.SetActive(true);
                                cabinet3Pictures.SetActive(true);
                            }
                            // 点击柜子4箱子 : 显示特写加密箱
                            else if (cabinet4Box.activeSelf && cabinet4Box.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                            {
                                Debug.Log("柜子4加密箱");
                            } else {
                                // 点击柜子4内容 ：关闭柜子4
                                if (cabinet4Content.activeSelf && cabinet4Content.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                                {
                                    cabinet4Content.SetActive(false);
                                    cabinet4Box.SetActive(false);
                                }
                                // 点击柜子6说明书 ：显示特写说明书
                                else if (cabinet6Instructions.activeSelf && cabinet6Instructions.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                                {
                                    Debug.Log("柜子6说明书");
                                }
                                // 点击柜子6内容 ：关闭柜子6
                                else if (cabinet6Content.activeSelf && cabinet6Content.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                                {
                                    cabinet6Content.SetActive(false);
                                    cabinet6Instructions.SetActive(false);
                                }
                                // 点击柜子7内容 ：关闭柜子7
                                else if (cabinet7Content.activeSelf && cabinet7Content.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                                {
                                    cabinet7Content.SetActive(false);
                                    cabinet7Screwdriver.SetActive(false);
                                }
                                // 点击柜子7螺丝刀 ：获取道具
                                else if (cabinet7Screwdriver.activeSelf && cabinet7Screwdriver.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                                {
                                    cabinet7Screwdriver.SetActive(false);
                                    SpriteRenderer spriteRenderer = cabinet7Content.GetComponent<SpriteRenderer>();
                                    Sprite newSprite = Resources.Load<Sprite>("angle2/angle2-9-(2)");
                                    spriteRenderer.sprite = newSprite; 
                                    flagCabinet7 = true;
                                } else {
                                    // 点击柜子7 ：显示内容
                                    if (cabinet7.activeSelf && cabinet7.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                                    {
                                        cabinet7Content.SetActive(true);
                                        cabinet7Screwdriver.SetActive(!flagCabinet7);
                                    }
                                    // 点击柜子5钥匙 ： 获取道具
                                    else if (cabinet5Keys.activeSelf && cabinet5Keys.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                                    {
                                        cabinet5Keys.SetActive(false);
                                        flagCabinet5 = true;
                                    }
                                    // 点击柜子5内容 ：关闭柜子5
                                    else if (cabinet5Content.activeSelf && cabinet5Content.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                                    {
                                        cabinet5Content.SetActive(false);
                                        cabinet5Keys.SetActive(false);
                                    } else {
                                        // 点击柜子5 ：显示内容
                                        if (cabinet5.activeSelf && cabinet5.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                                        {
                                            cabinet5Content.SetActive(true);
                                            cabinet5Keys.SetActive(!flagCabinet5);
                                        }
                                        // 点击柜子4 ：显示内容
                                        else if (cabinet4.activeSelf && cabinet4.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                                        {
                                            cabinet4Content.SetActive(true);
                                            cabinet4Box.SetActive(true);
                                        }
                                        // 点击柜子6 ：显示内容
                                        else if (cabinet6.activeSelf && cabinet6.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                                        {
                                            cabinet6Content.SetActive(true);
                                            cabinet6Instructions.SetActive(true);
                                        } 
                                        // 从视角2到视角7
                                        else if (doorToAngle7 && doorToAngle7.activeSelf && doorToAngle7.GetComponent<BoxCollider2D>().bounds.Contains(worldTouchPosition) && !doorToAngle7.GetComponent<CircleCollider2D>().bounds.Contains(worldTouchPosition))
                                        {
                                            Debug.Log("2->7");
                                            Camera.main.transform.position = new Vector3(60, 15, -10);
                                            leftArrow.SetActive(!leftArrow.activeSelf);
                                            backArrow.SetActive(!backArrow.activeSelf);
                                        }

                                    }
                                }
                            }
                        }
                    }

                    // 点击柜子1箱子 ： 文案交互
                    if (cabinet1Box.activeSelf && cabinet1Box.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                    {
                        Debug.Log("柜子1箱子");
                    }
                    // 点击柜子1 ：显示内容
                    else if (cabinet1.activeSelf && cabinet1.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                    {
                        cabinet1Content.SetActive(true);
                        cabinet1Box.SetActive(true);
                    }
                    // 点击柜子1内容 ：关闭柜子1
                    if (cabinet1Content.activeSelf && cabinet1Content.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                    {
                        cabinet1Content.SetActive(false);
                        cabinet1Box.SetActive(false);
                    }

                } else {
                    // 当前相片特写 点击相片外退出特写
                    if(picture0.activeSelf && )
                    {

                    }
                    // 当前说明书特写 点击说明书外退出特写
                    else if(instructions0.activeSelf)
                    {

                    }
                    // 当前加密盒子特写 点击加密盒子外退出特写
                    else if(box1.activeSelf || box2.activeSelf)
                    {

                    } else {
                        Debug.Log("太黑了，你无法进行探索");
                    }
                }
                
                // 滑动切换照片
                // 点击空白处退出照片特写
            }
        }
    }
    void DisableSomeGameObject()
    {
        cabinet1Content.SetActive(false);
        cabinet2Content.SetActive(false);
        cabinet3Content.SetActive(false);
        cabinet4Content.SetActive(false);
        cabinet5Content.SetActive(false);
        cabinet6Content.SetActive(false);
        cabinet7Content.SetActive(false);
        cabinet1Box.SetActive(false);
        cabinet2FirstAndKit.SetActive(false);
        cabinet3Pictures.SetActive(false);
        cabinet4Box.SetActive(false);
        cabinet5Keys.SetActive(false);
        cabinet6Instructions.SetActive(false);
        cabinet7Screwdriver.SetActive(false);
        picture0.SetActive(false);
        picture1.SetActive(false);
        picture2.SetActive(false);
        picture3.SetActive(false);
        instructions0.SetActive(false);
        instructions1.SetActive(false); 
        instructions2.SetActive(false);
        instructions3.SetActive(false);
        instructions4.SetActive(false);
        instructions5.SetActive(false);
        box0.SetActive(false);
        box1.SetActive(false);
        box2.SetActive(false);
        box3.SetActive(false);
        television.SetActive(false);
    }
    void SetFlag()
    {
        flagCabinet2 = false;
        flagCabinet4 = false;
        flagCabinet5 = false;
        flagCabinet7 = false;
    }
    public void FlagCurtainTransmit(bool flag)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Sprite newSprite = Resources.Load<Sprite>("angle2/angle2-1-(1)");
        if (flag)   //use curtain
        {
            newSprite = Resources.Load<Sprite>("angle2/angle2-1-(2)");
        }
        spriteRenderer.sprite = newSprite; 
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
