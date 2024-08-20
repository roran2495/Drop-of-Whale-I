using UnityEngine;

public class Painter : MonoBehaviour
{
    public RenderTexture renderTexture; // 拖放到Inspector中的RenderTexture
    public Camera drawingCamera; // 摄像机用于绘画
    public Material drawMaterial; // 材料用于绘画

    private void Update()
    {
        if (Input.GetMouseButton(0)) // 如果按下鼠标左键
        {
            Vector3 mousePos = Input.mousePosition;
            RaycastHit hit;
            Ray ray = drawingCamera.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out hit))
            {
                Draw(hit.textureCoord);
            }
        }
    }

    private void Draw(Vector2 textureCoord)
    {
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
