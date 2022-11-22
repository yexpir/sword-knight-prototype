using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAGMd_Simple : MonoBehaviour
{
    public int hp;
    private int _hp;

    public float speed;
    public float maxWalkingDistance;

    public float attackDistance;

    public float pdfLeaveCombat;
    public float pdfEnterCombat;

    public float dashForce;

    private float dir;

    public bool inCombat;

    public bool goingRight;

    public Movement player;

    private Vector3 pointReference;
    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        player = FindObjectOfType<Movement>();
        pointReference = gameObject.GetComponentInParent<Transform>().position;
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        dir = 1f;
        _hp = hp;
        
        anim.SetInteger("HP", _hp);
    }

    public void SetValues(int newHP)
    {

    }

    public void Patrol()
    {
        Move();

        if (Vector3.Distance(player.transform.position, transform.position) < pdfEnterCombat)
        {
            anim.SetBool("Combat", true);
            //print(anim.GetBool("Combat"));
            //print("asdADSDFGH");
        }
    }

    public void WaitForPlayer()
    {
        if (Vector3.Distance(player.transform.position, transform.position) > pdfLeaveCombat)
        {
            anim.SetBool("Combat", false);
        }
        else
        {
            if (Vector3.Distance(player.transform.position, transform.position) < attackDistance)
            {
                anim.SetTrigger("Attack");
            }
        }
        SetDirection(player.transform.position.x > transform.position.x);
    }
    

    public void SetDirection(bool _GoingRight)
    {
        if (_GoingRight)
        {
            dir = 1f;
        }
        else
        {
            dir = -1f;
        }

        gameObject.GetComponent<SpriteRenderer>().flipX = !_GoingRight;
    }

    public void Move()
    {
        if (transform.position.x < (pointReference.x - maxWalkingDistance))
        {
            goingRight = true;
        }
        else if (transform.position.x > (pointReference.x + maxWalkingDistance))
        {
            goingRight = false;
        }

        rb.velocity = new Vector2(speed * dir, rb.velocity.y);
        SetDirection(goingRight);
    }

    public void TakeDamage(int damage)
    {
        anim.SetTrigger("TakeDamage");
        _hp--;
        anim.SetInteger("HP", _hp);
    }

    public void Stop()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
    }

    public void DashAttack()
    {
        float _dir = dir;
        rb.velocity = new Vector2(dashForce * _dir, rb.velocity.y);
    }

    public void Die()
    {
        Destroy(gameObject.GetComponentInParent<GameObject>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player.sword)
        {
            TakeDamage(1);
        }
    }
}
