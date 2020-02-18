using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : AttackBase
{
    const float SPEED = 25;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject.Destroy(this.gameObject, base.duration);
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(transform.position + transform.right * Time.deltaTime * SPEED);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
