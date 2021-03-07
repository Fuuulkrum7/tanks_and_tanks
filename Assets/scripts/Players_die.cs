using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Players_die : MonoBehaviour
{
    public Image heart;
    public Text hp_visual;

    public GameObject control;
    public GameObject winner;
    public Text win;

    public GameObject cam;

    int hp = 10;

    public GameObject other;

    public GameObject sparks;

    next_scene script;
    Camera_changer script2;
    main script3;
    move wheels;

    public GameObject Load;

    private void Start()
    {
        script = Load.GetComponent<next_scene>();
        script2 = cam.GetComponent<Camera_changer>();
        script3 = FindObjectOfType<main>();
        wheels = FindObjectOfType<move>();
    }

    public void healing(int i)
    {
        hp += i;

        if (hp > 10)
            hp = 10;

        heart.fillAmount = 0.1f * hp;
        hp_visual.text = $"{hp}";
    }

    public void on_hit()
    {
        healing(-1);

        if (hp == 0)
        {
            foreach (AxleInfo axleInfo in wheels.axleInfos)
            {
                axleInfo.leftWheel.motorTorque = 0;
                axleInfo.rightWheel.motorTorque = 0;
                axleInfo.leftWheel.brakeTorque = 5;
                axleInfo.rightWheel.brakeTorque = 5;
            }
            control.SetActive(false);
            other.SetActive(false);
            sparks.SetActive(true);
            win.text = "Поражение";
            win.color = new Color(1f, 0.2196078f, 0f);

            if (script2.sight.enabled)
            {
                script2.turn_the_sight();
            }

            script3.stop_move();
            
            StartCoroutine(end_my_game());
        }
    }

    IEnumerator end_my_game()
    {
        yield return new WaitForSeconds(3f);
        winner.SetActive(true);

        yield return new WaitForSeconds(3f);
        script.Start_loading();
    }
}
