using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttackFuntion : MonoBehaviour
{
    Movement player;
    Rigidbody2D rb;
    public float projectileForce;
    Vector3 enemySpawnPos;
    public int enemyHP;


    // Start is called before the first frame update
    void Start()
    {
        print(player);
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = player.attackDir * projectileForce;
    }

    public void SetPlayerObject(Movement objPlayer)
    {
        player = objPlayer;
    }

    public void GetEnemyStats(int hp)
    {
        enemyHP = hp;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            SpawnsEnemy();
        }
    }

    void SpawnsEnemy()
    {
        PAGMd_Simple newPAGMd;
        newPAGMd = Instantiate(player.newEnemy, transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<PAGMd_Simple>();
        newPAGMd.SetValues(enemyHP);
        Destroy(gameObject);
    }
}
