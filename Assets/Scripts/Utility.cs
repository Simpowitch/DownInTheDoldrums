using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {Up, Right, Down, Left, None}
public static class Utility
{
    ///<summary>
    ///<para>Get Direction from -1 to 1 inputs </para>
    ///</summary>
    public static Direction GetDirection(float xDirection, float yDirection)
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
        return Direction.None;
    }

}
