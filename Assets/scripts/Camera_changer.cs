using UnityEngine;

public class Camera_changer : MonoBehaviour
{
    // главная камера
    private Camera main_camera;
    // камера-прицел
    public Camera sight;
    // скрипт с поворотом башни
    main script;

    player_shot script3;

    void Start()
    {
        // получаем данные о камере
        main_camera = Camera.main;
        main_camera = GetComponent<Camera>();
        // получаем доступ к скрипту с поворотом башни
        script = FindObjectOfType<main>();

        script3 = FindObjectOfType<player_shot>();
    }

    public void turn_the_sight()
    {
        // если прицел был включен
        if (sight.enabled)
        {
            // меняем значение скорости поворота головы на стандартную
            script._speed = 0.2f;
        }
        else
        {
            // иначе уменьшаем ее
            script._speed = 0.05f;
        }

        // выключаем/включаем прицел и главную камеру
        main_camera.enabled = !main_camera.enabled;
        sight.enabled = !sight.enabled;

        script3.on_sight = sight.enabled;
    }
}
