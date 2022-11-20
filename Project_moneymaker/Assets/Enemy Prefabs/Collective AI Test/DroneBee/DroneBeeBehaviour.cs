using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBeeBehaviour : MonoBehaviour
{
    BeeLvl02 me;

    Rigidbody2D rb;

    public float maxDistance, maxDir, minDir;
    private float _dir;

    private void Start()
    {
        me = GetComponent<BeeLvl02>();
        rb = GetComponentInChildren<Rigidbody2D>();

        _dir = maxDir;
    }

    public void Patroll()
    {
        rb.velocity = new Vector2(_dir, 0f);

        if (rb.transform.position.x > (transform.position.x + maxDistance))
        {
            _dir = -Random.Range(minDir, maxDir);
        }
        else if (rb.transform.position.x < (transform.position.x - maxDistance))
        {
            _dir = Random.Range(minDir, maxDir);
        }

        me.MoveServants(_dir);
    }
}
