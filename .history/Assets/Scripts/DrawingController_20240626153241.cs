using System.Collections;
using UnityEngine;

public class DrawingController : MonoBehaviour
{
    public Color drawColor = Color.black;   // 绘制的颜色
    public float lineWidth = 0.1f;          // 线条宽度

    private bool isDrawing = false;         // 是否正在绘制
    private Transform drawingParent;        // 线条的父对象
    private LineRenderer currentLine;       // 当前的线条组件
    private SpriteRenderer spriteRenderer;  // 当前游戏对象的SpriteRenderer

    void Start()
    {
        drawingParent = new GameObject("Drawing").transform;  // 创建一个空的GameObject作为线条的父对象
        drawingParent.parent = transform;  // 设置父对象为当前游戏对象
        spriteRenderer = GetComponent<SpriteRenderer>();  // 获取当前游戏对象的SpriteRenderer
    }

    void Update()
    { 
        Item item = GlobalManager.FindItem("25");
        if (item != null && item.isSelected)
        {
            drawColor = item.color;
            // 检测鼠标按下或触摸移动
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = GetMousePosition();
                if (IsMousePositionInBounds(mousePos))
                {
                    StartDrawing();
                }
            }
            else if (Input.GetMouseButton(0) && isDrawing)
            {
                Vector3 mousePos = GetMousePosition();
                if (IsMousePositionInBounds(mousePos))
                {
                    ContinueDrawing(mousePos);
                }
                else
                {
                    StopDrawing();
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                StopDrawing();
            }
        }
        else
        {
            if (GlobalManager.someItemIsSelected)
            {
                Debug.Log("你无法使用这个进行绘画");
            }
            else
            {
                Debug.Log("你需要颜料才能绘画");
            }
        }
        
    }

    void StartDrawing()
    {
        isDrawing = true;
        GameObject lineObject = new GameObject("Line");
        lineObject.transform.parent = drawingParent; // 设置线条的父对象为drawingParent

        currentLine = lineObject.AddComponent<LineRenderer>();

        // 设置线条参数
        currentLine.startWidth = lineWidth;
        currentLine.endWidth = lineWidth;
        
        // 设置材质和颜色
        currentLine.material = new Material(Shader.Find("Sprites/Default")); // 使用默认的精灵Shader
        currentLine.startColor = drawColor;
        currentLine.endColor = drawColor;

        // 设置sorting layer和order in layer
        currentLine.sortingLayerName = "features"; // 设置sorting layer为"features"
        currentLine.sortingOrder = 1; // 设置order in layer为1

        // 添加第一个点
        Vector3 mousePos = GetMousePosition();
        currentLine.positionCount = 1;
        currentLine.SetPosition(0, mousePos);
    }

    void ContinueDrawing(Vector3 mousePos)
    {
        // 添加新的点
        currentLine.positionCount++;
        currentLine.SetPosition(currentLine.positionCount - 1, mousePos);
    }

    void StopDrawing()
    {
        isDrawing = false;
        currentLine = null;
    }

    Vector3 GetMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z; // 调整z轴以匹配屏幕点
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    bool IsMousePositionInBounds(Vector3 mousePos)
    {
        // 获取SpriteRenderer的边界信息
        Bounds bounds = spriteRenderer.bounds;

        // 检查鼠标位置是否在SpriteRenderer的范围内
        return bounds.Contains(mousePos);
    }
    IEnumerator SetInformation()
    {
        yield return new WaitForSeconds(5 * 60f); // 等待指定的时间

        transform.parent.Find("information").gameObject.SetActive(true); // 激活目标游戏对象
        this.enabled = false;
    }
}
