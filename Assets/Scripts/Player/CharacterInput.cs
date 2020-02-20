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
    void Start()
    {
        interaction = GetComponent<CharacterAttack>();
        movement = GetComponent<CharacterMovement>();
        weaponSelect = GetComponent<CharacterWeaponSelect>();
    }

    // Update is called once per frame
    void Update()
    {
        weaponSelect.InputCheck(Input.GetAxis("WeaponHorizontal"), Input.GetAxis("WeaponVertical"));
        interaction.InputCheck(Input.GetAxis("HorizontalSecondary"), Input.GetAxis("VerticalSecondary"), weaponSelect.selectedSlot);

        movement.InputCheck(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }



}
