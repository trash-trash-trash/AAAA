using UnityEngine;
using UnityEngine.UI;

public class Mirror : MonoBehaviour
{
    public Camera camera;
    public RenderTexture renderTexture;
    public RawImage rawImage;

    void Start()
    {
        renderTexture = new RenderTexture(512, 512, 16);
        renderTexture.graphicsFormat = UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm;
        renderTexture.Create();
        camera.targetTexture = renderTexture;
        rawImage.texture = renderTexture;
    }
}
