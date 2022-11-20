using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAGMj_Simple : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    
    //public CircleCollider2D pdf;

    public Movement player;

    public Vector2 referece;

    int _hp;

    public int hp;

    public float jumpDistance, jumpForce, currentAttackDistance, minAttackDistance, maxAttackDistance, maxWalkDistance, speed;

    bool goRight, isGrounded;


    Vector2 SetObjective()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < currentAttackDistance)
        {
            return player.transform.position;
        }
        else
        {
            return referece;
        }
    }

    public float SetDirection()
    {
        if (Vector2.Distance(player.transform.position, transform.position) < currentAttackDistance)
        {
            currentAttackDistance = maxAttackDistance;
        }
        else
        {
            currentAttackDistance = minAttackDistance;
        }
        //pdf.radius = currentAttackDistance;

        float referenceX = SetObjective().x;


        if (transform.position.x < referenceX)
        {
            goRight = true;
        }
        else if (transform.position.x > referenceX)
        {
            goRight = false;
        }

        if (goRight)
        {
            return speed;
        }
        else
        {
            return -speed;
        }
    }

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponentInChildren<Animator>();

        player = FindObjectOfType<Movement>();

        //pdf = gameObject.GetComponent<CircleCollider2D>();

        _hp = hp;
    }

    void Update()
    {
        anim.SetBool("Grounded", isGrounded);
        anim.SetFloat("SpeedY", rb.velocity.y);
        anim.SetInteger("HP", _hp);
        //print(Vector2.Distance(player.transform.position, transform.position) < jumpDistance);
        Physics2D.IgnoreCollision(GetComponent<CapsuleCollider2D>(), player.GetComponent<CircleCollider2D>(), true);
    }

    public void Patroling()
    {
        rb.velocity = new Vector2(SetDirection(), rb.velocity.y);

        if (Vector2.Distance(player.transform.position, transform.position) < jumpDistance)
        {
            print("asdifhbaskdufyhbaskdjhfvbaskdjhfb");
            anim.SetTrigger("Jump");
        }
    }

    public void Jump(float direction)
    {
        rb.velocity = new Vector2(direction*2.3f, jumpForce);
    }

    public void TakeDamage(int damage)
    {
        anim.SetTrigger("TakeDamage");
        _hp--;
        print(_hp);
        anim.SetInteger("HP", _hp);
        print(anim.GetInteger("HP"));
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void Ground(bool state)
    {
        isGrounded = state;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player.sword)
        {
            TakeDamage(1);
        }
    }
}
