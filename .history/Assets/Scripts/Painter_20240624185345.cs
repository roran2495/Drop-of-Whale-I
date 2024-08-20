using UnityEngine;
using UnityEngine.UI;

public class Painter : MonoBehaviour
{
    public RenderTexture renderTexture; // 拖放到Inspector中的RenderTexture
    public Material drawMaterial; // 材料用于绘画
    public RawImage rawImage; // RawImage组件

    private Vector2? previousPosition = null; // 上一个绘制点的位置

    private void Update()
    {
        if (Input.GetMouseButton(0)) // 如果按下鼠标左键
        {
            // 将鼠标位置转换为 RawImage 的本地坐标
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rawImage.rectTransform,
                Input.mousePosition,
                null,
                out localPoint
            );

            // 计算纹理坐标
            Vector2 textureCoord = new Vector2(
                (localPoint.x - rawImage.rectTransform.rect.x) / rawImage.rectTransform.rect.width,
                (localPoint.y - rawImage.rectTransform.rect.y) / rawImage.rectTransform.rect.height
            );

            // 确保纹理坐标在0到1之间
            textureCoord = new Vector2(
                Mathf.Clamp01(textureCoord.x),
                Mathf.Clamp01(textureCoord.y)
            );

            if (previousPosition != null)
            {
                DrawLine(previousPosition.Value, textureCoord);
            }

            previousPosition = textureCoord;
        }
        else
        {
            previousPosition = null; // 重置上一个绘制点的位置
        }
    }

    private void DrawLine(Vector2 startPos, Vector2 endPos)
    {
        RenderTexture.active = renderTexture;
        GL.PushMatrix();
        GL.LoadPixelMatrix(0, renderTexture.width, renderTexture.height, 0);

        // 使用材质绘制线段
        drawMaterial.SetPass(0);
        GL.Begin(GL.LINES);
        GL.Color(Color.red);

        GL.Vertex3(startPos.x * renderTexture.width, startPos.y * renderTexture.height, 0);
        GL.Vertex3(endPos.x * renderTexture.width, endPos.y * renderTexture.height, 0);

        GL.End();

        GL.PopMatrix();
        RenderTexture.active = null;
    }
}
