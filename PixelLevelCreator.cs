using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting.FullSerializer;

public class PixelLevelCreator : EditorWindow
{
    public Texture2D LevelDesignTexture;
    public GameObject SpawnPrefab;
    public float XSpacing;
    public float Zspacing;


    GameObject[,] spawnedArray;

    

    [MenuItem("LevelCreator/Creator")]
    public static void ShowWindow()
    {
        GetWindow<PixelLevelCreator>("Pixel Art Creator");
    }

    private void OnGUI()
    {

        GUILayout.Label("Hello", EditorStyles.boldLabel);
        EditorGUI.BeginChangeCheck();
        XSpacing = EditorGUILayout.FloatField("X: ", XSpacing);
        Zspacing = EditorGUILayout.FloatField("Z: ", Zspacing);
        if (EditorGUI.EndChangeCheck() && spawnedArray != null)
        {
            Debug.Log(XSpacing);
            FixPos();
        }
        LevelDesignTexture = (Texture2D)EditorGUILayout.ObjectField("Image: ", LevelDesignTexture, typeof(Texture2D), false);
        SpawnPrefab = (GameObject)EditorGUILayout.ObjectField("Prefab: ", SpawnPrefab, typeof(GameObject), false);



        if (GUILayout.Button("CREATE!"))
        {
            DrawImage();            
        }
    }

    private void DrawImage()
    {
        Vector3 nextspawnPos = Vector3.zero;
        GameObject Level = new GameObject("Level");

        //Debug.Log(LevelDesignTexture.width + ", "+ LevelDesignTexture.height);
        spawnedArray = new GameObject[LevelDesignTexture.width, LevelDesignTexture.height];
        
        for(int j = 0; j < LevelDesignTexture.height; j++)
        {
            for(int i = 0; i < LevelDesignTexture.width; i++)
            {
                nextspawnPos.x += XSpacing;                
                if (LevelDesignTexture.GetPixel(i, j).a == 0)
                    continue;
                GameObject obj = Instantiate(SpawnPrefab, nextspawnPos, Quaternion.identity);
                obj.transform.parent = Level.transform;
                spawnedArray[i, j] = obj;
            }
            nextspawnPos.z += Zspacing;
            nextspawnPos.x = 0;
        }


    }


    private void FixPos()
    {
        Vector3 nextspawnPos = Vector3.zero;
        for (int j = 0; j < LevelDesignTexture.height; j++)
        {
            for (int i = 0; i < LevelDesignTexture.width; i++)
            {
                nextspawnPos.x += XSpacing;
                if (LevelDesignTexture.GetPixel(i, j).a == 0)
                    continue;
                spawnedArray[i, j].transform.position = nextspawnPos;
            }
            nextspawnPos.z += Zspacing;
            nextspawnPos.x = 0;
        }
    }


}
