using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeaponHolder : MonoBehaviour
{
    float cooldownTimer;
    public Weapon myWeapon;
    public SpriteRenderer mySpriteRenderer;
    private void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    public void Attack(RotationDirection attackDirection, string ignoreTag)
    {
        if (cooldownTimer <= 0)
        {
            GameObject spawnedObject = Instantiate(myWeapon.spawnableObject.gameObject, transform.position + attackDirection.direction, attackDirection.rotation);
            WeaponSpawnedObject weaponSpawnedObject = spawnedObject.GetComponent<WeaponSpawnedObject>();
            if (weaponSpawnedObject.attachToCharacter)
            {
                spawnedObject.transform.SetParent(transform);
            }
            weaponSpawnedObject.ignoreTag = ignoreTag;
            
            cooldownTimer = myWeapon.cooldown;
        }
    }
}
