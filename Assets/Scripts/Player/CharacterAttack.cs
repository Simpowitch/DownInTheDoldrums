using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    CharacterData characterData;

    private void Start()
    {
        characterData = GetComponent<CharacterData>();
    }

    public void InputCheck(Direction interactDirection)
    {
        characterData.selectedWeaponHolder.Attack(new RotationDirection(interactDirection));
    }
}
