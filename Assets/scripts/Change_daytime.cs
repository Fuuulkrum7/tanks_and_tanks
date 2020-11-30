using UnityEngine;

public class Change_daytime : MonoBehaviour
{
    Light light;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (light.intensity > 0.2f)
        {
            light.intensity -= 0.00001f;
        }
    }
}
