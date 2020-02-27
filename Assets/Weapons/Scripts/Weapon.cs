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
    public float attackRate = 0.5f;
    [SerializeField] bool isBurst;
    [SerializeField] bool isRandom;
    [SerializeField][Range(0,180)] float randomAngleOffset;

    public List<float> SpawnedObjectsOffset = new List<float>();

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
        for (int i = 0; i < SpawnedObjectsOffset.Count; i++)
        {
            Quaternion rotation = attackDirection.rotation;

            if (isRandom)
            {
                rotation *= Quaternion.Euler(0, 0, Random.Range(-randomAngleOffset/2, randomAngleOffset/2));
            }
            

            GameObject spawnedObject = Instantiate(spawnableObject, characterTransform.position + attackDirection.direction, rotation);
            WeaponSpawnedObject weaponSpawnedObject = spawnedObject.GetComponent<WeaponSpawnedObject>();
            if (spawnedObjectAttachToCharacter)
            {
                spawnedObject.transform.SetParent(characterTransform);
            }
            weaponSpawnedObject.CreateWeaponSpawnedObject(this, ignoreTag);
            if (isBurst)
            {
                yield return new WaitForSeconds(attackRate);
            }

        }
        yield return null;
    }
}
