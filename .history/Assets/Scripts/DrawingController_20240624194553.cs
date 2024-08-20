using UnityEngine;

public class DrawingBoard : MonoBehaviour
{
    public Color drawColor = Color.black;   // 绘制的颜色
    public float lineWidth = 0.1f;          // 线条宽度

    private bool isDrawing = false;         // 是否正在绘制
    private Transform drawingParent;        // 线条的父对象
    private LineRenderer currentLine;       // 当前的线条组件
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        drawingParent = new GameObject("Drawing").transform;  // 创建一个空的GameObject作为线条的父对象
        drawingParent.parent = transform;  // 设置父对象为当前游戏对象
        spriteRenderer = 
    }

    void Update()
    {
        // 检测鼠标按下或触摸移动
        if (Input.GetMouseButtonDown(0))
        {
            StartDrawing();
        }
        else if (Input.GetMouseButton(0) && isDrawing)
        {
            ContinueDrawing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopDrawing();
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
        currentLine.sortingLayerName = "Feature"; // 设置sorting layer为"Feature"
        currentLine.sortingOrder = 1; // 设置order in layer为1

        // 添加第一个点，并确保点在SpriteRenderer的范围内
        Vector3 mousePos = GetMousePosition();
        Vector3 clampedPos = ClampPositionToSpriteBounds(mousePos);
        currentLine.positionCount = 1;
        currentLine.SetPosition(0, clampedPos);
    }

    Vector3 ClampPositionToSpriteBounds(Vector3 position)
    {
        // 获取SpriteRenderer的边界信息
        Bounds bounds = spriteRenderer.bounds;

        // 将position限制在SpriteRenderer的范围内
        float clampedX = Mathf.Clamp(position.x, bounds.min.x, bounds.max.x);
        float clampedY = Mathf.Clamp(position.y, bounds.min.y, bounds.max.y);
        float clampedZ = position.z; // 不改变z轴

        return new Vector3(clampedX, clampedY, clampedZ);
    }


    void ContinueDrawing()
    {
        Vector3 mousePos = GetMousePosition();

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
}
