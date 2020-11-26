using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class shot : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bullet_spawn;

    public bool shooted = false;

    public ParticleSystem fire;
    
    bullet_script script;

    public void Fire(AudioClip clip)
    {
        if (shooted)
            return;
        shooted = true;
        GetComponent<AudioSource>().PlayOneShot(clip);

        fire.Play();

        Vector3 spawn = bullet_spawn.transform.position;
        Quaternion spawn_rot = bullet_spawn.transform.rotation;

        spawn.z += Random.Range(-2f, 2f);
        spawn.y += Random.Range(-2f, 2f);

        GameObject bul = Instantiate(bullet, spawn, spawn_rot);

        script = FindObjectOfType<bullet_script>();
        script.done = false;

        Rigidbody Run = bul.GetComponent<Rigidbody>();

        Run.AddForce(bul.transform.forward * 180, ForceMode.Impulse);

        try
        {
            Destroy(bul, 1);
        }
        catch
        {

        }
        StartCoroutine(sh());
    }

    IEnumerator sh()
    {
        yield return new WaitForSeconds(2f);

        shooted = false;
    }
}
