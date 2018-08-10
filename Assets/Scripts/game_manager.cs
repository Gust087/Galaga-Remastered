using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class game_manager : MonoBehaviour
{
    public Image life1, life2, life3, level1, level2, level3, level4;
    public Text score, game_over, success, ready;
    public AudioClip lvl_sound, g_o_sound;
    public GameObject enemy;
    public static game_manager instance;

    private IEnumerator coroutine;
    private AudioSource audio_src;

    int enemy_alive = 0;
    int enemy_spawns = 5;
    int lvl = 1;
    int lives = 3;
    int points = 0;

    bool next_load = false;
    const int next_round_spawn = 5;

    float next_spawn = 5;
    float diff = 1f, next_lvl_diff = 0.1f;
    float interval_spawn_max = 3, interval_spawn_min = 2;
    float time_count = 0;
    float temp = 0.5f;
    float pos_x = 3;
    float pos_y = 4;

    public int Lives
    {
        get
        {
            return lives;
        }

        set
        {
            lives = value;
        }
    }

    void Start()
    {
        instance = this;

        audio_src = GetComponent<AudioSource>();
        audio_src.PlayOneShot(lvl_sound);

        coroutine = instance.Ready(1f);
        StartCoroutine(coroutine);

        Refresh_ui();
    }

    private void Update()
    {
        if (enemy_spawns > 0)
        {
            if (next_spawn > 0)
            {
                next_spawn -= Time.deltaTime;
                if (next_spawn <= 0)
                {
                    Spawn_enemy();
                    next_spawn = Random.Range(interval_spawn_min, interval_spawn_max) * diff;
                    enemy_spawns--;
                }
            }
        }
        else if (enemy_alive <= 0 && next_load == false)
        {
            next_load = true;
            instance.lvl++;
            instance.coroutine = instance.Load_Next_Level();
            instance.StartCoroutine(coroutine);
        }
    }

    void Spawn_enemy()
    {
        Instantiate(enemy);
        enemy_alive++;
    }

    public static void Lose_live()
    {
        instance.Lives--;
        Refresh_ui();

        if (instance.Lives <= 0)
        {
            instance.coroutine = instance.Load_Intro();
            instance.StartCoroutine(instance.coroutine);
        }
    }

    public static void Get_points(int _points = 100)
    {
        instance.points += _points;
        Refresh_ui();
        instance.enemy_alive--;
    }

    static void Refresh_ui()
    {
        instance.score.text = instance.points.ToString();

        switch (instance.Lives)
        {
            case 0:
                instance.life1.enabled = false;
                break;
            case 1:
                instance.life2.enabled = false;
                break;
            case 2:
                instance.life3.enabled = false;
                break;
        }

        switch (instance.lvl)
        {
            case 2:
                instance.level2.enabled = true;
                break;
            case 3:
                instance.level3.enabled = true;
                break;
            case 4:
                instance.level4.enabled = true;
                break;
        }
    }

    IEnumerator Load_Next_Level()
    {
        audio_src.PlayOneShot(lvl_sound);
        Refresh_ui();
        success.enabled = true;

        while (time_count <= temp)
        {
            time_count += Time.deltaTime;
            success.color = Color.gray;
            yield return new WaitForSeconds(.1f);
            success.color = Color.white;
            yield return new WaitForSeconds(.1f);
        }
        
        success.enabled = false;

        instance.coroutine = instance.Ready(1f);
        instance.StartCoroutine(instance.coroutine);

        time_count = .5f;
        diff -= next_lvl_diff;
        enemy_spawns = next_round_spawn * lvl;
        next_spawn = interval_spawn_max;
        next_load = false;
    }

    IEnumerator Ready(float t)
    {

        ready.enabled = true;
        yield return new WaitForSeconds(t);
        ready.enabled = false;
    }

    IEnumerator Load_Intro()
    { 
        game_over.enabled = true;

        while (time_count <= temp)
        {
            time_count += Time.deltaTime;
            game_over.color = Color.gray;
            yield return new WaitForSeconds(.1f);
            game_over.color = Color.white;
            yield return new WaitForSeconds(.1f);
        }
        yield return new WaitForSeconds(4f);
        game_over.enabled = false;
        time_count = .5f;
        SceneManager.LoadScene("intro");
    }

    public Vector2 Fly_to_pos()
    {
        if (pos_x == (-2))
        {
            pos_x = 3;
            pos_y--;
        }
        pos_x--;
        return new Vector2(pos_x, pos_y);

    }

    public static float camera_height()
    {
        return Camera.main.orthographicSize;
    }

    public static float camera_width()
    {
        return camera_height() * Camera.main.aspect;
    }
}
