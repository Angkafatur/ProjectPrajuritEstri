using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public Animator animator;

    public int health = 300;

    public bool isInvulnerable = false;

    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
            return;

        health -= damage;

        animator.SetTrigger("Hurt");

        if (health <= 150)
        {
            GetComponent<Animator>().SetBool("IsEnraged", true);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("isDead", true);

        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
