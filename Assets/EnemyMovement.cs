using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.SearchService;
using UnityEditor.Tilemaps;
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

    private RaycastHit2D hit;
    private Transform target;
    private Animator animator;
    private float distance;
    private bool attackMode;
    private bool inRange;
    private bool cooling;
    private float intTimer;

    void Awake()
    {
        SelectTarget();
        intTimer = timer;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attackMode)
        {
            Move();
        }

        if (!InsideofPatrol() && !inRange && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_sAttack"))
        {
            SelectTarget();
        }

        if(inRange) 
        {
            hit = Physics2D.Raycast(rayCast.position, transform.right, rayCastLenght, rayCastMask);
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
            Vector2 targetPosition = new Vector2(target.position.x, target.transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
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

        Flip();
    }

    private void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if(transform.position.x > target.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;
        }

        transform.eulerAngles = rotation;
    }
}
