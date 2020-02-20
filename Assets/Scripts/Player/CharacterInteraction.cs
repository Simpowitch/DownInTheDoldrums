using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{

    float cooldown;
    CharacterData characterData;

    private void Start()
    {
        characterData = GetComponent<CharacterData>();
    }

    public void InputCheck(float xDirection, float yDirection, CharacterWeaponSelect.EquipmentSlots equipmentSlot)
    {

        WeaponSpawnedObject equipment = GetEquipment(equipmentSlot);
        //Check attack input
        if (xDirection != 0 || yDirection != 0)
        {
            if (cooldown <= 0)
            {
                Attack(new RotationDirection(Utility.GetDirection(xDirection, yDirection)), equipment);
                cooldown = equipment.cooldown;
            }
            else if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
            }
        }
    }
    WeaponSpawnedObject GetEquipment(CharacterWeaponSelect.EquipmentSlots equipmentSlot)
    {
        switch (equipmentSlot)
        {
            case CharacterWeaponSelect.EquipmentSlots.EquipOne:
                if (characterData.equipSlotOne == null)
                {
                    return characterData.basicAttack;
                }
                else
                {
                    return characterData.equipSlotOne;
                }

            case CharacterWeaponSelect.EquipmentSlots.EquipTwo:
                if (characterData.equipSlotTwo == null)
                {
                    return characterData.basicAttack;
                }
                else
                {
                    return characterData.equipSlotTwo;
                }

            case CharacterWeaponSelect.EquipmentSlots.EquipThree:
                if (characterData.equipSlotThree == null)
                {
                    return characterData.basicAttack;
                }
                else
                {
                    return characterData.equipSlotThree;
                }

            case CharacterWeaponSelect.EquipmentSlots.EquipFour:
                if (characterData.equipSlotFour == null)
                {
                    return characterData.basicAttack;
                }
                else
                {
                    return characterData.equipSlotFour;
                }

            default:
                return characterData.basicAttack;
        }
    }

    //Instantiate current ability with it's configuration
     public void Attack(RotationDirection attackDirection, WeaponSpawnedObject equipment)
    {
        GameObject newAbility = Instantiate(equipment.gameObject, transform.position + attackDirection.direction, attackDirection.rotation);
        if (equipment.attachedToPlayer)
        {
            newAbility.transform.SetParent(transform);
        }
    }
}
