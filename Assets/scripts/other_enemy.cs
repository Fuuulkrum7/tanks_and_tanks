using UnityEngine;

public class other_enemy : MonoBehaviour
{
    enemy_i script;
    public GameObject body;

    // Start is called before the first frame update
    void Start()
    {
        script = body.GetComponent<enemy_i>();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            script.on_shot(col);
        }
    }
}
