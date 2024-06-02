using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public int attackDamage = 100;
    public float attackRange = 0.5f;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime) 
        {
            if (Input.GetButtonDown("Fire2"))
            {
                Attack();
                nextAttackTime = Time.time + 1 / attackRate; 
            }
        }
    
    }

    void Attack()
    {
        if (animator != null)
            animator.SetTrigger("pCombat");

        if (attackPoint != null)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                Enemy enemyComponent = enemy.GetComponent<Enemy>();
                if (enemyComponent != null) 
                    enemyComponent.TakeDamage(attackDamage);

                Commander commanderComponent = enemy.GetComponent<Commander>();
                if (commanderComponent != null)
                    commanderComponent.TakeDamage(attackDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
