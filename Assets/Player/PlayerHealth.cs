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
    public Kalah kalah;
    public Menang menang;

    private bool isDead;
    private bool isWin;

    // Start is called before the first frame update
    void Start()
    {
         health = maxHealth;
         animator.SetBool("pLife", true);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
        //Debug.Log(healthBar.rectTransform.rect.width * healthBar.fillAmount);
        if (health <= 0)
        {
            Die();
            StartCoroutine(TillDie());
        }

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        
        animator.SetTrigger("pHurt");
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        // Tambahkan efek visual atau suara saat penyembuhan di sini jika perlu
        Debug.Log("Player healed, current health: " + health);
    }

    void Die()
    {
        animator.SetBool("pDead", true);
        animator.SetBool("pLife", false);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<BoxCollider2D>() .enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Jika pemain menabrak objek persegi kemenangan
        if (other.gameObject.CompareTag("BossZone") && !isWin)
        {
            isWin = true;
            menang.gameWin();
            Debug.Log("Win");
        }
    }

    IEnumerator TillDie()
    {
        yield return new WaitForSeconds(3);
        kalah.gameOver();
    }
}
