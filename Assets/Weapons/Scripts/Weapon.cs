using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    float cooldownTimer;

    public GameObject spawnableObject;

    public float spawnedObjectLifeTime;
    public float spawnedObjectSpeed;
    public bool spawnedObjectAttachToCharacter;

    public int damage;
    public List<Effect> onHitEffects = new List<Effect>();

    public Sprite weaponSprite;
    public float cooldown;
    public int numberOfSpawnObjectsPerAttack = 1;
    public float attackRate = 0.5f;


    private void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    public float GetExpectedRange()
    {
        WeaponSpawnedObject weaponSpawnedObject = spawnableObject.GetComponent<WeaponSpawnedObject>();
        return spawnedObjectSpeed * spawnedObjectLifeTime;
    }

    public void Attack(Transform characterTransform, RotationDirection attackDirection, string ignoreTag)
    {
        if (cooldownTimer <= 0)
        {
            StartCoroutine(AttackWithAttackRate(characterTransform, attackDirection, ignoreTag));
            cooldownTimer = cooldown;
        }
    }

    IEnumerator AttackWithAttackRate(Transform characterTransform, RotationDirection attackDirection, string ignoreTag)
    {
        for (int i = 0; i < numberOfSpawnObjectsPerAttack; i++)
        {
            GameObject spawnedObject = Instantiate(spawnableObject, characterTransform.position + attackDirection.direction, attackDirection.rotation);
            WeaponSpawnedObject weaponSpawnedObject = spawnedObject.GetComponent<WeaponSpawnedObject>();
            if (spawnedObjectAttachToCharacter)
            {
                spawnedObject.transform.SetParent(characterTransform);
            }
            weaponSpawnedObject.CreateWeaponSpawnedObject(this, ignoreTag);
            yield return new WaitForSeconds(attackRate);
        }
    }
}
