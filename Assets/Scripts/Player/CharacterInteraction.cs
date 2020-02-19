using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
    public Equipment equipment;

    float cooldown;

    public void InputCheck(float xDirection, float yDirection)
    {
        //Check attack input
        if (xDirection != 0 || yDirection != 0)
        {
            if (cooldown <= 0)
            {
                Attack(new EquipmentDirection(Utility.GetDirection(xDirection, yDirection)), equipment);
                cooldown = equipment.cooldown;
            }
            else if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
            }
        }
    }

    //Instantiate current ability with it's configuration
     public void Attack(EquipmentDirection attackDirection, Equipment equipment)
    {
        GameObject newAbility = Instantiate(equipment.gameObject, transform.position + attackDirection.direction, attackDirection.rotation);
        if (equipment.attachedToPlayer)
        {
            newAbility.transform.SetParent(transform);
        }
    }
}
