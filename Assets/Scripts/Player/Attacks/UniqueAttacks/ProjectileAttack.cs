using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : WeaponSpawnedObject
{
    public float speed;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(transform.position + transform.right * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyAI>().TakeDamage(base.damage);
            Destroy(gameObject);
        }
        else if (collision.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
