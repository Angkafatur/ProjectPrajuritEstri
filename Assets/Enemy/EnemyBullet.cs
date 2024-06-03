using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 20;
    public float speed = 15f;
    public Rigidbody2D rb;



    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth pHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (pHealth != null)
        {
            pHealth.TakeDamage(damage);
            Debug.Log("hit");
        }
        Destroy(gameObject);
    }
}
