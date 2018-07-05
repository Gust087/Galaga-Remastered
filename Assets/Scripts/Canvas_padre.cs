using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_padre : MonoBehaviour {

    private Text start;

    // Use this for initialization
    void Start() {
        if (GetComponentInChildren<Text>().gameObject.tag == "start")
        {
            start = GetComponentInChildren<Text>();
        }
        start.enabled = false;

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject aux = transform.GetChild(i).gameObject;

            if(aux.tag == "start")
            {

            }

        }
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
