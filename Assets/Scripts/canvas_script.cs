using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class canvas_script : MonoBehaviour {

    public GameObject title;
    public Image progress_bar, progress_bar_back;


    private AudioSource start_sound;
    private Text start;
    private IEnumerator coroutine;

    private float time_count;
    private float temp = 5f;

    // Use this for initialization
    void Start () {
        start_sound = GetComponent<AudioSource>();
        start = GetComponent<Text>();
        start.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {

        coroutine = appears(3f);
        StartCoroutine(coroutine);

        if (Input.anyKey)
        {
            AnalyticsEvent.GameStart();
            start_sound.Play();

            coroutine = start_effect();
            StartCoroutine(coroutine);

            title.SetActive(false);
            progress_bar_back.enabled = true;
            progress_bar.enabled = true;


            coroutine = LoadAsyncScene();
            StartCoroutine(coroutine);
        }

    }

    IEnumerator start_effect()
    {
        start.enabled = false;
        while (time_count <= temp)
        {
            time_count += Time.deltaTime;
            start.color = Color.gray;
            yield return new WaitForSeconds(.1f);
            start.color = Color.white;
            yield return new WaitForSeconds(.1f);
        }
    }

    IEnumerator LoadAsyncScene()
    {
        // (!asyncLoad.isDone); AsyncOperation asyncLoad = 

        while (time_count <= temp) {
            time_count += Time.deltaTime;
            progress_bar.fillAmount = time_count / temp;
            yield return null;
        }
        SceneManager.LoadSceneAsync("mein");
    }

    IEnumerator appears(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        start.enabled = true;
    }
}
