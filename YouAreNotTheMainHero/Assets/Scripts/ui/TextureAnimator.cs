using UnityEngine;

public class TextureAnimator : MonoBehaviour
{
    public Vector2 Offset = new Vector2(0f, 0.2f);
    public float Threshold = 0.1f;
    public int FrameNumber = 5;

    private Renderer renderer;

    private float time;
    private int direction = 1;
    private int curFrame;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }
    
    void Update()
    {
        time += Time.deltaTime;

        if (time > Threshold)
        {
            Animate();
            time = 0f;
        }
    }

    private void Animate()
    {
        renderer.material.mainTextureOffset = Offset * GetCurrentFrame();
    }

    private int GetCurrentFrame()
    {
        curFrame += direction;

        if (curFrame < 0)
        {
            curFrame = 1;
            direction = 1;
        }
        else if (curFrame >= FrameNumber)
        {
            curFrame = FrameNumber - 2;
            direction = -1;
        }

        return curFrame;
    }
}
