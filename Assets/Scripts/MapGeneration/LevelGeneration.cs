using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public GameObject[] mapSectionBlueprints = null;

    public static List<Vector2> usedPositions = new List<Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel(3);
    }

    public void GenerateLevel(int criticalPathLength)
    {
        Vector2 spawnPos = Vector2.zero;

        for (int i = 0; i < criticalPathLength; i++)
        {

            //Start
            GameObject newSectionObject = Instantiate(mapSectionBlueprints[Random.Range(0, mapSectionBlueprints.Length)]);
            newSectionObject.transform.position = spawnPos;
            MapSection newSection = newSectionObject.GetComponent<MapSection>();

            usedPositions.Add(spawnPos);

            Vector2 oldPosition = spawnPos;
            Vector2 newPosition = oldPosition;

            List<Direction> possibleDirections = new List<Direction>();

            foreach (var item in newSection.connectors)
            {
                possibleDirections.Add(item.exitDirection);
            }

            while (usedPositions.Contains(newPosition) && possibleDirections.Count > 0)
            {
                Direction newDirection = possibleDirections[Random.Range(0, possibleDirections.Count)];
                possibleDirections.Remove(newDirection);

                switch (newDirection)
                {
                    case Direction.Left:
                        newPosition = oldPosition + new Vector2(-MapSection.sectionXSize, 0);
                        break;
                    case Direction.Right:
                        newPosition = oldPosition + new Vector2(MapSection.sectionXSize, 0);
                        break;
                    case Direction.Up:
                        newPosition = oldPosition + new Vector2(0, MapSection.sectionYSize);
                        break;
                    case Direction.Down:
                        newPosition = oldPosition + new Vector2(0, -MapSection.sectionYSize);
                        break;
                }
            }

            if (!usedPositions.Contains(newPosition))
            {
                spawnPos = newPosition;
            }
            else
            {
                Debug.LogError("No possible exit found in level generator. Will cause fault in spawn");
            }
        }
    }
}
