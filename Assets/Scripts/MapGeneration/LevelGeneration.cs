using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{

    [SerializeField] int sectionSize = 20;
    [SerializeField] int mapSectionsVectorLimit = 11;

    public SectionSpawner[,] mapSectionSpawners;


    [SerializeField] GameObject[] startSections = null;
    [SerializeField] GameObject[] normalSections = null;
    [SerializeField] GameObject[] endSections = null;

    List<SectionSpawner> criticalPathSpawners = new List<SectionSpawner>();

    public int criticalPathMinLength = 4;
    public int criticalPathMaxLength = 8;


    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel(Random.Range(criticalPathMinLength, criticalPathMaxLength));
    }


    public void GenerateLevel(int criticalPathLength)
    {
        mapSectionSpawners = new SectionSpawner[mapSectionsVectorLimit, mapSectionsVectorLimit];
        for (int x = 0; x < mapSectionSpawners.GetLength(0); x++)
        {
            for (int y = 0; y < mapSectionSpawners.GetLength(1); y++)
            {
                mapSectionSpawners[x, y] = new SectionSpawner();
                mapSectionSpawners[x, y].spawnPos = new Vector2(this.transform.position.x + (x * sectionSize), this.transform.position.y + (y * sectionSize));
                mapSectionSpawners[x, y].coordinates = new Vector2Int(x, y);
            }
        }

        //Start
        //Setup Critical path
        Vector2Int startCoordinates = new Vector2Int(Random.Range(1, mapSectionsVectorLimit - 1), Random.Range(1, mapSectionsVectorLimit - 1));
        SetUpCriticalPath(startCoordinates, criticalPathLength, SectionSpawner.SectionType.StartSection);

        //Setup Extra Sections
        for (int i = 0; i < criticalPathSpawners.Count; i++)
        {
            if (criticalPathSpawners[i].sectionType == SectionSpawner.SectionType.NormalSection)
            {
                SetUpSideSections(criticalPathSpawners[i]);
            }
        }

        //Spawn Rooms
        foreach (var item in mapSectionSpawners)
        {
            GameObject spawnedObject = null;
            switch (item.sectionType)
            {
                case SectionSpawner.SectionType.Free:
                    break;
                case SectionSpawner.SectionType.StartSection:
                    spawnedObject = Instantiate(startSections[Random.Range(0, startSections.Length)]);
                    spawnedObject.transform.position = item.spawnPos;
                    break;
                case SectionSpawner.SectionType.NormalSection:
                    spawnedObject = Instantiate(normalSections[Random.Range(0, normalSections.Length)]);
                    spawnedObject.transform.position = item.spawnPos;
                    break;
                case SectionSpawner.SectionType.EndSection:
                    spawnedObject = Instantiate(endSections[Random.Range(0, endSections.Length)]);
                    spawnedObject.transform.position = item.spawnPos;
                    break;
            }
            if (spawnedObject != null)
            {
                spawnedObject.name = item.coordinates.ToString() + " " + spawnedObject.name;
            }
        }
    }

    private void SetUpSideSections(SectionSpawner startSpawner)
    {
        int rng = Random.Range(0, 100);
        int extraAreaSpawnChance = 75;

        if (rng < extraAreaSpawnChance)
        {
            List<Direction> possibleDirections = GetUnblockedDirections(startSpawner.coordinates);

            if (possibleDirections.Count > 0)
            {
                Direction newDir = possibleDirections[Random.Range(0, possibleDirections.Count)];
                startSpawner.openDirections.Add(newDir);

                SectionSpawner newSpawner = GetSectionSpawner(startSpawner, newDir);
                newSpawner.sectionType = SectionSpawner.SectionType.NormalSection;
                newSpawner.openDirections.Add(GetOppositeDirection(newDir));

                SetUpSideSections(newSpawner);
            }
        }
    }

    private void SetUpCriticalPath(Vector2Int mapSectionIndex, int roomsToSpawn, SectionSpawner.SectionType sectionType)
    {
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

        if (CheckValidAndFreeSectionIndex(testIndex))
        {
            SectionSpawner sectionSpawner = mapSectionSpawners[mapSectionIndex.x, mapSectionIndex.y];

            //Set type of section
            sectionSpawner.sectionType = sectionType;

            //Add out direction to this section
            sectionSpawner.openDirections.Add(outDirection);

            //Add to critical path spawner list
            criticalPathSpawners.Add(sectionSpawner);

            //Switch active index
            mapSectionIndex = testIndex;

            //Add in direction to the next section
            mapSectionSpawners[mapSectionIndex.x, mapSectionIndex.y].openDirections.Add(inDirection);

            roomsToSpawn--;

            switch (sectionType)
            {
                case SectionSpawner.SectionType.Free:
                    Debug.LogError("Not possible spawn");
                    break;
                case SectionSpawner.SectionType.StartSection:
                    sectionType = SectionSpawner.SectionType.NormalSection;
                    break;
                case SectionSpawner.SectionType.NormalSection:
                    if (roomsToSpawn == 1)
                    {
                        sectionType = SectionSpawner.SectionType.EndSection;
                    }
                    break;
                case SectionSpawner.SectionType.EndSection:
                    return;
            }

            SetUpCriticalPath(mapSectionIndex, roomsToSpawn, sectionType);
        }
        else
        {
            //Re-Do
            SetUpCriticalPath(mapSectionIndex, roomsToSpawn, sectionType);
        }
    }

    private bool CheckValidAndFreeSectionIndex(Vector2Int index)
    {
        return CheckValidAndFreeSectionIndex(index.x, index.y);
    }

    private bool CheckValidAndFreeSectionIndex(int x, int y)
    {
        if (x < 0 || x >= mapSectionsVectorLimit || y < 0 || y >= mapSectionsVectorLimit)
        {
            return false;
        }
        else if (mapSectionSpawners[x, y].openDirections.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private List<Direction> GetUnblockedDirections(Vector2Int startSection)
    {
        List<Direction> freeDirections = new List<Direction>();

        if (CheckValidAndFreeSectionIndex(startSection + new Vector2Int(0, 1)))
        {
            freeDirections.Add(Direction.Up);
        }
        if (CheckValidAndFreeSectionIndex(startSection + new Vector2Int(0, -1)))
        {
            freeDirections.Add(Direction.Down);
        }
        if (CheckValidAndFreeSectionIndex(startSection + new Vector2Int(1, 0)))
        {
            freeDirections.Add(Direction.Right);
        }
        if (CheckValidAndFreeSectionIndex(startSection + new Vector2Int(-1, 0)))
        {
            freeDirections.Add(Direction.Left);
        }

        return freeDirections;
    }

    private SectionSpawner GetSectionSpawner(SectionSpawner originalSpawner, Direction dir)
    {
        Vector2Int coordinates = originalSpawner.coordinates;
        switch (dir)
        {
            case Direction.Left:
                coordinates += new Vector2Int(-1, 0);
                break;
            case Direction.Right:
                coordinates += new Vector2Int(1, 0);
                break;
            case Direction.Up:
                coordinates += new Vector2Int(0, 1);
                break;
            case Direction.Down:
                coordinates += new Vector2Int(0, -1);
                break;
        }

        if (CheckValidAndFreeSectionIndex(coordinates))
        {
            return mapSectionSpawners[coordinates.x, coordinates.y];
        }
        else
        {
            return null;
        }
    }

    private Direction GetOppositeDirection(Direction dir)
    {
        switch (dir)
        {
            case Direction.Left:
                return Direction.Right;
            case Direction.Right:
                return Direction.Left;
            case Direction.Up:
                return Direction.Down;
            case Direction.Down:
                return Direction.Up;
            default:
                Debug.LogError("Trying to look for an unsupported enum");
                return Direction.Left;
        }
    }
}

[System.Serializable]
public class SectionSpawner
{
    public enum SectionType { Free, StartSection, NormalSection, EndSection }
    public SectionType sectionType = SectionType.Free;
    public Vector2 spawnPos;
    public Vector2Int coordinates;
    public List<Direction> openDirections = new List<Direction>();
}
