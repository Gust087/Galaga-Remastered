using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class delegate_handler : MonoBehaviour {

    bool e = false;
    public delegate void OnPauseDelegate(bool estado);
    public static event OnPauseDelegate BtnOnPauseDelegate;


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
            OnPause(!e);
    }

    //private void OnMouseDown()
    //{
    //    e = !e;
    //    BtnOnPauseDelegate(e);
    //}

    public void OnPause(bool e)
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            e = !e;
            BtnOnPauseDelegate(e);
            SceneManager.LoadScene("intro");
        }
    }
}
