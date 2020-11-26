using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fps_counter : MonoBehaviour
{
    public Text texts;

    private int frame_range = 60;
    private int[] fps_Buffer;
    private int fps_Buffer_Index;

    public int FPS { get; private set; }
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (fps_Buffer == null || frame_range != fps_Buffer.Length)
        {
            Buffer_init();
        }
        Update_Buffer();
        Calculate_fps();

        if (FPS < 30)
        {
            texts.color = new Color(1f, 0.2196078f, 0f); 
        }
        else if (FPS < 60)
        {
            texts.color = new Color(1f, 0.9499881f, 0f);
        }
        else
        {
            texts.color = new Color(0f, 1f, 0.1882f);
        }

        texts.text = $"FPS: {FPS}";
    }

    private void Buffer_init()
    {
        if (frame_range <= 0)
        {
            frame_range = 1;
        }

        fps_Buffer = new int[frame_range];
        fps_Buffer_Index = 0;
    }

    private void Update_Buffer()
    {
        fps_Buffer[fps_Buffer_Index++] = Mathf.Clamp((int)(1f / Time.unscaledDeltaTime), 0, 100);

        if (fps_Buffer_Index >= frame_range)
        {
            fps_Buffer_Index = 0;
        }
    }

    private void Calculate_fps()
    {
        int sum = 0;
        foreach(int i in fps_Buffer)
        {
            sum += i;
        }

        FPS = sum / frame_range;
    }
}
