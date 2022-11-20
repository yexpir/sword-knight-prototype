using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPAMj_Simple : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    BoxCollider2D bCol;

    Movement player;

    public Transform refMaxPointX, refMinPointX, refMaxPointY, refMinPointY;


    private Vector2 direction;

    public int hp;
    private int _hp;

    public float maxAttackDistance;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponentInChildren<Animator>();

        bCol = gameObject.GetComponentInParent<BoxCollider2D>();

        player = FindObjectOfType<Movement>();

        bCol.size = new Vector2(refMaxPointX.position.x - refMinPointX.position.x, refMaxPointY.position.y - refMinPointY.position.y);
    }

    public void Patrol()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < maxAttackDistance)
        {
            //print("Player in Attack Distance");
        }
    }

    public void SetDirection()
    {
        float _dirX = Random.Range((refMinPointX.position.x - transform.position.x), (refMaxPointX.position.x - transform.position.x));
        float _dirY = Random.Range((refMinPointX.position.y - transform.position.y), (refMaxPointX.position.y - transform.position.y));

        Vector2 _dir = new Vector2(_dirX, _dirY).normalized;

        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position, _dir, Mathf.Infinity);

        //rint(hit.point.magnitude);
        
        float speed = Random.Range(10f, maxAttackDistance);

        direction = _dir;
        gameObject.transform.up = _dir;
    }

    public void BackToPosition()
    {
        Vector2 _dir;

        _dir = direction * -1f;

        direction = _dir;
        gameObject.transform.up = _dir;
    }

    bool isDrying;

    public void Move()
    {
        rb.velocity = direction;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "water")
        {
            isDrying = true;
        }

        if (collision.gameObject == gameObject.transform.parent.gameObject)
        {
            anim.SetTrigger("ChangeDirection");
            //print("Out of Parent");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "water")
        {
            isDrying = false;
        }
    }

    float tickTakeDamageFromAirContact = 1f;
    float _tickTakeDamage;
    
    private void Update()
    {
        rb.velocity = direction * 10f;
    }

    public void TakeDamage()
    {
        _hp--;

        //print("Health downed to " + _hp);
    }
}
