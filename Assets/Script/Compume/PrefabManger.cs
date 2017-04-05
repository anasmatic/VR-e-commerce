using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManger : MonoBehaviour {

    private static PrefabManger prefabManger;

    public List<GameObject> prefabs = new List<GameObject>();
    public Transform standsContainer;
    public List<Vector3> standsPosList = new List<Vector3>();
    public List<Vector3> standsScaleList = new List<Vector3>();
    private static List<GameObject> prefabsList = new List<GameObject>();
    public const string DISPLAYCUBE = "displaycube";
    public const string DISPLAYCYLINDER = "displaycylinder";
    public const string DISPLAYHYPERRECTANGLE = "displayhyperrectangle";
    public const string DISPLAYSPHERE = "displaysphere";

    public static PrefabManger instance
    {
        get
        {
            if (!prefabManger)
            {
                prefabManger = FindObjectOfType(typeof(PrefabManger)) as PrefabManger;

                if (!prefabManger)
                {
                    Debug.LogError("There needs to be one active EventManger script on a Transform in your scene.");
                }
                else
                {
                    prefabManger.Init();
                }
            }

            return prefabManger;
        }
    }

    void Awake()
    {
        //Init();
    }

    // Use this for initialization
    void Init () {
        prefabsList = instance.prefabs;
        //save stands positions
        if (standsContainer)
        {
            foreach (Transform stand in standsContainer)
            {
                float numRandom = Random.Range(0, 1f) + 0.5f;
                stand.localScale = new Vector3(1, stand.localScale.y * numRandom, 1);
                standsPosList.Add(stand.localPosition);
                standsScaleList.Add(stand.localScale);
            }
        }
    }

    public GameObject[] getRandomSetOfPrefabs(int size)
    {
        GameObject[] prefabsSet = new GameObject[size];
        System.Random random = new System.Random();
        for (int i = 0; i< size; i++){
            //print("count:"+ prefabsList.Count);
            int randomPrefabIndex = random.Next(prefabsList.Count);
            prefabsSet[i] = prefabsList[randomPrefabIndex];
        }
        return prefabsSet;
    }
}
