using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyGroundCheck : MonoBehaviour
{
    PAGMj_Simple enemy;


    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.GetComponentInParent<PAGMj_Simple>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 8)
            enemy.Ground(true);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == 8)
            enemy.Ground(false);
    }
}
