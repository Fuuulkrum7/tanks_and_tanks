using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class player_shot : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bullet_spawn;

    public Image reloting;

    public Transform strike;
    public GameObject cam;

    public Camera sight;

    public bool shooted = false;

    public ParticleSystem fire;

    public bool on_sight = false;

    bullet_script script;
    int i = 101;
    public bool stop = false;

    private void Update()
    {
        if (stop && i != 101)
        {
            stop = false;
            StartCoroutine(sh());
        }

        Ray ray;
        if (!on_sight)
            ray = Camera.main.ScreenPointToRay(strike.position);
        else
            ray = sight.ScreenPointToRay(strike.position);

        Debug.DrawLine(cam.transform.position, ray.direction);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            bullet_spawn.transform.LookAt(hit.point);
        }
    }

    private void LateUpdate()
    {
        bullet_spawn.transform.localEulerAngles = new Vector3(bullet_spawn.transform.localEulerAngles.x, 0, 0);
    }

    public void Fire(AudioClip clip)
    {
        if (shooted)
            return;
        shooted = true;
        GetComponent<AudioSource>().PlayOneShot(clip);
        fire.Play();

        Vector3 spawn = bullet_spawn.transform.position;
        Quaternion spawn_rot = bullet_spawn.transform.rotation;

        GameObject bul = Instantiate(bullet, spawn, spawn_rot);

        script = FindObjectOfType<bullet_script>();
        script.done = false;

        Rigidbody Run = bul.GetComponent<Rigidbody>();

        Run.AddForce(bul.transform.forward * 170, ForceMode.Impulse);

        try
        {
            Destroy(bul, 1);
        }
        catch
        {

        }
        i = 0;
        StartCoroutine(sh());
    }

    IEnumerator sh()
    {
        while (i < 101)
        {
            reloting.fillAmount = i * 0.01f;

            i++;

            yield return new WaitForSeconds(0.009f);
        }

        shooted = false;
    }
}

