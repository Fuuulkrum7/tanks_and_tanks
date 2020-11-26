using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class next_scene : MonoBehaviour
{
    public int Scene_id;

    public GameObject panel;
    public Image loading;
    public Text progress;

    public Dropdown maps;

    public GameObject Button;

    Light light;

    private void Start()
    {
        try
        {
            maps.onValueChanged.AddListener(delegate { Choose(maps); });
        }
        catch
        {

        }
        light = FindObjectOfType<Light>();
    }

    // Start is called before the first frame update
    public void Start_loading()
    {
        StartCoroutine(AsyncLoad());
    }

    void Choose(Dropdown droped)
    {
        Scene_id = droped.value + 1;
        print(1.0f / Scene_id - 0.2f * droped.value);
        int a = 1;

        if (Scene_id == 2)
            a = -1;

        StartCoroutine(turn_the_light(a));
    }

    IEnumerator AsyncLoad()
    {
        panel.SetActive(true);
        Button.SetActive(false);

        AsyncOperation operation = SceneManager.LoadSceneAsync(Scene_id);

        while (!operation.isDone)
        {
            float true_progress = operation.progress / 0.9f;

            loading.fillAmount = true_progress;
            progress.text = string.Format("{0:0}%", true_progress * 100);

            yield return null;
        }
        yield return null;
    }

    IEnumerator turn_the_light(int j)
    {
        for (int i = 0; i < 71; i++)
        {
            light.intensity += j * 0.01f;
            yield return null;
        }
    }
}
