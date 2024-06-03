using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class EnemyMovement : MonoBehaviour
{
    public Transform rayCast;
    public LayerMask rayCastMask;
    public float rayCastLenght;
    public float attackDistance;
    public float moveSpeed;
    public float timer;
    public Transform patrolLeft;
    public Transform patrolRight;
    public GameObject sBullet;
    public Transform enemyShotPoint;
    public Rigidbody2D rb;

    private RaycastHit2D hit;
    private Transform target;
    private Animator animator;
    private float distance;
    private bool attackMode;
    private bool inRange;
    private bool cooling;
    private float intTimer;
    private Transform playerTransform;

    void Awake()
    {
        SelectTarget();
        intTimer = timer;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!attackMode)
        {
            Move();
        }

        if (!InsideofPatrol() && !inRange && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_sAttack"))
        {
            SelectTarget();
        }
        else if (inRange) 
        {
            target = playerTransform;
            Flip();

        }

        if(inRange) 
        {
            Vector2 raycastDirection;
            if (transform.position.x < target.position.x)
            {
                raycastDirection = Vector2.right;
            }
            else
            {
                raycastDirection = Vector2.left;
            }

            hit = Physics2D.Raycast(rayCast.position, raycastDirection, rayCastLenght, rayCastMask);
            RayCastDebugger();
        }

        if(hit.collider != null) 
        {
            EnemyLogic();
        }
        else if(hit.collider == null)
        {
            inRange = false;
        }

        if(inRange == false)
        {
            StopAttack();
        }
    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.gameObject.tag == "Player")
        {
            target = trig.transform;
            inRange = true;
            Flip();
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);
        
        if(distance > attackDistance)
        {
            StopAttack();
        }
        else if(attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        if(cooling)
        {
            Cooldown();
            animator.SetBool("sAttack", false);
        }
    }

    void Move()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_sAttack"))
        {
            int moveDirection;
            if (transform.position.x < target.position.x)
            {
                moveDirection = 1;
            }
            else
            {
                moveDirection = -1;
            }
            rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        }
    }

    void Attack()
    {
        
        timer = intTimer;
        attackMode = true;

        animator.SetBool("sAttack", true);
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;

        if(timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;

        animator.SetBool("sAttack", false);

    }

    void SoldierShoot()
    {
        Instantiate(sBullet, enemyShotPoint.position, enemyShotPoint.rotation);
    }

    void RayCastDebugger()
    {
        if(distance > attackDistance)
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLenght, Color.red);
        }
        else if(attackDistance > distance)
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLenght, Color.green);
        }
    }

    public void TriggerCooling()
    {
        cooling = true;
    }

    private bool InsideofPatrol()
    {
        return transform.position.x > patrolLeft.position.x && transform.position.x < patrolRight.position.x;
    }

    private void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, patrolLeft.position);
        float distanceToRight = Vector2.Distance(transform.position, patrolRight.position);

        if (distanceToLeft > distanceToRight)
        {
            target = patrolLeft;
        }
        else
        {
            target = patrolRight;
        }
        target.transform.position = new Vector2(target.transform.position.x, transform.position.y);

        Flip();
    }

    private void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if(transform.position.x < target.position.x)
        {
            rotation.y = 0f;
        }
        else
        {
            rotation.y = 180f;
        }

        transform.eulerAngles = rotation;
    }
}
