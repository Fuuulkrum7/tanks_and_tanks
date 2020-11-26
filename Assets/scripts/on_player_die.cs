using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class on_player_die : MonoBehaviour
{
    Players_die script;

    // Start is called before the first frame update
    void Start()
    {
        script = FindObjectOfType<Players_die>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
            script.on_hit();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "healing")
        {
            Destroy(other.gameObject);
            script.healing(1);
        }
    }
}
