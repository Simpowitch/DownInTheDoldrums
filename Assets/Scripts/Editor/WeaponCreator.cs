using UnityEngine;
using UnityEditor;
public class WeaponCreator
{
    const int IGNORELAYER = 8;

   [MenuItem("Assets/Weapon/New Weapon")]
    static void CreateWeapon()
    {
        //New Prefab
        GameObject newPrefab = new GameObject();
        newPrefab = GameObject.Instantiate(newPrefab);
        newPrefab.name = "New Weapon";
        newPrefab.gameObject.layer = IGNORELAYER;

        //SpriteRenderer
        newPrefab.AddComponent(typeof(SpriteRenderer));
        newPrefab.GetComponent<SpriteRenderer>().sortingOrder = 1;

        //Add weapon script
        newPrefab.AddComponent(typeof(Weapon));

        string localPath = "Assets/Weapons/New Weapon.prefab";

        // Make sure the file name is unique, in case an existing Prefab has the same name.
        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

        // Create the new Prefab.
        PrefabUtility.SaveAsPrefabAssetAndConnect(newPrefab, localPath, InteractionMode.UserAction);
        Object.DestroyImmediate(newPrefab);
    }

    [MenuItem("Assets/Weapon/SpawnableObject/Melee")]
    static void CreateMelee()
    {
        //New Prefab
        GameObject newPrefab = new GameObject();
        newPrefab = GameObject.Instantiate(newPrefab);
        newPrefab.name = "New Melee SpawnableObject";
        newPrefab.gameObject.layer = IGNORELAYER;

        //collider
        newPrefab.AddComponent(typeof(BoxCollider2D));
        newPrefab.GetComponent<BoxCollider2D>().isTrigger = true;

        //Rigidbody
        newPrefab.AddComponent(typeof(Rigidbody2D));
        Rigidbody2D rigidbody = newPrefab.GetComponent<Rigidbody2D>();
        rigidbody.bodyType = RigidbodyType2D.Kinematic;

        //SpriteRenderer
        newPrefab.AddComponent(typeof(SpriteRenderer));
        newPrefab.GetComponent<SpriteRenderer>().sortingOrder = 1;

        //Add weapon script
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
        //New Prefab
        GameObject newPrefab = new GameObject();
        newPrefab = GameObject.Instantiate(newPrefab);
        newPrefab.name = "New Ranged SpawnableObject";
        newPrefab.gameObject.layer = IGNORELAYER;

        //Collider
        newPrefab.AddComponent(typeof(BoxCollider2D));
        newPrefab.GetComponent<BoxCollider2D>().isTrigger = true;

        //Rigidbody
        newPrefab.AddComponent(typeof(Rigidbody2D));
        Rigidbody2D rigidbody = newPrefab.GetComponent<Rigidbody2D>();
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        rigidbody.gravityScale = 0;

        //SpriteRenderer
        newPrefab.AddComponent(typeof(SpriteRenderer));
        newPrefab.GetComponent<SpriteRenderer>().sortingOrder = 1;

        //Add weapon script
        newPrefab.AddComponent(typeof(ProjectileAttack));


        string localPath = "Assets/Weapons/SpawnableObjects/New Ranged.prefab";

        // Make sure the file name is unique, in case an existing Prefab has the same name.
        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

        // Create the new Prefab.
        PrefabUtility.SaveAsPrefabAssetAndConnect(newPrefab, localPath, InteractionMode.UserAction);
        Object.DestroyImmediate(newPrefab);
    }
}
