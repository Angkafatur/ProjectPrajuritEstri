using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBossZone : MonoBehaviour
{
    public Vector3 enterBossZone;

    public void EnterZone()
    {
        transform.position = enterBossZone;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "BossZone") 
        {
            EnterZone();
        }
    }
}
