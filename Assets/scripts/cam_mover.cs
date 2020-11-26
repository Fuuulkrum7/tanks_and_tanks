using UnityEngine;

public class cam_mover : MonoBehaviour
{
    public GameObject head;
    public GameObject check_ray;

    int cam_move = 0;

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = transform.position - head.transform.position;

        float distance = Mathf.Pow(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2), 0.5f);

        Vector3 direction2 = check_ray.transform.position - transform.position;

        bool collide = Physics.Raycast(transform.position, direction2, out RaycastHit second_hit, 1f);
        bool collide2 = Physics.Raycast(head.transform.position, direction, out RaycastHit hit, distance);

        if (collide)
        {
            if (second_hit.collider.gameObject.tag != "Player" && second_hit.collider.gameObject.tag != "Bullet")
            {
                if (second_hit.distance < 0.9f)
                {
                    transform.Translate(new Vector3(0, 0, 0.1f));
                    check_ray.transform.Translate(new Vector3(0, 0, 0.05f));
                    cam_move++;
                }
            }
        }

        if (collide2)
        {
            print(hit.collider.gameObject.tag);
            if (hit.collider.gameObject.tag != "Player" && hit.collider.gameObject.tag != "Bullet")
            {
                int x = Mathf.Abs(Mathf.RoundToInt(transform.position.x - hit.point.x));
                transform.Translate(new Vector3(0, 0, x));
                check_ray.transform.Translate(new Vector3(0, 0, (float)x / 2));
                cam_move += x * 10;
            }
        }

        if (cam_move != 0 && !collide && (!collide2 || hit.collider.gameObject.tag == "Player"))
        {
            transform.Translate(new Vector3(0, 0, -0.1f));
            check_ray.transform.Translate(new Vector3(0, 0, -0.05f));
            cam_move--;
        }
    }
}
