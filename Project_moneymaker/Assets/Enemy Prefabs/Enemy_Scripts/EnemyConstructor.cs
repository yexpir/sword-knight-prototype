using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyConstructor : MonoBehaviour
{
    public SO_Enemy thisEnemy;
    public Movement player;
    public IPlayerDetenting functionDetenctPlayer;

    private int health;
    private string name;
    private int type;

    private void Start()
    {
        player = GameObject.Find("knight").GetComponent<Movement>();

        health = thisEnemy.enemyHP;
        name = thisEnemy.base_EnemyName;
        type = thisEnemy.enemyType;

        print("Hello! My name is " + name + ", and I'm your enemy.");
        print("My current HP is " + health + ". Come and try to kill me. It'll be fun.");
    }

    private void DetectPlayer()
    {
        functionDetenctPlayer.DetectPlayer();
    }

    private void TakeDamage()
    {
        health--;
        if (health > 0)
        {
            print("I've benn hit. My health is " + health + " now.");
        }
        else
        {
            Die();
        }
    }

    public void GetThrusted()
    {
        print("I " + thisEnemy.name + " has been thrusted.");
        //player.GetEnemyThruested("caca", type, health, this.gameObject);
    }

    private void Die()
    {
        print("I'm dead now. That's sad. You should thrusted me! That would granted you a " + type + "type Special Attack.");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == player.sword)
        {
            TakeDamage();
        }
        /*if (col.gameObject == player.thrust)
        {
            print("Get thrusted");
            GetThrusted();
        }*/
    }
}
