using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : WeaponSpawnedObject
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ignoreTag)
        {
            return;
        }


        if (other.tag == "Enemy" || other.tag == "Player")
        {
            CharacterData characterData = other.GetComponent<CharacterData>();
            base.ApplyDamage(characterData);

            foreach (Effect effect in base.effects)
            {
                characterData.AddEffect(effect);
            }
        }
    }
}
