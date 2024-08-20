using UnityEngine;
using UnityEngine.EventSystems;

public class DrawingLogic : MonoBehaviour
{
    public RectTransform drawingArea;  // 绘画区域的RectTransform
    public LineRenderer lineRenderer;  // LineRenderer组件
    public float lineWidth = 0.1f;     // 线条宽度

    private bool isDrawing = false;    // 是否正在绘画

    void Start()
    {
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
    }

    void Update()
    {
        // 检测鼠标按下或触摸移动
        if (Input.GetMouseButtonDown(0) && IsPointerInsideRect())
        {
            isDrawing = true;
            lineRenderer.positionCount = 1;
            lineRenderer.SetPosition(0, GetMousePositionInCanvas());
        }
        else if (Input.GetMouseButton(0) && isDrawing)
        {
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, GetMousePositionInCanvas());
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDrawing = false;
        }
    }

    // 检查鼠标位置是否在矩形区域内
    bool IsPointerInsideRect()
    {
        return RectTransformUtility.RectangleContainsScreenPoint(drawingArea, Input.mousePosition);
    }

    // 获取鼠标在Canvas中的位置
    Vector3 GetMousePositionInCanvas()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -drawingArea.position.z - Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
