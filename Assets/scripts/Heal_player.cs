using UnityEngine;

public class Heal_player : MonoBehaviour
{
    On_player_die player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            try
            {
                player = other.gameObject.GetComponent<On_player_die>();
                player.heal(1);
                Destroy(gameObject);
            }
            catch
            {

            }
        }
    }
}
