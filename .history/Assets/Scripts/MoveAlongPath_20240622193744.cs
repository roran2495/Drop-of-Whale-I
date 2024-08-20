using UnityEngine;

public class MoveAlongPath : MonoBehaviour
{
    public LineRenderer lineRenderer; // LineRenderer 组件
    public GameObject cupPigments;
    public GameObject cupEgg;
    public GameObject cupResin;
    public DeskFeatureController deskFeatureController;
    public float speed = 1.0f; // 移动速度
    private Vector3[] pathPoints; // 路径点数组
    private int currentPointIndex = 0; // 当前路径点索引
    private float t = 0f; // 插值参数

    void Start()
    {
        // 获取 LineRenderer 的点
        int numPoints = lineRenderer.positionCount;
        pathPoints = new Vector3[numPoints];
        for (int i = 0; i < numPoints; i++)
        {
            pathPoints[i] = lineRenderer.GetPosition(i);
        }
    }

    void OnEnable()
    {
        // 每次对象激活时初始化参数
        currentPointIndex = 0;
        t = 0f;
        if (pathPoints != null && pathPoints.Length > 0)
        {
            transform.position = pathPoints[0]; // 将对象位置设置到路径起点
        }
    }

    void Update()
    {
        if (pathPoints == null || pathPoints.Length == 0)  // 如果没有路径点，退出
        {
            this.gameObject.SetActive(false);
            return;
        }

        // 获取当前和下一个路径点
        Vector3 startPoint = pathPoints[currentPointIndex];
        Vector3 endPoint = pathPoints[currentPointIndex + 1];

        // 使用插值方法平滑移动
        t += Time.deltaTime * speed / Vector3.Distance(startPoint, endPoint);
        transform.position = Vector3.Lerp(startPoint, endPoint, t);

        // 检查是否到达终点
        if (t >= 1f)
        {
            t = 0f;
            currentPointIndex++;
            
            // 检查是否到达最后一个点
            if (currentPointIndex >= pathPoints.Length - 1)
            {
                // 到达最后一个点，执行相应的操作
                this.gameObject.SetActive(false);
                deskFeatureController.SetColor(cupPigments.GetComponent<SpriteRenderer>().color);
                deskFeatureController.
                cupEgg.SetActive(false);
                cupPigments.SetActive(false);
                cupResin.SetActive(false);
            }
        }
    }
}
