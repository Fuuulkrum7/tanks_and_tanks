using System;
using UnityEngine;

public class main : MonoBehaviour
{
    public enum RotationAxes
    {
        X_Y,
        x,
        y
    }

    public RotationAxes _axes = RotationAxes.X_Y;
    public float _speed = 0.2f;

    public bool check = true;
    float y_move = 0;

    float max_y = 10f;
    float min_y = -10f;

    public GameObject head;
    public GameObject cam;
    public GameObject dylo;

    public int index = 0;

    float pointer_x = 0;
    float pointer_y = 0;

    // на сколько градусов нужно повернуть башню
    public float need_to_tern = 0;

    // коснулись ли экрана
    bool touched = false;

    // сработала ли ошибка
    bool error = false;

    int fingers = 0;

    // поворот башни и камеры
    void FixedUpdate()
    {
        pointer_x = 0;
        if (_axes == RotationAxes.X_Y && check)
        {
            try
            {
                if (Input.GetTouch(index).phase == TouchPhase.Moved)
                {
                    print(index);
                    pointer_x = Input.touches[index].deltaPosition.x;
                    pointer_y = Input.touches[index].deltaPosition.y;
                    y_move -= pointer_y * _speed;
                    y_move = Mathf.Clamp(y_move, min_y, max_y);
                    float a = pointer_x * _speed;
                    var y_mover = cam.transform.localEulerAngles.y + a;
                    var dylo_rot = dylo.transform.localEulerAngles.x;

                    cam.transform.Rotate(0, a, 0);
                    cam.transform.localEulerAngles = new Vector3(0, y_mover, y_move);

                    dylo.transform.Rotate(new Vector3(y_move - dylo_rot, 0, 0));
                }
            }
            catch (ArgumentException)
            {
                index--;
                print("error with index");
                if (index < 0)
                    index = 0;
                error = true;
            }
        }
        if (Math.Abs(pointer_x) < 5 && need_to_tern == 0)
        {
            head.transform.Rotate(0, pointer_x * _speed, 0);
        }
        else if (Math.Abs(need_to_tern) < 1 && need_to_tern != 0)
        {
            head.transform.Rotate(new Vector3(0, cam.transform.localEulerAngles.y - 90 - head.transform.localEulerAngles.y, 0));
            need_to_tern = 0f;
        }
        else if ((!check && (need_to_tern != 0 || pointer_x != 0)) || check)
        {
            need_to_tern += pointer_x * _speed;
            if (Math.Abs(need_to_tern) > 180)
            {
                if (need_to_tern > 0)
                    need_to_tern = (360f - need_to_tern) * -1;
                else
                    need_to_tern = 360 + need_to_tern;
            }
            if (need_to_tern > 0)
            {
                head.transform.Rotate(0, 1, 0);
                need_to_tern -= 1f;
            }
            else
            {
                head.transform.Rotate(0, -1, 0);
                need_to_tern += 1f;
            }
        }
    }

    public void move_it()
    {
        fingers++;
        check = true;
        if (!touched)
        {
            index = Input.touchCount - 1;
            touched = true;
        }
    }

    public void stop_move()
    {
        check = false;
        j_unpressed();
    }

    public void safe()  // 
    {
        fingers++;
        if (error)
        {
            print("ayg");
            index++;
            error = false;
        }
    }

    public void j_unpressed()  //
    {
        fingers--;
        if (fingers < 1)
            touched = false;
    }
}
