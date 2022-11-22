using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;

    public Transform[] dirs;

    public GameObject pepe;
    public GameObject pepito;

    public GameObject sword;
    //public GameObject thrust;
    public GameObject swordReference;

    public GameObject animatorHandler;
    public Animator anim;
    public  Animator swordAnim;
    public Animator thrustAnim;

    public Vector3 mousePos;
    public Vector3 auxMousePos;
    float dist1;

    //Move
    Vector3 moveDir;
    public Vector3 dirAux;


    public float speed;
    public float maxRunSpeed;
    public float maxRunSpeedHold;
    public float minRunSpeed;
    public float maxFallSpeed;
    public float minFallSpeed;
    public float landingFallSpeed;
    float velocityFall;
    float velocityFallMult = 0.9f;



    bool flip;
    bool flipOnce = true;

    //Jump
    bool jumpFrame;
    bool jumpMade;
    bool diagonalJump;
    bool diagonalJumpMade;
    bool changeDirDiagJump;
    public float maxAirRotation;
    public groundCheck currentGround;
    float yValueGrounded;
    Vector3 pretendedPlayerPosition;
    public Transform jumpDir;
    public bool grounded;
    bool jumpCondition;
    //bool jumping;
    //bool jump;
    public float jumpForceHold;
    public float jumpForce;
    public float jumpForceLerp;

    //Dash
    public GameObject dashTrial;
    public Vector3 dashDir;
    public float dashForce;
    //public float _dashTime;
    float dashTime;
    public float _dashCD;
    float dashCD;
    public bool dashReady;
    bool atackDash;
    //bool dashable;
    //bool dashing;
    public bool dash;
    //public float _gravityTime;
    //float gravityTime;
    public float gravityScale;
    bool afterDash;

    //bool dashAttack;

    public float dashDuration;

    
    //Attack
    public GameObject enemyProjectile;
    public GameObject newEnemy;

    private float _tickAttack;
    public bool attack;
    public int attackCharge;

    bool chargingAttack;
    bool attacking;
    public float charge1time;
    public float charge2time;
    float timeCharging;
    public float _attackingTime;
    float attackingTime;
    public Vector2 attackDir;

    //Thrust
    public float thrustForce;
    float thrustTime;
    public float thrustDuration;
    public Vector3 thrustDir;
    bool thrustReady = true;
    bool afterThrust;

    bool thrusting;
    public Vector2 playerAuxPos;
    public bool grabbed;
    public float _grabbedTime;
    float grabbedTime;
    public bool projectileAttackFunction;
    public bool enemyHasBeenThrusted;
    public float projectileForce;
    public Transform projectileSpawn;
    bool cancelThrustWall;

    // Start is called before the first frame update
    void Start()
    {
        StartSettings();
    }

    void StartSettings()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren <Animator>();
        swordAnim = sword.GetComponent<Animator>();
        //thrustAnim = thrust.GetComponent<Animator>();
        dashCD = _dashCD;
        grabbedTime = _grabbedTime;
        dashReady = true;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSettings();

        print(grabbed);

        GetInput();
        GetFixedDir(0);

        Move();
        FallingSpeed();
        Landing();

        FixSpriteDirection();
    }

    void UpdateSettings()
    {
        anim.SetFloat("VerticalSpeed", rb.velocity.y);
        anim.SetBool("Grounded", grounded);
    }

    void FixedUpdate()
    {
        if (dash)
        {
            Dash();
        }
        if (afterDash)
        {
            AfterDash();
        }
        if (anim.GetBool("Thrusting"))
        {
            ThrustDash();
        }
        if (afterThrust)
        {
            AfterThrustDash();
        }
        if (anim.GetBool("Grabbed"))
        {
            ThrustWall();
        }
    }

    public void GetInput()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        moveDir = (mousePos - transform.position);
        if (!attack)
        {
            auxMousePos = mousePos;
        }
        if (Input.GetMouseButton(0))
        {
            _tickAttack += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0))
        {
            swordReference = sword;
            if (_tickAttack > charge2time)
            {
                attackCharge = 2;
            }
            else if (_tickAttack > charge1time)
            {
                attackCharge = 1;
            }
            else
            {
                attackCharge = 0;
            }
            if (!(attackCharge == 0 && !thrustReady))
            {
                swordAnim.SetInteger("AttackLvl", attackCharge);
                AttackDir(attackCharge);
                attack = true;
                swordAnim.SetTrigger("Attack");
            }
            
            _tickAttack = 0f;
        }
        if (Input.GetKey(KeyCode.W) && !grabbed)
        {
            anim.SetBool("Walking", true);
        }
        else if (Input.GetKeyUp(KeyCode.W) || grabbed)
        {
            anim.SetBool("Walking", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && (grounded || jumpCondition))
        {
            Jump();
            jumpFrame = true;
            ResetAttack();
            ResetThrustWall();
            thrustReady = true;
            afterThrust = false;
        }
        else if (Input.GetKeyUp(KeyCode.Space) || ((rb.velocity.y <= 1.0f) && anim.GetBool("Jump")) 
                                               || anim.GetBool("Grabbed") /*|| (anim.GetBool("Jump") && grounded)*/)
        {
            ResetJump();
        }
        else
        {
            jumpFrame = false;
        }

        
        if (grounded && !anim.GetBool("Jump"))
        {
            yValueGrounded = currentGround.leftVertex.y + gameObject.GetComponent<CircleCollider2D>().radius;
            if (gameObject.transform.position.y < yValueGrounded)
            {
                if(Mathf.Sign(dirAux.x) != Mathf.Sign(jumpDir.up.x))
                {
                    maxRunSpeed = maxRunSpeedHold;
                }
                else if (Mathf.Sign(dirAux.x) == Mathf.Sign(jumpDir.up.x))
                {
                    maxRunSpeed = maxRunSpeedHold * 0.15f;
                }
                pretendedPlayerPosition = new Vector3(gameObject.transform.position.x, yValueGrounded, 0);
                if(Vector3.Distance(currentGround.rightVertex, gameObject.transform.position) < Vector3.Distance(currentGround.leftVertex, gameObject.transform.position))
                {
                    jumpDir.up = pretendedPlayerPosition - currentGround.rightVertex;
                }
                else
                {
                    jumpDir.up = pretendedPlayerPosition - currentGround.leftVertex;
                }
                jumpDir.up = new Vector3(jumpDir.up.x * maxAirRotation, jumpDir.up.y, jumpDir.up.z);
                diagonalJump = true;
            }
            else
            {
                //jumpForce = 0;
                diagonalJump = false;
                jumpDir.up = Vector3.up;
                changeDirDiagJump = false;
                maxRunSpeed = maxRunSpeedHold;
                diagonalJumpMade = false;
            }
            //print(gameObject.transform.position.y + "      " + yValueGrounded);
        }
        if(!grounded && anim.GetBool("Jump"))
        {
            jumpMade = true;
            jumpForce = jumpForceHold;
        }
        if (diagonalJump && anim.GetBool("Jump"))
        {
            diagonalJumpMade = true;
            maxRunSpeed = maxRunSpeedHold;
        }
        if (diagonalJumpMade)
        {
            if (jumpDir.up.x > 0)
            {
                if (dirAux.x < jumpDir.up.x)
                {
                    changeDirDiagJump = true;
                }
            }
            else if (jumpDir.up.x < 0)
            {
                if (dirAux.x > jumpDir.up.x)
                {
                    changeDirDiagJump = true;
                }
            }
        }
        if (changeDirDiagJump)
        {
            jumpDir.up = (new Vector3(moveDir.normalized.x * maxAirRotation, jumpDir.up.normalized.y, 0)).normalized;
        }

        if (Input.GetKeyDown(KeyCode.E) && dashReady)
        {
            if (!attack)
            {
                anim.SetBool("Dashing", true);
                swordAnim.SetBool("Dashing", true);
            }
            else
                atackDash = true;
        }
        else if (atackDash && !attack)
        {
            anim.SetBool("Dashing", true);
            swordAnim.SetBool("Dashing", true);
            atackDash = false;
        }
    }

    public Vector3 GetFixedDir(int value)
    {
        Transform lowerDist = null;
        int choosedFixedDir = new int();
        Vector3 dashDir = new Vector3();
        pepe.transform.LookAt(new Vector2(mousePos.x, mousePos.y));
        dist1 = 100000;

        for (int i = 0; i < dirs.Length; i++)
        {
            if (Vector2.Distance(pepito.transform.position, dirs[i].position) < dist1)
            {
                dist1 = Vector2.Distance(pepito.transform.position, dirs[i].position);
                lowerDist = dirs[i];
                choosedFixedDir = i;
                if (!attack)
                {
                    if (i == 4)
                        flip = true;
                    else
                        flip = false;
                }
            }
        }
        //ShowFixedDir
        /*for (int i = 0; i < dirs.Length; i++)
        {
            if (dirs[i].position == lowerDist.position)
            {
                dirs[i].gameObject.SetActive(true);
            }
            else
            {
                dirs[i].gameObject.SetActive(false);
            }
        }*/
        if(value == 0)
            return (lowerDist.position - transform.position).normalized;
        else if(value == 1)
        {
            if (choosedFixedDir == 7 || choosedFixedDir == 0 || choosedFixedDir == 1)
            {
                if (moveDir.x >= 0)
                {
                    choosedFixedDir = 2;
                }
                else if (moveDir.x < 0)
                {
                    choosedFixedDir = 6;
                }
                dashDir = (dirs[choosedFixedDir].position - transform.position).normalized;
            }
            else
            {
                dashDir = (lowerDist.position - transform.position).normalized;
            }
            return dashDir;
        }
        return new Vector3();
    }
    
    void Move()
    {
        if (moveDir.x > maxRunSpeed)
        {
            dirAux = new Vector3(maxRunSpeed, 0);
        }
        else if (moveDir.x < -maxRunSpeed)
        {
            dirAux = new Vector3(-maxRunSpeed, 0);
        }
        else if (Mathf.Abs(moveDir.x) < minRunSpeed)
        {
            dirAux *= 0;
        }
        else
        {
            dirAux = new Vector3(moveDir.x, 0);
        }
        if (anim.GetBool("Walking"))
        {
            rb.velocity = new Vector3((dirAux.x * speed) + (jumpDir.up.x * (jumpForce * (jumpDir.up.y * 0.2f))), changeDirDiagJump ? rb.velocity.y + (jumpDir.up.normalized.y * 0.02f) : rb.velocity.y, 0);
        }
        if (!anim.GetBool("Walking") /*&& !anim.GetBool("Jump")*/)
        {
            rb.velocity = new Vector3(0 + (jumpDir.up.x * jumpForce), rb.velocity.y, 0);
        }
    }

    public void FixSpriteDirection()
    {
        if(attack && flip && flipOnce)
        {
            sword.GetComponent<SpriteRenderer>().flipX = ((auxMousePos.x > transform.position.x));
            flipOnce = false;
        }
        else if (attack && !flip && flipOnce)
        {
            sword.GetComponent<SpriteRenderer>().flipX = ((mousePos.x < transform.position.x));
            flipOnce = false;
        }
        anim.gameObject.GetComponent<SpriteRenderer>().flipX = ((mousePos.x < transform.position.x) && !dash);
    }

    public void AttackDir(int lvlAttack)
    {
        if (dash)
        {
            swordReference.transform.up = dashDir;
        }
        else
        {
            swordReference.transform.up = GetFixedDir(0);
        }
    }

    public void ResetAttack()
    {
        flipOnce = true;
        jumpCondition = false;
        cancelThrustWall = false;
        grabbedTime = _grabbedTime;
        attackCharge = 0;
    }

    public void ThrustDash()
    {
        thrustReady = false;
        if (thrustTime < thrustDuration && anim.GetBool("Thrusting") && !anim.GetBool("Dashing"))
        {
            thrustTime += Time.fixedDeltaTime;
            rb.gravityScale *= 0;
            rb.velocity = thrustDir * thrustForce;
        }
        if (thrustTime >= thrustDuration || anim.GetBool("Dashing"))
        {
            anim.SetBool("Thrusting", false);
            ResetThrustDash();
        }
    }

    public void ResetThrustDash()
    {
        afterThrust = true;
        thrustTime = 0;
        ///if (!anim.GetBool("Grabbed"))
        rb.gravityScale = gravityScale;
        //animatorHandler.gameObject.transform.right = Vector3.right;
        rb.velocity = Vector2.zero;
    }

    public void AfterThrustDash()
    {
        if (grounded || anim.GetBool("Dashing"))
        {
            thrustReady = true;
            afterThrust = false;
        }
    }

    void ThrustWall()
    {
        grabbed = true;
        transform.position = playerAuxPos;
        rb.gravityScale *= 0;
        rb.velocity = new Vector2(0, 0);
        grabbedTime -= Time.deltaTime;
        jumpCondition = true;
        if (grabbedTime <= 0 || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.W) || anim.GetBool("Jump") || Input.GetMouseButtonUp(0))
        {
            ResetThrustWall();
        }
    }

    public void ResetThrustWall()
    {
        ResetAttack();
        attackCharge = 1;
        anim.SetBool("Grabbed", false);
        swordAnim.SetBool("Grabbed", false);
        jumpCondition = false;
        cancelThrustWall = false;
        grabbed = false;
        grabbedTime = _grabbedTime;
    }

    public void Ground(bool state)
    {
        grounded = state;
    }

    public void Jump()
    {
        jumpForce = jumpForceHold;
        anim.SetBool("Jump", true);
        rb.velocity = jumpDir.up * (jumpForce * (jumpDir.up.y * jumpDir.up.y));
    }

    public void ResetJump()
    {
        jumpFrame = false;
        anim.SetBool("Jump", false);
        rb.gravityScale *= 0.9f;
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.3f);
    }

    public void Dash()
    {
        if(dashTime < dashDuration && anim.GetBool("Dashing"))
        {
            dashTime += Time.fixedDeltaTime;
            rb.velocity = dashDir * dashForce;
            rb.gravityScale *= 0;
            animatorHandler.gameObject.transform.right = dashDir;
            //dashTrial.SetActive(true);
        }
        if (dashTime >= dashDuration || anim.GetBool("Grabbed"))
        {
            anim.SetBool("Dashing", false);
            swordAnim.SetBool("Dashing", false);
            //dashTrial.SetActive(false);
        }
        dashReady = false;
    }

    public void ResetDash()
    {
        animatorHandler.gameObject.transform.right = Vector3.right;
        afterDash = true;
        if(!anim.GetBool("Grabbed"))
            rb.gravityScale = gravityScale;
        dash = false;
        dashTime = 0;
    }

    void AfterDash()
    {
        if (!dashReady)
        {
            dashCD -= Time.fixedDeltaTime;
        }
        if (dashCD <= 0 && (grounded || jumpCondition))
        {
            dashReady = true;
            afterDash = false;
            dashCD = _dashCD;
        }
    }

    void FallingSpeed()
    {
        velocityFall = rb.velocity.y;
        rb.gravityScale = gravityScale;
        if (!anim.GetBool("Dashing") && !grounded)
        {
            if (rb.velocity.y > 0)
            {
                animatorHandler.transform.up = rb.velocity;
            }
            else if (rb.velocity.y < 0)
            {
                animatorHandler.transform.up = -rb.velocity;
            }
        }
        else if (!anim.GetBool("Dashing") && grounded)
        {
            animatorHandler.transform.up = Vector3.up;
        }
        if (!grounded)
        {
            maxRunSpeed = maxRunSpeedHold;
        }
        if (rb.velocity.y < 2)
        {
            rb.gravityScale = gravityScale * 0.3f;
            maxRunSpeed = maxRunSpeedHold * 0.8f;
        }
        if (rb.velocity.y < -0.001f && !grounded)
        {
            rb.gravityScale = gravityScale * 0.8f;
        }
        if (rb.velocity.y < -2.0f && !grounded)
        {
            rb.gravityScale = gravityScale * 1.75f;
            maxRunSpeed = maxRunSpeedHold * 1.0f;
        }
        if (rb.velocity.y < -10.0f && rb.velocity.y > -maxFallSpeed && !grounded)
        {
            rb.gravityScale = gravityScale * 3.5f;
        }
        if (rb.velocity.y < -maxFallSpeed)
        {
            rb.gravityScale = gravityScale * 1.0f;
            if (velocityFallMult < 0.97f)
                velocityFallMult += 0.001f;
            velocityFall *= velocityFallMult;
            rb.velocity = new Vector2(rb.velocity.x, velocityFall);
        }
        //print(rb.gravityScale);
    }

    void Landing()
    {
        if (!grounded)
        {
            landingFallSpeed = rb.velocity.y;
        }
        else if (grounded)
        {
            rb.gravityScale = gravityScale * 1.0f;
            if (jumpMade)
            {
                jumpForce = jumpForceHold * 0.0f;
                jumpMade = false;
            }
            if (landingFallSpeed < -minFallSpeed)
                anim.SetBool("Landing", true);
            else
                anim.SetBool("Landing", false);
        }
    }
}