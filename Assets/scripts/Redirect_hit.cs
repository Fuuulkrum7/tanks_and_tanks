using UnityEngine;

public class Redirect_hit : MonoBehaviour
{
    enemy_i enemy;
    public GameObject m24;
    // Start is called before the first frame update
    void Start()
    {
        enemy = m24.GetComponent<enemy_i>();
    }

    public void on_hit()
    {
        enemy.on_shot();
    }
}
