using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle2Controller : MonoBehaviour
{
    public GameObject doorToAngle7;
    public GameObject leftArrow;
    public GameObject backArrow;
    public GameObject useKey;
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
    public GameObject password1;
    public GameObject password2;
    public GameObject password3;
    public GameObject password4;
    private int psw1;
    private int psw2;
    private int psw3;
    private int psw4;
    public GameObject open;
    public GameObject close;
    public GameObject letters;
    private bool flagCabinet2;  // donnot get : false  ;   already get : true
    private bool flagCabinet4;  // donnot open : false  ;   already open : true
    private bool flagCabinet5;
    private bool flagCabinet7;
    private bool flagUseKey; // 使用钥匙-》柜子开锁 -》true
    private Vector3 touchStartPosition; // 存储鼠标按下或开始触摸时的位置
    private Vector3 touchEndPosition; // 存储鼠标弹起或结束触摸时的位置
    private GameObject currentGameObject; // 用于说明书仅使用一个collider

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
            // 检测触摸开始或鼠标按下
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
            {
                // 获取触摸位置或鼠标点击位置
                Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                touchStartPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                // 将触摸位置的 Z 值设置为场景的 Z 值
                touchStartPosition.z = doorToAngle7.transform.position.z;
            }
            // 检测触摸结束或鼠标弹起
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
            {
                // 获取触摸位置或鼠标点击位置
                Vector3 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                touchEndPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                // 将触摸位置的 Z 值设置为场景的 Z 值
                touchEndPosition.z = doorToAngle7.transform.position.z;
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
    void HandleClick(Vector3 worldTouchPosition)
    {
        if(!television.activeSelf)  //黑暗状态
        {
            if (!flagUseKey && useKey.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
            {
                Item item = GlobalManager.FindItem("01");
                if (item != null && item.isSelected)
                {
                    flagUseKey = true;
                    GlobalManager.RemoveItem(item);
                }
                else
                {
                    Debug.Log("上锁了，你无法打开");
                }
            }
            else
            {

            }

        } 
        else 
        {
            // 当前相片特写 点击相片外退出特写
            if(picture0.activeSelf)
            {
                if (!picture0.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    Camera.main.transform.Find("darkness").gameObject.SetActive(false);
                    picture0.SetActive(false);
                    picture1.SetActive(false);
                    picture2.SetActive(false);
                    picture3.SetActive(false);
                    EnableOtherColliders();
                }
            } 
            // 当前说明书特写 点击说明书外退出特写 说明书1-5collider用的1
            else if (instructions0.activeSelf) 
            {
                if (instructions1.activeSelf && !instructions1.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition)
                || !instructions1.activeSelf && !instructions0.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    Camera.main.transform.Find("darkness").gameObject.SetActive(false);
                    instructions0.SetActive(false);
                    instructions1.SetActive(false);
                    instructions2.SetActive(false);
                    instructions3.SetActive(false);
                    instructions4.SetActive(false);
                    instructions5.SetActive(false);
                    EnableOtherColliders();
                }
            } 
            // 当前加密盒子特写 
            else if(box0.activeSelf)
            {
                // 点击加密盒子外退出特写
                if (box2.activeSelf){
                    if (!box2.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                    {
                        box2.SetActive(false);
                        if (!box1.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            box1.SetActive(false);
                            if (!box0.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                            {
                                box0.SetActive(false);
                                Camera.main.transform.Find("darkness").gameObject.SetActive(false);
                                EnableOtherColliders();
                            }
                        }
                    
                    }   
                } 
                else if (box1.activeSelf)
                {
                    if (!box1.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                    {
                        box1.SetActive(false);
                        if (!box0.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            box0.SetActive(false);
                            Camera.main.transform.Find("darkness").gameObject.SetActive(false);
                            EnableOtherColliders();
                        }
                    } 
                    // box1 ---letters---> box2
                    else if (letters.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                    {
                        box2.SetActive(true);
                    }
                    // box1 ---close---> box0
                    else if (close.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                    {
                        box1.SetActive(false);
                    }
                }
                else if (!box0.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                {
                    box0.SetActive(false);
                    Camera.main.transform.Find("darkness").gameObject.SetActive(false);
                    EnableOtherColliders();
                }
                // box0 ---> passoword correct ---open---> box1
                else
                {
                    if (!flagCabinet4)   // need right password
                    {
                        if (password1.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            psw1 = (psw1 + 1) % 10;
                            password1.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("others/number-" + psw1);
                        }
                        if (password2.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            psw2 = (psw2 + 1) % 10;
                            password2.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("others/number-" + psw2);
                        }
                        if (password3.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            psw3 = (psw3 + 1) % 10;
                            password3.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("others/number-" + psw3);
                        }
                        if (password4.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                        {
                            psw4 = (psw4 + 1) % 10;
                            password4.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("others/number-" + psw4);
                        }
                        if (psw1 == 9 && psw2 == 1 && psw3 == 7 && psw4 == 4)
                        {
                            flagCabinet4 = true;
                            password1.GetComponent<PolygonCollider2D>().enabled = false;
                            password2.GetComponent<PolygonCollider2D>().enabled = false;
                            password3.GetComponent<PolygonCollider2D>().enabled = false;
                            password4.GetComponent<PolygonCollider2D>().enabled = false;
                        }
                    }
                    if (flagCabinet4 && open.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
                    {
                        password1.SetActive(false);
                        password2.SetActive(false);
                        password3.SetActive(false);
                        password4.SetActive(false);
                        box1.SetActive(true);
                    }    
                }
            } else {
                Debug.Log("太黑了，你无法进行探索");
            }
        }
    }
    void HandleDrag(Vector3 worldTouchPosition , bool direction)
    {
        //照片特写
        if (picture0.activeSelf && picture0.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition))
        {
            if (direction)  // true 左->右 132
            {
                if (picture1.activeSelf)
                {
                    picture1.SetActive(false);
                    picture3.SetActive(true);
                } 
                else if (picture3.activeSelf)
                {
                    picture3.SetActive(false);
                    picture2.SetActive(true);
                }
                else
                {
                    picture2.SetActive(false);
                    picture1.SetActive(true);
                }
            }
            else    //false 右->左 123
            {
                if (picture1.activeSelf)
                {
                    picture1.SetActive(false);
                    picture2.SetActive(true);
                } 
                else if (picture2.activeSelf)
                {
                    picture2.SetActive(false);
                    picture3.SetActive(true);
                }
                else
                {
                    picture3.SetActive(false);
                    picture1.SetActive(true);
                }
            }
        }
        // 说明书特写
        else if (instructions0.activeSelf && (instructions1.activeSelf && instructions1.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition)
            || !instructions1.activeSelf && instructions0.GetComponent<PolygonCollider2D>().bounds.Contains(worldTouchPosition)))
        {
            if (direction)  // true 左->右 054321
            {
                if (currentGameObject == instructions0)
                {
                    instructions1.SetActive(true);
                    instructions1.GetComponent<PolygonCollider2D>().enabled = true;
                    instructions5.SetActive(true);
                    currentGameObject = instructions5;
                } 
                else if (currentGameObject == instructions5)
                {
                    instructions5.SetActive(false);
                    instructions4.SetActive(true);
                    currentGameObject = instructions4;
                }
                else if (currentGameObject == instructions4)
                {
                    instructions4.SetActive(false);
                    instructions3.SetActive(true);
                    currentGameObject = instructions3;
                }
                else if (currentGameObject == instructions3)
                {
                    instructions3.SetActive(false);
                    instructions2.SetActive(true);
                    currentGameObject = instructions2;
                }
                else if (currentGameObject == instructions2)
                {
                    instructions2.SetActive(false);
                    currentGameObject = instructions1;
                }
                else
                {
                    instructions1.SetActive(false);
                    currentGameObject = instructions0;
                }

            }  
            else //false 右->左 012345
            {
                if (currentGameObject == instructions0)
                {
                    currentGameObject =instructions1;
                    instructions1.SetActive(true);
                    instructions1.GetComponent<PolygonCollider2D>().enabled = true;
                    currentGameObject = instructions1;
                } 
                else if (currentGameObject == instructions1)
                {
                    instructions2.SetActive(true);
                    currentGameObject = instructions2;
                }
                else if (currentGameObject == instructions2)
                {
                    instructions2.SetActive(false);
                    instructions3.SetActive(true);
                    currentGameObject = instructions3;
                }
                else if (currentGameObject == instructions3)
                {
                    instructions3.SetActive(false);
                    instructions4.SetActive(true);
                    currentGameObject = instructions4;
                }
                else if (currentGameObject == instructions4)
                {
                    instructions4.SetActive(false);
                    instructions5.SetActive(true);
                    currentGameObject = instructions5;
                }
                else
                {
                    instructions5.SetActive(false);
                    instructions1.SetActive(false);
                    currentGameObject = instructions0;
                }
            }          
        }    
    }
    void EnableSomeCollider()
    {
        password1.GetComponent<PolygonCollider2D>().enabled = true;
        password2.GetComponent<PolygonCollider2D>().enabled = true;
        password3.GetComponent<PolygonCollider2D>().enabled = true;
        password4.GetComponent<PolygonCollider2D>().enabled = true;
        open.GetComponent<PolygonCollider2D>().enabled = true;
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
        television.SetActive(false);
    }
    void SetFlag()
    {
        flagCabinet2 = false;
        flagCabinet4 = false;
        flagCabinet5 = false;
        flagCabinet7 = false;
        flagUseKey = false;
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
    void EnableOtherColliders()
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
