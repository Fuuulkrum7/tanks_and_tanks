using UnityEngine;

public class On_player_die : MonoBehaviour
{
    Players_die script;
    public GameObject head;

    // Start is called before the first frame update
    void Start()
    {
        script = head.GetComponent<Players_die>();
    }

    public void on_shot()
    {
        script.on_hit();
    }

    public void heal(int i)
    {
        script.healing(i);
    }
}
