using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeLvl03 : MonoBehaviour
{
    public int servantAmount;

    BeeLvl02[] allServants;

    public Transform ServantsCenterPoint;
    public Transform[] servantPoints;
    public BeeLvl02[] myServants;

    private void Start()
    {
        myServants = new BeeLvl02[servantAmount];

        allServants = FindObjectsOfType<BeeLvl02>();

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

        for (int i = 0; i < servantAmount; i++)
        {
            if (myServants[i] != null)
            {
                myServants[i].SetReferencePoint(servantPoints[i]);
            }
        }
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
