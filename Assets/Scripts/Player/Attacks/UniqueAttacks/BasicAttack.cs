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
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyAI>().TakeDamage(base.damage);
            GetComponent<Collider>().enabled = false;
        }
    }
}
