using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{

    private Vector2 direction;

    [SerializeField] private float speed;
    [SerializeField] private string targettag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void Setup(Vector2 direction)
    {
        this.direction = direction;
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
