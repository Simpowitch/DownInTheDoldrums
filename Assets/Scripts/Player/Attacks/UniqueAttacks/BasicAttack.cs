using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : AttackBase
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Destroy(this.gameObject, base.duration);
    }
    void Update()
    {
            
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyAI>().TakeDamage(base.damage);
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
