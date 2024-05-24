using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public float health;
    public float maxHealth;
    public Image healthBar;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
         health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);

        if (health <= 0)
        {
            Die();
        }

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        
        animator.SetTrigger("pHurt");
    }

    void Die()
    {
        animator.SetBool("pDead", true);

        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
}
