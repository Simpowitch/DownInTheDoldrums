using UnityEngine;
using UnityEditor;
public class WeaponCreator
{
   [MenuItem("Assets/Weapon")]
    static void CreatePrefab()
    {
        GameObject weaponPrefab = new GameObject();
        weaponPrefab = GameObject.Instantiate(weaponPrefab);
        weaponPrefab.name = "New Weapon";
        weaponPrefab.AddComponent(typeof(SpriteRenderer));
        weaponPrefab.GetComponent<SpriteRenderer>().sortingOrder = 1;

        weaponPrefab.AddComponent(typeof(Weapon));

        string localPath = "Assets/Weapons/New Weapon.prefab";

        // Make sure the file name is unique, in case an existing Prefab has the same name.
        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

        // Create the new Prefab.
        PrefabUtility.SaveAsPrefabAssetAndConnect(weaponPrefab, localPath, InteractionMode.UserAction);
        GameObject.DestroyImmediate(weaponPrefab);

    }
}
