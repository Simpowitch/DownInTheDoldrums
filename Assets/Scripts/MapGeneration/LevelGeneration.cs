using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public GameObject[] mapSectionBlueprints = null;

    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel(3);
    }

    public void GenerateLevel(int criticalPathLength)
    {
        Vector3 spawnPos = Vector3.zero;

        for (int i = 0; i < criticalPathLength; i++)
        {
            //Start
            GameObject newSectionObject = Instantiate(mapSectionBlueprints[Random.Range(0, mapSectionBlueprints.Length)]);
            newSectionObject.transform.position = spawnPos;


            MapSection newSection = newSectionObject.GetComponent<MapSection>();

            Direction newDirection = newSection.exitDirections[Random.Range(0, newSection.exitDirections.Count)];

            switch (newDirection)
            {
                case Direction.Left:
                    spawnPos += new Vector3(-newSection.sectionXSize, 0);
                    break;
                case Direction.Right:
                    spawnPos += new Vector3(newSection.sectionXSize, 0);
                    break;
                case Direction.Up:
                    spawnPos += new Vector3(0, newSection.sectionYSize);
                    break;
                case Direction.Down:
                    spawnPos += new Vector3(0, -newSection.sectionYSize);
                    break;
            }
        }
    }
}
