using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camFollow : MonoBehaviour {

    GameManager gm;

    public Movement player;
    Vector3 playerPos;
    public float lerpAmount;
    public float offsetZ;
    public float offsetY;
    public float offsetX;
    Vector3 pos;
    Vector3 auxPos;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();

        //gm.SetPlayer(player);
    }

    void Update()
    {
        playerPos = new Vector3(player.transform.position.x + offsetX, player.transform.position.y + offsetY);
        pos = Vector3.Lerp(transform.position, playerPos, lerpAmount * Time.deltaTime);
        //pos.y += offsetY;
        //pos.x += offsetX;
        pos.z = offsetZ;
        transform.position = pos;
    }
}
