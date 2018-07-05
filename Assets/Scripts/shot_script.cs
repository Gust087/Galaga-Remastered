using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot_script : MonoBehaviour {


    public AudioClip shot, explotion;
    
    private float mov;
    private IEnumerator coroutine;
    private Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        mov = 0.5f;
    }

    // Update is called once per frame
    void Update () {
        mover(mov);
	}

    private void mover(float m)
    {

        transform.Translate(new Vector2(0, m));

        if (transform.position.y >= 5)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag != "shot" && collision.gameObject.tag != "Player")
        {
            mov = 0;
            anim.SetBool("destroy", true);
            AudioSource.PlayClipAtPoint(explotion, Camera.main.transform.position);

            game_manager.Get_points();

            collision.gameObject.SetActive(false);

            coroutine = destroy_enemy(.7f);
            StartCoroutine(coroutine);

        }
        else
        {
            coroutine = destroy_enemy(.7f);
            StartCoroutine(coroutine);
        }

    }
    private void OnEnable()
    {
        AudioSource.PlayClipAtPoint(shot, Camera.main.transform.position);
    }

    private IEnumerator destroy_enemy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        gameObject.SetActive(false);
        anim.SetBool("destroy", false);
        mov = 0.5f;
        StopCoroutine("destroy_enemy");
    }

}
