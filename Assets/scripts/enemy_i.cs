using UnityEngine;

public class enemy_i : MonoBehaviour
{
    public int enemy_hp = 3;
    public GameObject sparks;

    public GameObject head;
    public GameObject dylo;
    public GameObject player;
    public Rigidbody rb;
    public Rigidbody rb2;

    shot script;
    enemy_mover script2;
    enemy_number_checker counter;

    public AudioClip clip;

    public AudioSource move_sound;

    public GameObject smoke;

    public float ray_distance = 10f;

    public bool stop = false;

    public int far = 100;

    void Start()
    {
        rb = head.GetComponent<Rigidbody>();
        script = head.GetComponent<shot>();
        script2 = GetComponent<enemy_mover>();
        counter = FindObjectOfType<enemy_number_checker>();
    }

    void FixedUpdate()
    {
        if (enemy_hp > 0 && !stop)
        {
            Vector3 fromPosition = head.transform.position;
            Vector3 toPosition = player.transform.position;
            Vector3 direction = toPosition - fromPosition;

            if (Physics.Raycast(head.transform.position, direction, out RaycastHit hit) && hit.collider.gameObject.tag == "Player")
            {
                Quaternion rotation = Quaternion.LookRotation(direction);
                head.transform.rotation = Quaternion.RotateTowards(head.transform.rotation, rotation, 0.7f);
            }

            var fwd = dylo.transform.TransformDirection(Vector3.forward);
            Debug.DrawRay(dylo.transform.position, fwd);

            if (Physics.Raycast(dylo.transform.position, fwd, out RaycastHit hit2, 70))
            {
                if (!script.shooted && hit2.collider.gameObject.tag == "Player")
                    script.Fire(clip);
            }
        }
    }

    public void on_shot()
    {
        enemy_hp--;

        if (enemy_hp == 0)
        {
            counter.recount();
            script2.stop = true;
            script2.hp = 0;
            rb.isKinematic = false;
            rb2.isKinematic = true;

            move_sound.Stop();

            smoke.SetActive(false);
            rb.AddForce(head.transform.up * 90, ForceMode.Impulse);
            rb.AddForce(head.transform.right * 20, ForceMode.Impulse);
            sparks.SetActive(true);

        }
    }
}
