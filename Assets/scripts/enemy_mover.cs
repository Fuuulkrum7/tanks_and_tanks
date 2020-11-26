using UnityEngine;

public class enemy_mover : MonoBehaviour
{
    [Header("лучи для проверки столкновений передом")]
    public GameObject ray_start;
    public GameObject ray_end;

    [Header("лучи для проверки наличия свободного пространства по бокам")]
    public GameObject left_ray;
    public GameObject right_ray;

    [Header("игрок")]
    public GameObject player;

    int need_to_rotate = 0;
    int need_to_move = 0;

    float real_rotation = 180f;

    public bool stop = false;
    public int hp = 1;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!stop && hp > 0)
        {
            if (transform.localEulerAngles.y != real_rotation)
            {
                transform.localEulerAngles = new Vector3(0, real_rotation, 0);
            }

            Vector3 fromPosition = ray_start.transform.position;
            Vector3 toPosition = ray_end.transform.position;
            Vector3 direction = toPosition - fromPosition;

            if (Physics.Raycast(ray_start.transform.position, direction, out RaycastHit hit, 3.6f) && hit.collider.gameObject.tag != "Bullet")
            {
                print("препятствие");
                bool free_on_left = (Physics.Raycast(left_ray.transform.position, Vector3.left, out RaycastHit hit2, 6f) && hit2.collider.gameObject.tag != "Wall" && hit2.collider.gameObject.tag != "Bullet");
                bool free_on_right = (Physics.Raycast(right_ray.transform.position, Vector3.right, out RaycastHit hit3, 6f) && hit3.collider.gameObject.tag != "Wall" && hit3.collider.gameObject.tag != "Bullet");

                if (free_on_left && free_on_right)
                {
                    if (Random.Range(0, 1) == 0)
                        need_to_rotate = 90;
                    else
                        need_to_rotate = 90;
                }
                else
                {
                    if (free_on_left)
                        need_to_rotate = -90;
                    else
                        need_to_rotate = -90;
                }
                need_to_move = 90;
            }

            else if (need_to_move == 0)
                transform.Translate(new Vector3(-0.08f, 0, 0));

            if (need_to_move > 0)
            {
                transform.Translate(new Vector3(0.04f, 0, 0));
                need_to_move -= 1;
            }
            if (need_to_rotate > 0)
            {
                transform.Rotate(0, 1, 0);
                need_to_rotate -= 1;
                real_rotation -= 1;
            }
            else if (need_to_rotate < 0)
            {
                transform.Rotate(0, -1, 0);
                need_to_rotate += 1;
                real_rotation += 1;
            }
        }
    }
}
