using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeLvl02 : MonoBehaviour
{
    public int servantAmount;

    public BeeLvl03 myBoss;
    public BeeLvl01[] myServants;

    Transform bossReferencePoint;

    public Transform ServantsCenterPoint;
    public Transform[] servantPoints;
    BeeLvl01[] allServants;

    private void Start()
    {
        myServants = new BeeLvl01[servantAmount];

        allServants = FindObjectsOfType<BeeLvl01>();

        Recruit();
    }

    void Recruit()
    {
        for (int i = 0; i < servantAmount; i++)
        {
            for (int e = 0; e < allServants.Length; e++)
            {
                if (myServants[i] == null && allServants[e].Submit(this))
                {
                    myServants[i] = allServants[e];
                }
            }
        }
    }

    public bool Submit(BeeLvl03 boss)
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

    public void SetReferencePoint(Transform point)
    {
        bossReferencePoint = point;
    }

    public void MoveToPosition(Vector2 newPosition)
    {
        transform.position = Vector2.Lerp(transform.position, newPosition, Random.Range(0.1f, 0.0001f));
    }

    public void MoveServants(float time)
    {
        int modifier = 1;

        for (int i = 0; i < servantAmount; i++)
        {
            if (myServants[i] != null)
            {
                modifier++;
            }
        }

        ServantsCenterPoint.rotation *= Quaternion.Euler(0f, 0f, (time * Time.deltaTime));

        for (int i = 0; i < servantAmount; i++)
        {
            if (myServants[i] != null)
            {
                myServants[i].MoveToPosition(servantPoints[i].position);
            }
        }
    }
}
