using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterWeaponSelect : MonoBehaviour
{
    public enum EquipmentSlots {EquipOne, EquipTwo, EquipThree, EquipFour}
    public EquipmentSlots selectedSlot;
    public Weapon selectedWeapon;

    [SerializeField] Image selectIcon = null;

    CharacterData characterData;

    void Start()
    {
        characterData = GetComponent<CharacterData>();      
    }

    public void InputCheck(float horizontal, float vertical)
    {
        if (horizontal > 0)
        {
            selectedSlot = EquipmentSlots.EquipTwo;
            selectIcon.transform.localPosition = new Vector3(30,0);
            SetWeapon(characterData.equipSlotTwo);
        }
        if (horizontal < 0)
        {
            selectedSlot = EquipmentSlots.EquipFour;
            selectIcon.transform.localPosition = new Vector3(-30, 0);
            SetWeapon(characterData.equipSlotFour);
        }
        if (vertical > 0)
        {
            selectedSlot = EquipmentSlots.EquipOne;
            selectIcon.transform.localPosition = new Vector3(0, 30);
            SetWeapon(characterData.equipSlotOne);
        }
        if (vertical < 0)
        {
            selectedSlot = EquipmentSlots.EquipThree;
            selectIcon.transform.localPosition = new Vector3(0, -30);
            SetWeapon(characterData.equipSlotThree);
        }
    }

    void SetWeapon(Weapon weapon)
    {
        //ActivateSprite
        /*
         
        .sprite = disable;

        selectedWeapon = weapon;

        selectedWeapon. sprite = enable;


        */

        //inactivatePrevious
    }
}
