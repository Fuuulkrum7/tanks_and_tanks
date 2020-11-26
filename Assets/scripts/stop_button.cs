using System.Collections.Generic;
using UnityEngine;

public class stop_button : MonoBehaviour
{
    enemy_i[] script;
    enemy_mover[] script2;
    player_shot script3;

    public GameObject control;
    public GameObject stop;

    private bool is_stoped = false;

    void Start()
    {
        script = FindObjectsOfType<enemy_i>();
        script2 = FindObjectsOfType<enemy_mover>();
        script3 = FindObjectOfType<player_shot>();
    }

    public void Stop()
    {
        is_stoped = !is_stoped;


        if (is_stoped)
        {
            control.SetActive(false);
            stop.SetActive(true);

            foreach(enemy_i i in script)
            {
                i.stop = true;
            }

            foreach (enemy_mover i in script2)
            {
                i.stop = true;
            }
        }
        else
        {
            script3.stop = true;
            control.SetActive(true);
            stop.SetActive(false);
            
            foreach (enemy_i i in script)
            {
                i.stop = false;
            }

            foreach (enemy_mover i in script2)
            {
                i.stop = false;
            }
        }
    }
}
