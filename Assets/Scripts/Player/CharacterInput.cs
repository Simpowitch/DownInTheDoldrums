using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterInteraction), typeof(CharacterMovement), typeof(CharacterWeaponSelect))]
public class CharacterInput : MonoBehaviour
{
    CharacterInteraction interaction;
    CharacterMovement movement;
    CharacterWeaponSelect weaponSelect;

   
    // Start is called before the first frame update
    void Start()
    {
        interaction = GetComponent<CharacterInteraction>();
        movement = GetComponent<CharacterMovement>();
        weaponSelect = GetComponent<CharacterWeaponSelect>();
    }

    // Update is called once per frame
    void Update()
    {
        interaction.InputCheck(Input.GetAxis("HorizontalSecondary"), Input.GetAxis("VerticalSecondary"));
        movement.InputCheck(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        weaponSelect.InputCheck(Input.GetAxis("WeaponHorizontal"), Input.GetAxis("WeaponVertical"));
    }



}
