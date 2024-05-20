using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public Transform shotPoint;
    public GameObject arrowPrefab;

    public Animator animator;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        animator.SetTrigger("pRanged");
        Instantiate(arrowPrefab, shotPoint.position, shotPoint.rotation);
    }
}
