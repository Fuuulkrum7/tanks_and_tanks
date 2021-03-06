﻿using System.Collections;
using UnityEngine;

public class enemy_number_checker : MonoBehaviour
{
    int enemies = 0;

    public GameObject panel;
    public GameObject winner;

    public GameObject Load;
    next_scene script;
    Camera_changer script2;
    main script3;

    public GameObject cam;

    private void Start()
    {
        script = Load.GetComponent<next_scene>();
        script2 = cam.GetComponent<Camera_changer>();
        script3 = FindObjectOfType<main>();
        enemies = FindObjectsOfType<enemy_i>().Length;
    }

    public void recount()
    {
        enemies -= 1;

        if (enemies == 0)
            StartCoroutine(end_game());

    }

    public IEnumerator end_game()
    {
        yield return new WaitForSeconds(5f);
        panel.SetActive(false);
        winner.SetActive(true);
        script3.stop_move();

        if (script2.sight.enabled)
        {
            script2.turn_the_sight();
        }

        yield return new WaitForSeconds(3f);
        script.Start_loading();
    }
}
