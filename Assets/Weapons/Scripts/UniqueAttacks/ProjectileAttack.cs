using UnityEngine;

public class ProjectileAttack : WeaponSpawnedObject
{
    Rigidbody2D rigidBody;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rigidBody.MovePosition(transform.position + transform.right * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == ignoreTag)
        {
            return;
        }
        print(collision.name);
        if (collision.tag == "Enemy" || collision.tag == "Player")
        {
            CharacterData characterData = collision.GetComponent<CharacterData>();
            base.ApplyDamage(characterData);

            foreach (Effect effect in base.effects)
            {
                characterData.AddEffect(effect);
            }
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
