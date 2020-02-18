using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{

    float xDirection;
    float yDirection;

    float cooldown;

    public AttackBase ability;



    void Start()
    {
    }
    void Update()
    {
        xDirection = Input.GetAxis("HorizontalSecondary");
        yDirection = Input.GetAxis("VerticalSecondary");

        if (xDirection != 0 || yDirection != 0)
        {
            if (cooldown <= 0)
            {
                
                Attack(new AttackDirection(Utility.GetAttackDirection(xDirection, yDirection)), ability);
                cooldown = ability.cooldown;
            }
            else if(cooldown > 0)
            {
                cooldown -= Time.deltaTime;
            }
        }
    }

    
     public void Attack(AttackDirection attackDirection, AttackBase ability)
    {
        GameObject newAbility = Instantiate(ability.gameObject, transform.position + attackDirection.direction, attackDirection.rotation);
        if (ability.attachedToPlayer)
        {
            newAbility.transform.SetParent(transform);
        }
    }
}
