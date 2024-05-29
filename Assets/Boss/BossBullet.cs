using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{

    private Vector2 direction;

    [SerializeField] private float speed;
    [SerializeField] private string targettag;
    [SerializeField] Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
