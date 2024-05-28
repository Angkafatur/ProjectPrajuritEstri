using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour, ICollisionHandler
{

    private Animator animator;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bossShotPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player") 
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject go = Instantiate(bullet, bossShotPoint.position, Quaternion.identity);

        Vector3 direction = new Vector3(transform.localScale.x, 0);

        go.GetComponent<BossBullet>().Setup(direction);
    }

}
