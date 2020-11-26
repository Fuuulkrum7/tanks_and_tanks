using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_daytime : MonoBehaviour
{
    Light light;
    public Light players_light;

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
            light.intensity -= 0.00005f;
            players_light.intensity += 0.0005f;
        }
    }
}
