using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundCheck : MonoBehaviour
{
    Movement player;

    public Vector3 leftVertex;
    public Vector3 rightVertex;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponentInParent<Movement>();
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.layer == 8)
        {
            player.Ground(true);
            leftVertex = new Vector3(col.bounds.center.x - col.bounds.size.x * 0.5f, col.bounds.center.y + col.bounds.size.y * 0.5f, 0);
            rightVertex = new Vector3(col.bounds.center.x + col.bounds.size.x * 0.5f, col.bounds.center.y + col.bounds.size.y * 0.5f, 0);
            //print(leftVertex);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == 8)
            player.Ground(false);
    }
}
