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

    public void Attack(RotationDirection attackDirection)
    {
        if (cooldownTimer <= 0)
        {
            GameObject spawnedObject = Instantiate(myWeapon.spawnableObject.gameObject, transform.position + attackDirection.direction, attackDirection.rotation);
            if (spawnedObject.GetComponent<WeaponSpawnedObject>().attachedToPlayer)
            {
                spawnedObject.transform.SetParent(transform);
            }
            
            cooldownTimer = myWeapon.cooldown;
        }
    }
}
