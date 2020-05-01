using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShoteable {

    void Inicializa(Transform shotSpawn, GameObject shot);
    void Shot(float fireRate, Transform shotSpawn, GameObject shot);
}
