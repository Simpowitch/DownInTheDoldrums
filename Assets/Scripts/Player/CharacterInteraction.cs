using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
    public AbilityBase ability;

    float xDirection;
    float yDirection;

    float cooldown;

    void Update()
    {
        InputCheck();
    }

    void InputCheck()
    {
        xDirection = Input.GetAxis("HorizontalSecondary");
        yDirection = Input.GetAxis("VerticalSecondary");

        //Check attack input
        if (xDirection != 0 || yDirection != 0)
        {
            if (cooldown <= 0)
            {
                Attack(new AbilityDirection(Utility.GetDirection(xDirection, yDirection)), ability);
                cooldown = ability.cooldown;
            }
            else if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
            }
        }
    }

    //Instantiate current ability with it's configuration
     public void Attack(AbilityDirection attackDirection, AbilityBase ability)
    {
        GameObject newAbility = Instantiate(ability.gameObject, transform.position + attackDirection.direction, attackDirection.rotation);
        if (ability.attachedToPlayer)
        {
            newAbility.transform.SetParent(transform);
        }
    }
}
