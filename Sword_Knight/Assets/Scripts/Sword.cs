using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public Movement player;
    public CapsuleCollider2D Attack;
    public BoxCollider2D Thrust;
    public bool thrust;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Movement>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.layer == 8 && thrust)
        {
            player.anim.SetBool("Grabbed", true);
            player.swordAnim.SetBool("Grabbed", true);
            player.playerAuxPos = player.sword.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.layer == 8)
        {
            player.anim.SetBool("Grabbed", false);
            player.swordAnim.SetBool("Grabbed", false);
        }
    }

    public void Thrusting(bool state)
    {
        Thrust.enabled = state;
        thrust = state;
    }

    public void Attacking(bool state)
    {
        Attack.enabled = state;
    }
}
