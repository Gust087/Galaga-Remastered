using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot_enemy : MonoBehaviour {
    
    public AudioClip shot_audio;
    
    Vector2 destination, current_position;

    void Start ()
    {
        current_position = transform.position;
        destination = new Vector2(UnityEngine.Random.Range(-game_manager.camera_width(), game_manager.camera_width()), -5);
    }

    private void Update()
    {
        if (Mathf.Abs(transform.position.y) >= 5)
        {
            gameObject.SetActive(false);
        }
        current_position = Vector2.MoveTowards(current_position, destination, 2 * Time.deltaTime);

        transform.position = current_position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag != "shot" && collision.gameObject.tag != "Enemy")
        {
            gameObject.SetActive(false);   
        }

    }

    private void OnEnable()
    {
        AudioSource.PlayClipAtPoint(shot_audio, Camera.main.transform.position);
    }
}
