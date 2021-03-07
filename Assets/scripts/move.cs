using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}

public class move : MonoBehaviour
{
    public Joystick joystick;
    public GameObject t34;
    public GameObject move_GO1;
    public GameObject move_GO2;

    public ParticleSystem move_1;
    public ParticleSystem move_2;

    public AudioSource move_sound;

    public AudioSource stait;

    bool done = false;

    float[] need_to_rotate = new float[2];

    bool check = false;

    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;


    private void move_tank()
    {
        float motor = maxMotorTorque * joystick.Vertical;
        float steering = maxSteeringAngle * joystick.Horizontal;

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x_rot = t34.transform.localEulerAngles.x;
        float z_rot = t34.transform.localEulerAngles.z;

        if ((Mathf.Abs(x_rot) < 330 && Mathf.Abs(x_rot) > 30) || (Mathf.Abs(z_rot) < 330 && Mathf.Abs(z_rot) > 30))
        {
            need_to_rotate[0] = x_rot;
            need_to_rotate[1] = z_rot;
        }

        if (need_to_rotate[0] != 0)
        {
            if (need_to_rotate[0] > 1f)
            {
                t34.transform.Rotate(new Vector3(1f, 0, 0));
                need_to_rotate[0] -= 1f;
            }

            else if (need_to_rotate[1] < -1f)
            {
                t34.transform.Rotate(new Vector3(-1f, 0, 0));
                need_to_rotate[0] += 1f;
            }

            else
            {
                t34.transform.Rotate(new Vector3(need_to_rotate[0], 0, 0));
                need_to_rotate[0] = 0;
            }
        }

        if (need_to_rotate[1] != 0)
        {
            if (need_to_rotate[1] > 1f)
            {
                t34.transform.Rotate(new Vector3(0, 0, 1f));
                need_to_rotate[1] -= 1f;
            }

            else if (need_to_rotate[1] < -1f)
            {
                t34.transform.Rotate(new Vector3(0, 0, -1f));
                need_to_rotate[1] += 1f;
            }

            else
            {
                t34.transform.Rotate(new Vector3(0, 0, need_to_rotate[1]));
                need_to_rotate[1] = 0;
            }
        }

        if (joystick.Vertical != 0 || joystick.Horizontal != 0)
        {
            if (!done)
            {
                check = false;
                done = true;
                move_GO1.SetActive(true);
                move_GO2.SetActive(true);
                StartCoroutine(staing(false));
            }
            var main = move_1.main;
            var main2 = move_2.main;

            if (Math.Abs(joystick.Vertical) + 0.1f > Math.Abs(joystick.Horizontal))
            {
                main.startSpeed = 4f * joystick.Vertical;
                main2.startSpeed = 4f * joystick.Vertical;
            }
            else
            {
                main.startSpeed = -4f * joystick.Horizontal;
                main2.startSpeed = 4f * joystick.Horizontal;
            }
            if (check)
                move_sound.volume = (Math.Abs(joystick.Horizontal) + Math.Abs(joystick.Vertical)) / 2 + 0.2f;
        }
        else if (done)
        {
            done = false;
            move_GO1.SetActive(false);
            move_GO2.SetActive(false);
            StartCoroutine(staing(true));
        }


        move_tank();
    }


    IEnumerator staing(bool c)
    {
        if (c)
        {
            while (move_sound.volume > 0.1f)
            {
                move_sound.volume -= 0.1f;
                yield return new WaitForSeconds(0.02f);
            }
            move_sound.volume = 0.1f;
            stait.enabled = true;
            for (int i = 1; i < 11; i++)
            {
                stait.volume = 0.01f * i;
                yield return new WaitForSeconds(0.04f);
            }
        }

        else
        {
            stait.enabled = false;
            move_sound.enabled = true;
            move_sound.volume = 0f;
            while (move_sound.volume < 0.2f)
            {
                move_sound.volume += 0.025f;
                yield return new WaitForSeconds(0.04f);
            }

            check = true;
        }
    }
}
