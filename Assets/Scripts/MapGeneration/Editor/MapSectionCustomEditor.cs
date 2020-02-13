using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapSection))]
public class MapSectionCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MapSection system = (MapSection)target;


        if (GUILayout.Button("Validate Exits"))
        {
            system.ValidateMapSection();
        }
    }
}
