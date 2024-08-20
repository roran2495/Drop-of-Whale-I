using UnityEngine;
using UnityEngine.EventSystems;

public class DrawingController : MonoBehaviour
{
    public RectTransform drawingArea;  // 绘画区域的RectTransform
    public GameObject drawingPrefab;   // 绘画点的预制体
    public float pointSize = 0.1f;     // 绘画点的大小

    private GameObject currentDrawing; // 当前绘画点实例

    void Update()
    {
        // 检测鼠标按下或触摸移动
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector2 localPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(drawingArea, Input.mousePosition, null, out localPosition);

            if (IsPointInsideRect(localPosition))
            {
                if (currentDrawing == null)
                {
                    currentDrawing = Instantiate(drawingPrefab, drawingArea);
                    ResizePoint(currentDrawing, pointSize);
                }

                currentDrawing.transform.localPosition = localPosition;
            }
        }
        else
        {
            Destroy(currentDrawing);
            currentDrawing = null;
        }
    }

    // 检查点是否在矩形区域内
    bool IsPointInsideRect(Vector2 localPosition)
    {
        Rect rect = new Rect(-drawingArea.rect.width / 2, -drawingArea.rect.height / 2, drawingArea.rect.width, drawingArea.rect.height);
        return rect.Contains(localPosition);
    }

    // 调整绘画点的大小
    void ResizePoint(GameObject point, float size)
    {
        var rectTransform = point.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(size, size);
    }
}
