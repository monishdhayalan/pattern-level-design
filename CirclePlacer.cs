using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CirclePlacer : EditorWindow
{
    // The prefab that you want to place in a circle
    public GameObject prefab;

    // The radius of the circle
    public float radius = 1.0f;

    // The number of prefabs to place in the circle
    public int count = 10;

    // The starting angle for the first prefab
    public float startingAngle = 0.0f;

    // The space between each prefab in the circle
    public float spacing = 10.0f;

    // The parent transform for the prefabs
    public Transform parent;

    public List<GameObject> spawnedArray;

    [MenuItem("LevelCreator/Creator")]
    public static void ShowWindow()
    {
        GetWindow<CirclePlacer>("Circle Placer");
    }
    
    
    private void OnGUI()
    {
        //parent = (Transform)EditorGUILayout.ObjectField("PreExisting Parent: ", parent, typeof(Transform), true);
        
        GUILayout.Label("Hello", EditorStyles.boldLabel);
        EditorGUI.BeginChangeCheck();
        
        prefab = (GameObject)EditorGUILayout.ObjectField("Prefab: ", prefab, typeof(GameObject), false);
        radius = EditorGUILayout.FloatField("Radius: ", radius);
        if (parent == null)
        {
            count = EditorGUILayout.IntField("Count: ", count);
        }

        if (EditorGUI.EndChangeCheck() && parent != null && parent.childCount > 0)
        {
            //Debug.Log(XSpacing);
            PrefabPlacerCode();
            //PlaceInCircle();
        }
        //LevelDesignTexture = (Texture2D)EditorGUILayout.ObjectField("Image: ", LevelDesignTexture, typeof(Texture2D), false);
        
        
        
        if (parent == null && GUILayout.Button("CREATE!"))
        {
            SpawnGameObjects();
        }
    }

    public void SpawnGameObjects()
    {
        spawnedArray = new List<GameObject>();
        GameObject tempParent = new GameObject("Parent");
        parent = tempParent.transform;
        for (int i = 0; i < count; i++)
        {
            GameObject go = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            go.transform.SetParent(tempParent.transform);
            spawnedArray.Add(go);
        }
        PrefabPlacerCode();
    }
    
    
    
    void PrefabPlacerCode()
    {
        float radiusnew = (parent.childCount * radius) / (2 * Mathf.PI);
        for (int i = 0; i < parent.childCount; i++)
        {
            float angle = (360 / parent.childCount) * i;
            
            float x = radiusnew * Mathf.Cos(angle * Mathf.Deg2Rad);
            float z = radiusnew * Mathf.Sin(angle * Mathf.Deg2Rad);

            parent.GetChild(i).transform.position = new Vector3(x, 0, z);
        }
    }
}
