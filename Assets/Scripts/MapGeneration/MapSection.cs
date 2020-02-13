using System.Collections.Generic;
using UnityEngine;


public class MapSection : MonoBehaviour
{
    public int sectionXSize = 10;
    public int sectionYSize = 10;

    public Tile[,] sectionTiles;

    public Transform sectionExitParent = null;
    public List<Tile> sectionExits = new List<Tile>();
    public List<Direction> exitDirections = new List<Direction>();


    private void Start()
    {
        InitializeSectionTiles();
        SetSectionExits();
    }

    public void InitializeSectionTiles()
    {
        sectionTiles = new Tile[sectionXSize, sectionYSize];

        for (int x = 0; x < sectionTiles.GetLength(0); x++)
        {
            for (int y = 0; y < sectionTiles.GetLength(1); y++)
            {
                sectionTiles[x, y] = new Tile(x, y);
            }
        }
    }

    public void SetSectionExits()
    {
        Transform[] sectionExitTransforms = sectionExitParent.GetComponentsInChildren<Transform>();
        foreach (var item in sectionExitTransforms)
        {
            sectionExits.Add(GetClosestTile(item.localPosition.x, item.localPosition.y));
        }
    }

    private Tile GetClosestTile(float x, float y)
    {
        int _x = Mathf.RoundToInt(x);
        int _y = Mathf.RoundToInt(y);

        foreach (var item in sectionTiles)
        {
            if (item.x == _x && item.y == _y)
            {
                return item;
            }
        }
        Debug.LogError("Tile not found, this will cause unexpected behaviour");
        return new Tile(10000, 10000);
    }
}

[System.Serializable]
public struct Tile
{
    public int x;
    public int y;

    public Tile(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
}
