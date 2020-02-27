using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterWeaponSelect : MonoBehaviour
{
    [SerializeField] Image selectIcon = null;

    CharacterData characterData;

    private void Awake()
    {
        characterData = GetComponent<CharacterData>();
    }


    public void InputCheck(Direction inputDirection)
    {
        switch (inputDirection)
        {
            case Direction.Up:
                selectIcon.transform.localPosition = new Vector3(0, 30);
                SelectWeapon(characterData.equipSlotOne);
                break;

            case Direction.Right:
                selectIcon.transform.localPosition = new Vector3(30, 0);
                SelectWeapon(characterData.equipSlotTwo);
                break;

            case Direction.Down:
                selectIcon.transform.localPosition = new Vector3(0, -30);
                SelectWeapon(characterData.equipSlotThree);
                break;

            case Direction.Left:
                selectIcon.transform.localPosition = new Vector3(-30, 0);
                SelectWeapon(characterData.equipSlotFour);
                break;

            default:
                break;
        }
    }

    public void SelectWeapon(CharacterWeaponHolder weaponHolder)
    {
        if (characterData.selectedWeaponHolder != null)
        {
            if (characterData.selectedWeaponHolder.myWeapon != null)
            {
                characterData.selectedWeaponHolder.myWeapon.GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        characterData.selectedWeaponHolder = weaponHolder;
        if (characterData.selectedWeaponHolder.myWeapon != null)
        {
            characterData.selectedWeaponHolder.myWeapon.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
