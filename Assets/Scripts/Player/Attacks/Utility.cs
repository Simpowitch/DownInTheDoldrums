using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static Direction GetAttackDirection(float xDirection, float yDirection)
    {
        if (xDirection < 0)
        {
            return Direction.Left;
        }
        if (xDirection > 0)
        {
            return Direction.Right;
        }
        if (yDirection < 0)
        {
            return Direction.Down;
        }
        if (yDirection > 0)
        {
            return Direction.Up;
        }
        return Direction.Down;
    }

}
