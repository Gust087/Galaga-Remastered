using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intro_controller : MonoBehaviour {

    private Vector2 destination = new Vector2(0,2);
    private Vector2 position = new Vector2(0,-5);
   // Use this for initialization
   void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        position = Vector2.MoveTowards(position, destination, 2 * Time.deltaTime);
        transform.position = position;
    }
}
