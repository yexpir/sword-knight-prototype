using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeLvl01 : MonoBehaviour
{
    public BeeLvl02 myBoss;

    private void Start()
    {

    }

    public bool Submit(BeeLvl02 boss)
    {
        bool recruited = false;

        if (myBoss == null)
        {
            myBoss = boss;
            recruited = true;
        }
        else
        {
            recruited = false;
        }

        return recruited;
    }

    public void MoveToPosition(Vector2 newPosition)
    {
        Vector2 futurePosition = new Vector2(newPosition.x + Random.Range(-25f, 25f), newPosition.y + Random.Range(-25f, 25f));

        transform.position = Vector2.Lerp(transform.position, futurePosition, Random.Range(0.1f, 0.0001f));
    }
}
