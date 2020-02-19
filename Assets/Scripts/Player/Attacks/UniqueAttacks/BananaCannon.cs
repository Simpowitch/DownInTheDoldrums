using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collision2D))]
public class BananaCannon : Equipment
{
    public float speed;
    Rigidbody2D rb;
    public float slowDownFactor = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject.Destroy(this.gameObject, base.duration);
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
            EnemyAI enemy = collision.GetComponent<EnemyAI>();
            enemy.TakeDamage(base.damage);
            StartCoroutine(enemy.SlowDown(duration, slowDownFactor));
            collision.GetComponent<EnemyAI>().TakeDamage(base.damage);
            Destroy(gameObject);
        }
        else if (collision.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
