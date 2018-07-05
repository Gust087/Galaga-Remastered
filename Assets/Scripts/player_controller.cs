using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour {

    public AudioClip explotion;
    public GameObject shot;
    List<shot_script> shot_list = new List<shot_script>();
    public Transform shotSpawn;
    public float fireRate;
    public Animator anim;

    private IEnumerator coroutine;

    private float nextFire;
    private int cshot = 0;
    private const int shot_max = 3;
    private bool is_dead = false;
    private float move_x;
    private float move_y;

    // Use this for initialization
    void Start ()
    {
        gameObject.SetActive(true);
        for (int i = 0; i < shot_max; i++)
        {
            GameObject auxgo = Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            shot_list.Add(auxgo.GetComponent<shot_script>());
        }

    }

    private void FixedUpdate()
    {
        if (is_dead == false)
        {
            move_x = Input.GetAxis("Horizontal");
            move_y = Input.GetAxis("Vertical");

            if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
            {
                if (cshot == shot_max) { cshot = 0; }
                nextFire = Time.time + fireRate;
                shot_list[cshot].transform.SetPositionAndRotation(shotSpawn.position, shotSpawn.rotation);
                shot_list[cshot].gameObject.SetActive(true);
                cshot++;
            }
        }
        else
        {
            move_x = .0f;
            move_y = .0f;
        }

        Vector2 movement = new Vector2(move_x * 0.1f, move_y * 0.1f);

        transform.Translate(movement);

        transform.position = new Vector2
        (
            Mathf.Clamp(transform.position.x, -2.5f, 2.5f),
            Mathf.Clamp(transform.position.y, -4.5f, 0f)
        );

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        is_dead = true;
        anim.SetBool("destroy", true);
        AudioSource.PlayClipAtPoint(explotion, Camera.main.transform.position);

        if (collision.gameObject.tag == "Enemy")
        {
            game_manager.Get_points();
        }

        game_manager.Lose_live();

        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        collision.gameObject.SetActive(false);

        coroutine = dead(4f);
        StartCoroutine(coroutine);
    }

    private IEnumerator dead(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        anim.SetBool("destroy", false);
        transform.position = new Vector2(0,-4);
        yield return new WaitForSeconds(1.5f);
        is_dead = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        if (game_manager.instance.Lives == 0)
        {
            gameObject.SetActive(false);
        }
        StopCoroutine("dead");
    }

}
