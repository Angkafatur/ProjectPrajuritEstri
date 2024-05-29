using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour, ICollisionHandler
{

    private Animator animator;
    private Rigidbody2D rb;

    public GameObject bullet;
    public Transform bossShotPoint;

    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player") 
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bullet, bossShotPoint.position, bossShotPoint.rotation);
    }

}
