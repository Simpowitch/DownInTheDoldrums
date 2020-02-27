using UnityEngine;
using UnityEditor;
public class WeaponCreator
{
   [MenuItem("Assets/Weapon/New Weapon")]
    static void CreateWeapon()
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
        Object.DestroyImmediate(weaponPrefab);

    }
    [MenuItem("Assets/Weapon/SpawnableObject/Melee")]
    static void CreateMelee()
    {
        GameObject newPrefab = new GameObject();
        newPrefab = GameObject.Instantiate(newPrefab);
        newPrefab.name = "New Melee SpawnableObject";



        newPrefab.AddComponent(typeof(BoxCollider2D));
        newPrefab.GetComponent<BoxCollider2D>().isTrigger = true;

        newPrefab.AddComponent(typeof(Rigidbody2D));
        Rigidbody2D rigidbody = newPrefab.GetComponent<Rigidbody2D>();
        rigidbody.bodyType = RigidbodyType2D.Kinematic;

        newPrefab.AddComponent(typeof(SpriteRenderer));
        newPrefab.GetComponent<SpriteRenderer>().sortingOrder = 1;

        newPrefab.AddComponent(typeof(BasicAttack));

        string localPath = "Assets/Weapons/SpawnableObjects/New Melee.prefab";

        // Make sure the file name is unique, in case an existing Prefab has the same name.
        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

        // Create the new Prefab.
        PrefabUtility.SaveAsPrefabAssetAndConnect(newPrefab, localPath, InteractionMode.UserAction);
        Object.DestroyImmediate(newPrefab);

    }

    [MenuItem("Assets/Weapon/SpawnableObject/Ranged")]
    static void CreateRanged()
    {
        GameObject newPrefab = new GameObject();
        newPrefab = GameObject.Instantiate(newPrefab);
        newPrefab.name = "New Ranged SpawnableObject";

        newPrefab.AddComponent(typeof(BoxCollider2D));
        newPrefab.GetComponent<BoxCollider2D>().isTrigger = true;

        newPrefab.AddComponent(typeof(Rigidbody2D));
        Rigidbody2D rigidbody = newPrefab.GetComponent<Rigidbody2D>();
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        rigidbody.gravityScale = 0;

        newPrefab.AddComponent(typeof(SpriteRenderer));
        newPrefab.GetComponent<SpriteRenderer>().sortingOrder = 1;

        newPrefab.AddComponent(typeof(ProjectileAttack));

        string localPath = "Assets/Weapons/SpawnableObjects/New Ranged.prefab";

        // Make sure the file name is unique, in case an existing Prefab has the same name.
        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

        // Create the new Prefab.
        PrefabUtility.SaveAsPrefabAssetAndConnect(newPrefab, localPath, InteractionMode.UserAction);
        Object.DestroyImmediate(newPrefab);
    }

}
