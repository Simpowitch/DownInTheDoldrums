using System.Collections.Generic;
using UnityEngine;

public class MapSection : MonoBehaviour
{
    public Transform sectionExitParent = null;
    public List<SectionConnector> connectors;


    public void ValidateMapSection()
    {
        SetSectionExits();
    }

    private void SetSectionExits()
    {
        SectionConnector[] sectionExitTransforms = sectionExitParent.GetComponentsInChildren<SectionConnector>();
        connectors = new List<SectionConnector>();
        foreach (var item in sectionExitTransforms)
        {
            connectors.Add(item);
        }
    }
}