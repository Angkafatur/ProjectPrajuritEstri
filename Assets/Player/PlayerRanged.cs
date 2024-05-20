using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public Transform shotPoint;
    public GameObject arrowPrefab;

    public Animator animator;

    public float shotRate = 2f;
    float nextShotTime = 0f;


    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextShotTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
                nextShotTime = Time.time + 1 / shotRate;
            }
        }
       
    }

    void Shoot()
    {
        animator.SetTrigger("pRanged");
        Instantiate(arrowPrefab, shotPoint.position, shotPoint.rotation);
    }
}
