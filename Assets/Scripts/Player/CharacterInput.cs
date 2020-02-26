using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterAttack), typeof(CharacterMovement), typeof(CharacterWeaponSelect))]
public class CharacterInput : MonoBehaviour
{
    CharacterAttack interaction;
    CharacterMovement movement;
    CharacterWeaponSelect weaponSelect;

   
    // Start is called before the first frame update
    void Awake()
    {
        interaction = GetComponent<CharacterAttack>();
        movement = GetComponent<CharacterMovement>();
        weaponSelect = GetComponent<CharacterWeaponSelect>();
    }

    private void Start()
    {
        weaponSelect.InputCheck(Direction.Up);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAxisUsed("WeaponHorizontal", "WeaponVertical"))
        {
            weaponSelect.InputCheck(Utility.GetDirection(Input.GetAxis("WeaponHorizontal"), Input.GetAxis("WeaponVertical")));
        }
        if (IsAxisUsed("HorizontalSecondary", "VerticalSecondary"))
        {
            interaction.InputCheck(Utility.GetDirection(Input.GetAxis("HorizontalSecondary"), Input.GetAxis("VerticalSecondary")));
        }

        movement.InputCheck(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    bool IsAxisUsed(string horizontal, string vertical)
    {
        return Input.GetAxis(horizontal) != 0 || Input.GetAxis(vertical) != 0;
    }




}
