using UnityEngine;

public class MoveAlongPath : MonoBehaviour
{
    public Transform[] pathPoints; // 路径点
    public DeskFeatureController deskFeatureController;
    public float speed = 1.0f; // 移动速度
    private int currentPointIndex = 0; // 当前路径点索引
    private float t = 0f; // 插值参数

    void OnEnable()
    {
        // 每次对象激活时初始化参数
        currentPointIndex = 0;
        t = 0f;
    }

    void Update()
    {
        if (pathPoints.Length == 0)  // 如果没有路径点，退出
        {
            this.gameObject.SetActive(false);
            
        }

        // 获取当前和下一个路径点
        Transform startPoint = pathPoints[currentPointIndex];
        Transform endPoint = pathPoints[(currentPointIndex + 1) % pathPoints.Length];

        // 使用插值方法平滑移动
        t += Time.deltaTime * speed / Vector3.Distance(startPoint.position, endPoint.position);
        transform.position = Vector3.Lerp(startPoint.position, endPoint.position, t);

        // 检查是否到达终点
        if (t >= 1f)
        {
            t = 0f;
            currentPointIndex = (currentPointIndex + 1) % pathPoints.Length; // 移动到下一个路径点
        }
    }
}
