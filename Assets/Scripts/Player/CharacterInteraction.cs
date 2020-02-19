using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{

    float cooldown;
    CharacterData characterData;
    CharacterWeaponSelect tempWeaponSelect;

    private void Start()
    {
        characterData = GetComponent<CharacterData>();
        tempWeaponSelect = GetComponent<CharacterWeaponSelect>();
    }

    public void InputCheck(float xDirection, float yDirection, CharacterWeaponSelect.EquipmentSlots equipmentSlot)
    {

        Equipment equipment = GetEquipment(equipmentSlot);
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
    Equipment GetEquipment(CharacterWeaponSelect.EquipmentSlots equipmentSlot)
    {
        switch (equipmentSlot)
        {
            case CharacterWeaponSelect.EquipmentSlots.EquipOne:
                if (tempWeaponSelect.equipSlotOne == null)
                {
                    return tempWeaponSelect.basicAttack;
                }
                else
                {
                    return tempWeaponSelect.equipSlotOne;
                }
            case CharacterWeaponSelect.EquipmentSlots.EquipTwo:
                if (tempWeaponSelect.equipSlotTwo == null)
                {
                    return tempWeaponSelect.basicAttack;
                }

                else
                {
                    return tempWeaponSelect.equipSlotTwo;
                }
            case CharacterWeaponSelect.EquipmentSlots.EquipThree:
                if (tempWeaponSelect.equipSlotThree == null)
                {
                    return tempWeaponSelect.basicAttack;
                }
                else
                {
                    return tempWeaponSelect.equipSlotThree;
                }
            case CharacterWeaponSelect.EquipmentSlots.EquipFour:
                if (tempWeaponSelect.equipSlotFour == null)
                {
                    return tempWeaponSelect.basicAttack;
                }
                else
                {
                    return tempWeaponSelect.equipSlotFour;
                }
            default:
                return tempWeaponSelect.basicAttack;
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
