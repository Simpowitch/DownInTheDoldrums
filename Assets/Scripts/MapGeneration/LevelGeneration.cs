using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{

    [SerializeField] int sectionSize = 20;
    [SerializeField] int mapSectionsVectorLimit = 11;

    public SectionSpawner[,] mapSectionSpawners;

    List<SectionSpawner> criticalPathSpawners = new List<SectionSpawner>();
    public int extraAreaSpawnChance = 0;


    public int criticalPathMinLength = 4;
    public int criticalPathMaxLength = 8;

    GameObject[] normalSectionBlueprints;
    GameObject[] startSectionBlueprints;
    GameObject[] endSectionBlueprints;


    // Start is called before the first frame update
    void Start()
    {
        normalSectionBlueprints = Resources.LoadAll<GameObject>("MapSections/Normal");
        startSectionBlueprints = Resources.LoadAll<GameObject>("MapSections/Start");
        endSectionBlueprints = Resources.LoadAll<GameObject>("MapSections/End");


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
        Vector2Int startCoordinates = new Vector2Int(Mathf.RoundToInt(mapSectionsVectorLimit/2), Mathf.RoundToInt(mapSectionsVectorLimit/2));
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
        foreach (var mapSpawner in mapSectionSpawners)
        {
            GameObject spawnedObject = null;
            switch (mapSpawner.sectionType)
            {
                case SectionSpawner.SectionType.Free:
                    break;
                case SectionSpawner.SectionType.StartSection:
                    spawnedObject = Instantiate(FindSectionWithMatchingOpenings(mapSpawner.GetOpenDirections(), startSectionBlueprints));
                    spawnedObject.transform.position = mapSpawner.spawnPos;
                    break;
                case SectionSpawner.SectionType.NormalSection:
                    spawnedObject = Instantiate(FindSectionWithMatchingOpenings(mapSpawner.GetOpenDirections(), normalSectionBlueprints));
                    spawnedObject.transform.position = mapSpawner.spawnPos;
                    break;
                case SectionSpawner.SectionType.EndSection:
                    spawnedObject = Instantiate(FindSectionWithMatchingOpenings(mapSpawner.GetOpenDirections(), endSectionBlueprints));
                    spawnedObject.transform.position = mapSpawner.spawnPos;
                    break;
            }
            if (spawnedObject != null)
            {
                spawnedObject.name = mapSpawner.coordinates.ToString() + " " + spawnedObject.name + " " + mapSpawner.sectionType.ToString();
            }
        }

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerObject.transform.position = new Vector2(startCoordinates.x * sectionSize, startCoordinates.y * sectionSize);
    }

    /// <summary>
    /// Returns a map section which has all the required directions specified in the parameter
    /// </summary>
    private GameObject FindSectionWithMatchingOpenings(List<Direction> requiredDirections, GameObject[] sectionBlueprints)
    {
        List<GameObject> fittingBlueprints = new List<GameObject>();
        foreach (GameObject blueprint in sectionBlueprints)
        {
            List<Direction> blueprintOpenings = blueprint.GetComponent<MapSection>().openings;
            bool matching = true;
            foreach (Direction direction in requiredDirections)
            {
                if (!blueprintOpenings.Contains(direction))
                {
                    matching = false;
                    break;
                }
                if (requiredDirections.Count != blueprintOpenings.Count)
                {
                    matching = false;
                    break;
                }
            }

            if (matching)
            {
                fittingBlueprints.Add(blueprint);
            }
        }

        if (fittingBlueprints.Count > 0)
        {
            return fittingBlueprints[Random.Range(0, fittingBlueprints.Count)];
        }
        else
        {
            Debug.LogError("Map section not found with the required direction openings");
            return null;
        }
    }

    /// <summary>
    /// Adds opening to siderooms through recursive methods
    /// </summary>
    private void SetUpSideSections(SectionSpawner startSpawner)
    {
        List<Direction> possibleDirections = GetAllowedSideRoomDirections(startSpawner);

        for (int i = 0; i < possibleDirections.Count; i++)
        {
            int rng = Random.Range(0, 100);

            if (rng < extraAreaSpawnChance)
            {
                Direction newDir = possibleDirections[Random.Range(0, possibleDirections.Count)];
                startSpawner.AddOpenDirection(newDir);

                SectionSpawner newSpawner = GetSectionSpawner(startSpawner, newDir);
                newSpawner.AddOpenDirection(GetOppositeDirection(newDir));

                if (newSpawner.sectionType == SectionSpawner.SectionType.Free)
                {
                    newSpawner.sectionType = SectionSpawner.SectionType.NormalSection;
                    SetUpSideSections(newSpawner);
                }
            }
        }
    }

    private void SetUpCriticalPath(Vector2Int mapSectionIndex, int roomsToSpawn, SectionSpawner.SectionType sectionType)
    {
        //If at the last section, end here
        if (sectionType == SectionSpawner.SectionType.EndSection)
        {
            SectionSpawner sectionSpawner = mapSectionSpawners[mapSectionIndex.x, mapSectionIndex.y];

            //Set type of section
            sectionSpawner.sectionType = sectionType;
        }
        else
        {
            //Test to add a new opening to next area
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
                sectionSpawner.AddOpenDirection(outDirection);

                //Add to critical path spawner list
                criticalPathSpawners.Add(sectionSpawner);

                //Switch active index
                mapSectionIndex = testIndex;

                //Add in direction to the next section
                mapSectionSpawners[mapSectionIndex.x, mapSectionIndex.y].AddOpenDirection(inDirection);

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

                //Recursion
                SetUpCriticalPath(mapSectionIndex, roomsToSpawn, sectionType);
            }
            else
            {
                //Re-Do
                SetUpCriticalPath(mapSectionIndex, roomsToSpawn, sectionType);
            }
        }
    }


    private bool CheckIfWithinMapLimit(Vector2Int index)
    {
        if (index.x < 0 || index.x >= mapSectionsVectorLimit || index.y < 0 || index.y >= mapSectionsVectorLimit)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool CheckValidAndFreeSectionIndex(Vector2Int index)
    {
        return (CheckIfWithinMapLimit(index) && mapSectionSpawners[index.x, index.y].sectionType == SectionSpawner.SectionType.Free);
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

    /// <summary>
    /// Adds all directions which can be used to create paths into other mapsections, but not into the end or start-area
    /// </summary>
    private List<Direction> GetAllowedSideRoomDirections(SectionSpawner startSection)
    {
        List<Direction> possibleDirections = new List<Direction>();

        //UP
        Direction testingDirection = Direction.Up;
        SectionSpawner spawnerToTest = GetSectionSpawner(startSection, testingDirection);
        if (spawnerToTest != null)
        {
            if (spawnerToTest.sectionType == SectionSpawner.SectionType.NormalSection || spawnerToTest.sectionType == SectionSpawner.SectionType.Free)
            {
                possibleDirections.Add(testingDirection);
            }
        }

        //RIGHT
        testingDirection = Direction.Right;
        spawnerToTest = GetSectionSpawner(startSection, testingDirection);
        if (spawnerToTest != null)
        {
            if (spawnerToTest.sectionType == SectionSpawner.SectionType.NormalSection || spawnerToTest.sectionType == SectionSpawner.SectionType.Free)
            {
                possibleDirections.Add(testingDirection);
            }
        }

        //DOWN
        testingDirection = Direction.Down;
        spawnerToTest = GetSectionSpawner(startSection, testingDirection);
        if (spawnerToTest != null)
        {
            if (spawnerToTest.sectionType == SectionSpawner.SectionType.NormalSection || spawnerToTest.sectionType == SectionSpawner.SectionType.Free)
            {
                possibleDirections.Add(testingDirection);
            }
        }

        //LEFT
        testingDirection = Direction.Left;
        spawnerToTest = GetSectionSpawner(startSection, testingDirection);
        if (spawnerToTest != null)
        {
            if (spawnerToTest.sectionType == SectionSpawner.SectionType.NormalSection || spawnerToTest.sectionType == SectionSpawner.SectionType.Free)
            {
                possibleDirections.Add(testingDirection);
            }
        }

        return possibleDirections;
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

        if (CheckIfWithinMapLimit(coordinates))
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
    List<Direction> openDirections = new List<Direction>(); public List<Direction> GetOpenDirections() { return openDirections; }

    public void AddOpenDirection(Direction diretionToAdd)
    {
        if (!openDirections.Contains(diretionToAdd))
        {
            openDirections.Add(diretionToAdd);
        }
    }
}
