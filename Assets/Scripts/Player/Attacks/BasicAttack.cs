using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    public int damage = 5;
    public float duration = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Destroy(this.gameObject, duration);
    }
    void Update()
    {
            
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyAI>().TakeDamage(damage);
            GetComponent<Collider>().enabled = false;
        }
    }
}
