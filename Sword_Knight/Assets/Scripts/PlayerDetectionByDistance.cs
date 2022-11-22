using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectionByDistance : MonoBehaviour, IPlayerDetenting
{
    public Transform player;

    public float pdfOnCombat;
    public float pdfOutOfCombat;

    public bool isCombating;

    private void Start()
    {
        player = FindObjectOfType<Movement>().gameObject.GetComponent<Transform>();
    }

    private void Update()
    {
        isCombating = DetectPlayer();
    }

    public bool DetectPlayer()
    {
        if (Vector2.Distance(transform.position, player.position) > pdfOutOfCombat)
        {
            return false;
        }
        else if (Vector2.Distance(transform.position, player.position) < pdfOnCombat)
        {
            return true;
        }
        else return isCombating;
    }
}
