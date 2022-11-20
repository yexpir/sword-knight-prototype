using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBoss_Simple : MonoBehaviour
{
    BoxCollider2D pdf;

    public Movement player;

    public int hp;

    int _hp;

    bool isFightOn;
    bool imAlive;

    private void Start()
    {
        SetStartingStats();
    }

    private void SetStartingStats()
    {
        pdf = gameObject.GetComponentInParent<BoxCollider2D>();
        _hp = hp;
    }

    private void Update()
    {
        imAlive = CheckHealth();
        isFightOn = GetPlayerDetected();
    }

    private bool CheckHealth()
    {
        return (_hp > 0);
    }

    private bool GetPlayerDetected()
    {
        return (pdf.bounds.Contains(player.gameObject.transform.position) && imAlive);
    }
}
