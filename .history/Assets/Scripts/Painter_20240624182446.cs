using UnityEngine;
using UnityEngine.UI;

public class Painter : MonoBehaviour
{
    public RenderTexture renderTexture; // 拖放到Inspector中的RenderTexture
    public Material drawMaterial; // 材料用于绘画
    public RawImage rawImage; // RawImage组件

    private void Update()
    {
        if (Input.GetMouseButton(0)) // 如果按下鼠标左键
        {
            Debug.Log("2");

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

            Debug.Log("1");
            Draw(textureCoord);
        }
    }

    private void Draw(Vector2 textureCoord)
    {
        Debug.Log("3");
        RenderTexture.active = renderTexture;
        GL.PushMatrix();
        GL.LoadPixelMatrix(0, renderTexture.width, renderTexture.height, 0);

        // 使用材质绘制点
        drawMaterial.SetPass(0);
        GL.Begin(GL.QUADS);
        GL.Color(Color.red); // 颜色
        GL.Vertex3(textureCoord.x * renderTexture.width - 5, textureCoord.y * renderTexture.height - 5, 0);
        GL.Vertex3(textureCoord.x * renderTexture.width + 5, textureCoord.y * renderTexture.height - 5, 0);
        GL.Vertex3(textureCoord.x * renderTexture.width + 5, textureCoord.y * renderTexture.height + 5, 0);
        GL.Vertex3(textureCoord.x * renderTexture.width - 5, textureCoord.y * renderTexture.height + 5, 0);
        GL.End();

        GL.PopMatrix();
        RenderTexture.active = null;
    }
}
