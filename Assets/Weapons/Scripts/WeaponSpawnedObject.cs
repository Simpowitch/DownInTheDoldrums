using System.Collections.Generic;
using UnityEngine;


public class WeaponSpawnedObject : MonoBehaviour
{
    protected float objectLifetime; 
    protected float speed; 
    protected bool attachToCharacter;

    protected int damage;
    protected List<Effect> effects = new List<Effect>();
    protected string ignoreTag;

    void Start()
    {
        Destroy(this.gameObject, objectLifetime);
    }

    public void ApplyDamage(CharacterData characterData)
    {
        characterData.TakeDamage(damage);
    }


    public void CreateWeaponSpawnedObject(Weapon weapon, string _ignoreTag)
    {
        effects = weapon.onHitEffects;
        objectLifetime = weapon.spawnedObjectLifeTime;
        speed = weapon.spawnedObjectSpeed;
        damage = weapon.damage;
        attachToCharacter = weapon.spawnedObjectAttachToCharacter;
        ignoreTag = _ignoreTag;
    }
}
