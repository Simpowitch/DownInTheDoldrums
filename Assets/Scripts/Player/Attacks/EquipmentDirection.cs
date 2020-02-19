using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentDirection
{
    public Vector3 direction;
    public Quaternion rotation;

    public EquipmentDirection(Direction attackDirection)
    {
        switch (attackDirection)
        {
            case Direction.Left:
                direction = Vector3.left * 0.5f;
                rotation = Quaternion.Euler(0, 0, 180);
                break;

            case Direction.Right:
                direction = Vector3.right * 0.5f;
                rotation = Quaternion.Euler(0, 0, 0);
                break;

            case Direction.Up:
                direction = Vector3.up * 0.52f;
                rotation = Quaternion.Euler(0, 0, 90);
                break;

            case Direction.Down:
                direction = Vector3.down * 0.53f;
                rotation = Quaternion.Euler(0, 0, 270);
                break;

            default:
                break;
        }
    }
}
