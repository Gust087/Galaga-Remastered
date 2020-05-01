using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoter : MonoBehaviour
{

    List<shot_script> shot_list = new List<shot_script>();

    private int cshot = 0;
    private const int shot_max = 3;

    // Use this for initialization
    void Inicializa(Transform shotSpawn, GameObject shot)
    {
        for (int i = 0; i < shot_max; i++)
        {
            GameObject auxgo = Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            shot_list.Add(auxgo.GetComponent<shot_script>());
        }

    }

    void Shot(float fireRate, Transform shotSpawn, GameObject shot)
    {
        if (cshot == shot_max) { cshot = 0; }
        //nextFire = Time.time + fireRate;
        shot_list[cshot].transform.SetPositionAndRotation(shotSpawn.position, shotSpawn.rotation);
        shot_list[cshot].gameObject.SetActive(true);
        cshot++;
    }
}
