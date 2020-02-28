using UnityEngine;

[RequireComponent(typeof(CharacterAttack), typeof(CharacterMovement), typeof(CharacterWeaponSelect))]
public class CharacterInput : MonoBehaviour
{
    CharacterAttack interaction;
    CharacterMovement movement;
    CharacterWeaponSelect weaponSelect;

    void Awake()
    {
        interaction = GetComponent<CharacterAttack>();
        movement = GetComponent<CharacterMovement>();
        weaponSelect = GetComponent<CharacterWeaponSelect>();
    }

    private void Start()
    {
        weaponSelect.InputCheck(Direction.Up); //Start game with weapon slot ONE selected
    }

    void Update()
    {
        //Select weapon
        if (IsAxisUsed("WeaponHorizontal", "WeaponVertical"))
        {
            weaponSelect.InputCheck(Utility.GetDirection(Input.GetAxis("WeaponHorizontal"), Input.GetAxis("WeaponVertical")));
        }

        //Right stick / arrow keys input
        if (IsAxisUsed("HorizontalSecondary", "VerticalSecondary"))
        {
            interaction.InputCheck(Utility.GetDirection(Input.GetAxis("HorizontalSecondary"), Input.GetAxis("VerticalSecondary")));
        }

        //Left stick / WASD Inputs
        movement.InputCheck(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    //Check if there is an active axis input
    bool IsAxisUsed(string horizontal, string vertical)
    {
        return Input.GetAxis(horizontal) != 0 || Input.GetAxis(vertical) != 0;
    }
}
