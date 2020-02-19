using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeaponSelect : MonoBehaviour
{
    public enum EquipmentSlots {EquipOne, EquipTwo, EquipThree, EquipFour}
    public EquipmentSlots selectedSlot;

    public Equipment basicAttack;

    //Move slots to character data
    public Equipment equipSlotOne;
    public Equipment equipSlotTwo;
    public Equipment equipSlotThree;
    public Equipment equipSlotFour;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    public void InputCheck(float horizontal, float vertical)
    {
        if (horizontal > 0)
        {
            selectedSlot = EquipmentSlots.EquipTwo;
        }
        if (horizontal < 0)
        {
            selectedSlot = EquipmentSlots.EquipFour;
        }
        if (vertical > 0)
        {
            selectedSlot = EquipmentSlots.EquipOne;
        }
        if (vertical < 0)
        {
            selectedSlot = EquipmentSlots.EquipThree;
        }
    }
}
