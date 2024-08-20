using UnityEngine;

public class DrawingController : MonoBehaviour
{
    public Color drawColor = Color.black;   // 绘制的颜色
    public float lineWidth = 0.1f;          // 线条宽度

    private bool isDrawing = false;         // 是否正在绘制
    private LineRenderer currentLine;       // 当前的线条组件

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
        currentLine = lineObject.AddComponent<LineRenderer>();

        // 设置线条参数
        currentLine.startWidth = lineWidth;
        currentLine.endWidth = lineWidth;
        currentLine.material = new Material(Shader.Find("Sprites/Default")); // 使用默认的精灵Shader
        currentLine.startColor = drawColor;
        currentLine.endColor = drawColor;

        // 添加第一个点
        Vector3 mousePos = GetMousePosition();
        currentLine.positionCount = 1;
        currentLine.SetPosition(0, mousePos);
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