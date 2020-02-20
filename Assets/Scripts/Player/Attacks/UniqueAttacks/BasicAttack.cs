using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : WeaponSpawnedObject
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, base.duration);
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyAI>().TakeDamage(base.damage);
        }
    }
}
