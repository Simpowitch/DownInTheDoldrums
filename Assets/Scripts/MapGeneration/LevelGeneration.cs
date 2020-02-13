using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] int sectionSize = 20;
    [SerializeField] int mapSectionsVectorLimit = 11;

    public SectionDemands[,] mapSectionDemands;

    public Vector2Int startMapSection = Vector2Int.zero;

    [SerializeField] GameObject[] startSections = null;
    [SerializeField] GameObject[] normalSections = null;
    [SerializeField] GameObject[] endSections = null;

    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel(3);
    }

    public void GenerateLevel(int criticalPathLength)
    {
        mapSectionDemands = new SectionDemands[mapSectionsVectorLimit, mapSectionsVectorLimit];
        for (int x = 0; x < mapSectionDemands.GetLength(0); x++)
        {
            for (int y = 0; y < mapSectionDemands.GetLength(1); y++)
            {
                mapSectionDemands[x, y] = new SectionDemands();
            }
        }

        //Start
        SetUpStartMapSection(criticalPathLength);
    }

    private void SetUpStartMapSection(int criticalPathLength)
    {
        Vector2Int mapSectionIndex = startMapSection;

        Direction inDirection = Direction.Left;
        Direction outDirection = (Direction)Random.Range(0, 4);

        Vector2Int testIndex = new Vector2Int(mapSectionIndex.x, mapSectionIndex.y);
        switch (outDirection)
        {
            case Direction.Left:
                testIndex += new Vector2Int(-1, 0);
                inDirection = Direction.Right;
                break;
            case Direction.Right:
                testIndex += new Vector2Int(1, 0);
                inDirection = Direction.Left;
                break;
            case Direction.Up:
                testIndex += new Vector2Int(0, 1);
                inDirection = Direction.Down;
                break;
            case Direction.Down:
                testIndex += new Vector2Int(0, -1);
                inDirection = Direction.Up;
                break;
        }

        if (CheckValidSectionIndex(testIndex))
        {
            mapSectionDemands[mapSectionIndex.x, mapSectionIndex.y].openDemands.Add(outDirection);
            mapSectionIndex = testIndex;
            mapSectionDemands[mapSectionIndex.x, mapSectionIndex.y].openDemands.Add(inDirection);
            SetUpCriticalPathSection(inDirection, mapSectionIndex, criticalPathLength - 1);
        }
        else
        {
            SetUpStartMapSection(criticalPathLength);
        }
    }

    private void SetUpCriticalPathSection(Direction inDirection, Vector2Int mapSectionIndex, int sectionsLeftToCreate)
    {
        if (sectionsLeftToCreate == 0)
        {
            return;
        }

        Direction outDirection = (Direction)Random.Range(0, 4);

        Vector2Int testIndex = new Vector2Int(mapSectionIndex.x, mapSectionIndex.y);
        switch (outDirection)
        {
            case Direction.Left:
                testIndex += new Vector2Int(-1, 0);
                inDirection = Direction.Right;
                break;
            case Direction.Right:
                testIndex += new Vector2Int(1, 0);
                inDirection = Direction.Left;
                break;
            case Direction.Up:
                testIndex += new Vector2Int(0, 1);
                inDirection = Direction.Down;
                break;
            case Direction.Down:
                testIndex += new Vector2Int(0, -1);
                inDirection = Direction.Up;
                break;
        }

        if (CheckValidSectionIndex(testIndex))
        {
            mapSectionDemands[mapSectionIndex.x, mapSectionIndex.y].openDemands.Add(outDirection);
            mapSectionIndex = testIndex;
            mapSectionDemands[mapSectionIndex.x, mapSectionIndex.y].openDemands.Add(inDirection);
            SetUpCriticalPathSection(inDirection, mapSectionIndex, sectionsLeftToCreate - 1);
        }
        else
        {
            SetUpCriticalPathSection(inDirection, mapSectionIndex, sectionsLeftToCreate);
        }
    }

    private bool CheckValidSectionIndex(Vector2Int index)
    {
        return CheckValidSectionIndex(index.x, index.y);
    }

    private bool CheckValidSectionIndex(int x, int y)
    {
        if (x < 0 || x >= mapSectionsVectorLimit || y < 0 || y >= mapSectionsVectorLimit)
        {
            return false;
        }
        else if (mapSectionDemands[x, y].openDemands.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}

[System.Serializable]
public class SectionDemands
{
    public List<Direction> openDemands = new List<Direction>();
}
