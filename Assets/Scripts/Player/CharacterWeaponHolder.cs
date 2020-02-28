using UnityEngine;

public class CharacterWeaponHolder : MonoBehaviour
{
    public Weapon myWeapon;

    public void Attack(RotationDirection attackDirection, string ignoreTag)
    {
        if (myWeapon != null)
        {
            myWeapon.Attack(this.transform, attackDirection, ignoreTag);
        }
    }
}
