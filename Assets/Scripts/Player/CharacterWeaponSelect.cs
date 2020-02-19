using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterWeaponSelect : MonoBehaviour
{
    public enum EquipmentSlots {EquipOne, EquipTwo, EquipThree, EquipFour}
    public EquipmentSlots selectedSlot;
    [SerializeField] Image selectIcon;

    public void InputCheck(float horizontal, float vertical)
    {
        if (horizontal > 0)
        {
            selectedSlot = EquipmentSlots.EquipTwo;
            selectIcon.transform.localPosition = new Vector3(30,0);
        }
        if (horizontal < 0)
        {
            selectedSlot = EquipmentSlots.EquipFour;
            selectIcon.transform.localPosition = new Vector3(-30, 0);
        }
        if (vertical > 0)
        {
            selectedSlot = EquipmentSlots.EquipOne;
            selectIcon.transform.localPosition = new Vector3(0, 30);
        }
        if (vertical < 0)
        {
            selectedSlot = EquipmentSlots.EquipThree;
            selectIcon.transform.localPosition = new Vector3(0, -30);
        }
    }
}
