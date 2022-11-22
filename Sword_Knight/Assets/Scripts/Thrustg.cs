using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrustg : MonoBehaviour
{
    public Movement player;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.layer == 8)
        {
            player.thrustAnim.SetBool("Grabbed", true);
            player.anim.SetBool("Grabbed", true);
            player.playerAuxPos = transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.layer == 8)
        {
            player.thrustAnim.SetBool("Grabbed", false);
            //player.ResetThrustWall();
        }
    }
}