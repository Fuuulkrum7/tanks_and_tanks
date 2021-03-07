using UnityEngine;

public class bullet_script : MonoBehaviour
{
    // сама пуля
    public GameObject bullet;
    // эффект попадания
    public GameObject shot_effect;
    // проверка на касание
    public bool done = false;

    void OnCollisionEnter(Collision other)
    {
        // место контакта пули и предмета, куда она попала
        ContactPoint contact = other.contacts[0];
        // получаем координаты места попадания
        Vector3 pos = contact.point;

        // добавляем эффект попадания
        GameObject sh = Instantiate(shot_effect, pos, Quaternion.identity);
        // уничтожаем эффект попадания с задержкой 1.5 сек
        Destroy(sh, 1.5f);

        // если попали в стену которая красная
        if (other.gameObject.tag == "Wall")
        {
            // получаем угол попадания 
            Vector3 cont = other.contacts[0].point;

            float x = cont.x - gameObject.transform.position.x;
            float z = cont.z - gameObject.transform.position.z;

            float a = x / Mathf.Pow((Mathf.Pow(x, 2) + Mathf.Pow(z, 2)), 0.5f);

            a = Mathf.Abs(Mathf.Asin(a) / Mathf.PI * 180);

            
            if (Mathf.Round((Mathf.Abs(other.gameObject.transform.position.x - cont.x)) * 10) / 10 % 1.5f == 0)
                a = 90 - a;

            //print(a);

            // если угол больше 30 или меньше 70 и пуля не рикошетила до этого
            if (a < 45f && !done)
            {
                // уничтожаем стену, куда попали
                Destroy(other.gameObject);

                // и пулю
                Destroy(gameObject);
                // пуля отрикошетила
                done = true;
            }

            // во всех противных случаях рикошет
        }

        else if (other.gameObject.tag == "Player")
        {
            On_player_die player = other.gameObject.GetComponent<On_player_die>();
            player.on_shot();
        }

        else if (other.gameObject.tag == "enemy")
        {
            enemy_i enemy = other.gameObject.GetComponent<enemy_i>();
            if (enemy != null)
            {
                enemy.on_shot();
            }
            else
            {
                Redirect_hit enemy_part = other.gameObject.GetComponent<Redirect_hit>();
                enemy_part.on_hit();
            }
        }
        // иначе просто уничтожаем пулю
        if (other.gameObject.tag != "Wall" || done)
            Destroy(gameObject);
    }
}
