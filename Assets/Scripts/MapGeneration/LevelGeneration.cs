using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] int sectionSize = 20;
    [SerializeField] int mapSectionsVectorLimit = 11;

    //public SectionSpawner[,] mapSectionSpawners;

    //public Vector2Int startMapSection = Vector2Int.zero;

    public GameObject startMapSection = null;


    private void SpawnStartSection()
    {

    }


    //[SerializeField] GameObject[] startSections = null;
    //[SerializeField] GameObject[] normalSections = null;
    //[SerializeField] GameObject[] endSections = null;


    //// Start is called before the first frame update
    //void Start()
    //{
    //    GenerateLevel(3);
    //}



//    public void GenerateLevel(int criticalPathLength)
//    {
//        mapSectionSpawners = new SectionSpawner[mapSectionsVectorLimit, mapSectionsVectorLimit];
//        for (int x = 0; x < mapSectionSpawners.GetLength(0); x++)
//        {
//            for (int y = 0; y < mapSectionSpawners.GetLength(1); y++)
//            {
//                mapSectionSpawners[x, y] = new SectionSpawner();
//                mapSectionSpawners[x, y].spawnPos = new Vector2(this.transform.position.x + (x * sectionSize), this.transform.position.y + (y * sectionSize));
//            }
//        }

//        //Start
//        //Setup Critical path
//        SetUpSpawners(startMapSection, criticalPathLength, SectionSpawner.SectionType.StartSection);

//        //Setup Other sections


//        //Spawn Start Room
//        foreach (var item in mapSectionSpawners)
//        {
//            GameObject spawnedObject;
//            switch (item.sectionType)
//            {
//                case SectionSpawner.SectionType.Free:
//                    break;
//                case SectionSpawner.SectionType.StartSection:
//                    spawnedObject = Instantiate(new GameObject(item.spawnPos.ToString() + item.sectionType.ToString()));
//                    spawnedObject.transform.position = item.spawnPos;
//                    break;
//                case SectionSpawner.SectionType.NormalSection:
//                    spawnedObject = Instantiate(new GameObject(item.spawnPos.ToString() + item.sectionType.ToString()));
//                    spawnedObject.transform.position = item.spawnPos;
//                    break;
//                case SectionSpawner.SectionType.EndSection:
//                    spawnedObject = Instantiate(new GameObject(item.spawnPos.ToString() + item.sectionType.ToString()));
//                    spawnedObject.transform.position = item.spawnPos;
//                    break;
//            }

//        }
//        //Spawn Other Rooms
//        //Spawn End Rooms
//    }

//    private void SetUpSpawners(Vector2Int mapSectionIndex, int roomsToSpawn, SectionSpawner.SectionType sectionType)
//    {
//        Direction inDirection = Direction.Left;
//        Direction outDirection = (Direction)Random.Range(0, 4);

//        Vector2Int testIndex = new Vector2Int(mapSectionIndex.x, mapSectionIndex.y);
//        switch (outDirection)
//        {
//            case Direction.Left:
//                testIndex += new Vector2Int(-1, 0);
//                inDirection = Direction.Right;
//                break;
//            case Direction.Right:
//                testIndex += new Vector2Int(1, 0);
//                inDirection = Direction.Left;
//                break;
//            case Direction.Up:
//                testIndex += new Vector2Int(0, 1);
//                inDirection = Direction.Down;
//                break;
//            case Direction.Down:
//                testIndex += new Vector2Int(0, -1);
//                inDirection = Direction.Up;
//                break;
//        }

//        if (CheckValidSectionIndex(testIndex))
//        {
//            mapSectionSpawners[mapSectionIndex.x, mapSectionIndex.y].sectionType = sectionType;
//            mapSectionSpawners[mapSectionIndex.x, mapSectionIndex.y].openDirections.Add(outDirection);
//            mapSectionIndex = testIndex;
//            mapSectionSpawners[mapSectionIndex.x, mapSectionIndex.y].openDirections.Add(inDirection);
//            roomsToSpawn--;

//            if (sectionType == SectionSpawner.SectionType.StartSection)
//            {
//                sectionType = SectionSpawner.SectionType.NormalSection;
//            }
//            if (roomsToSpawn == 1)
//            {
//                sectionType = SectionSpawner.SectionType.EndSection;
//            }
//            else if (roomsToSpawn == 0)
//            {
//                return;
//            }
//            SetUpSpawners(mapSectionIndex, roomsToSpawn, sectionType);
//        }
//        else
//        {
//            //Re-Do
//            SetUpSpawners(mapSectionIndex, roomsToSpawn, sectionType);
//        }
//    }

//    private bool CheckValidSectionIndex(Vector2Int index)
//    {
//        return CheckValidSectionIndex(index.x, index.y);
//    }

//    private bool CheckValidSectionIndex(int x, int y)
//    {
//        if (x < 0 || x >= mapSectionsVectorLimit || y < 0 || y >= mapSectionsVectorLimit)
//        {
//            return false;
//        }
//        else if (mapSectionSpawners[x, y].openDirections.Count > 0)
//        {
//            return false;
//        }
//        else
//        {
//            return true;
//        }
//    }
}

//[System.Serializable]
//public class SectionSpawner
//{
//    public enum SectionType { Free, StartSection, NormalSection, EndSection }
//    public SectionType sectionType = SectionType.Free;
//    public Vector2 spawnPos;
//    public List<Direction> openDirections = new List<Direction>();
//}
