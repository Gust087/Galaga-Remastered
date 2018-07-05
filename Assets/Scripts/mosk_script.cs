using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mosk_script : MonoBehaviour {

    Vector2 destination, current_position, nav_pos;
    List<shot_enemy> shot_list = new List<shot_enemy>();

    public GameObject shot;
    public Transform shot_spawn;

    const int shot_max = 3;

    int cshot = 0;

    float pos_y = 4;
    float destination_range = .2f;
    float time_to_go_pos = 5f;
    float time_in_pos = 10f;

    void Start () {

        current_position = transform.position;
        current_position.y = pos_y;
        current_position.x = Random.Range(0, game_manager.camera_width());
        transform.position = current_position;
        Choose_destination();

        nav_pos = new Vector2(Random.Range(-game_manager.camera_width(), game_manager.camera_width()), -5);

        for (int i = 0; i < shot_max; i++)
        {
            GameObject auxgo = Instantiate(shot, shot_spawn.position, shot_spawn.rotation);
            shot_list.Add(auxgo.GetComponent<shot_enemy>());
        }
    }
	
	void Update () {

        current_position = Vector2.MoveTowards(current_position, destination, 2 * Time.deltaTime);
        transform.position = current_position;

        if (time_to_go_pos > 0)
        {
            if ((current_position - destination).magnitude < destination_range)
            {
                Shot();
                Choose_destination();
            }

            time_to_go_pos -= Time.deltaTime;

            if (time_to_go_pos <= 0)
            {
                destination = game_manager.instance.Fly_to_pos();
            }
        }
        else if (time_in_pos > 0)
        {
            time_in_pos -= Time.deltaTime;
            if (time_in_pos <= 0)
            {
                Kmikze();
            }
        }
        else
        {
            Kmikze();
        }
    }

    void Kmikze()
    {
        if ((Mathf.Abs(transform.position.y)) >= 5)
        {
            current_position.y = pos_y;
            nav_pos = new Vector2(Random.Range(-game_manager.camera_width(), game_manager.camera_width()), -5);
        }
        destination = nav_pos;
    }

    void Shot()
    {
        if (cshot == shot_max) { cshot = 0; }
        shot_list[cshot].transform.SetPositionAndRotation(shot_spawn.position, shot_spawn.rotation);
        shot_list[cshot].gameObject.SetActive(true);
        cshot++;
    }

    void Choose_destination()
    {
        destination = new Vector2(Random.Range(-game_manager.camera_width(), game_manager.camera_width()), Random.Range(-game_manager.camera_height(), game_manager.camera_height()));
    }
}
