using System.Collections.Generic;
using UnityEngine;

public class MapSection : MonoBehaviour
{
    public Transform sectionSpawnersParent = null;
    public List<SectionSpawner> sectionSpawners;


    public void ValidateMapSection()
    {
        SetSpawners();
    }

    private void SetSpawners()
    {
        SectionSpawner[] sectionExitTransforms = sectionSpawnersParent.GetComponentsInChildren<SectionSpawner>();
        sectionSpawners = new List<SectionSpawner>();
        foreach (var item in sectionExitTransforms)
        {
            sectionSpawners.Add(item);
        }
    }
}