using System.Collections.Generic;
using UnityEngine;


public class MapSection : MonoBehaviour
{
    public static int sectionXSize = 10;
    public static int sectionYSize = 10;

    public Transform sectionExitParent = null;
    public List<SectionConnector> connectors;


    private void Awake()
    {
        SetSectionExits();
    }

    public void SetSectionExits()
    {
        SectionConnector[] sectionExitTransforms = sectionExitParent.GetComponentsInChildren<SectionConnector>();
        connectors = new List<SectionConnector>();
        foreach (var item in sectionExitTransforms)
        {
            connectors.Add(item);
        }
    }
}
