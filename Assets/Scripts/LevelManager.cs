using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private List<Map> levels = new List<Map>();
    [SerializeField]
    private float scaling = 0.05f;

    public float Scale { get { return scaling; } }

    private int levelIndex = 0;
    private Map spawnedMap;
	// Use this for initialization
	void Start ()
    {
        Physics.gravity *= (scaling * 2);
        SpawnLevel();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void SpawnLevel()
    {
        spawnedMap = Instantiate(levels[levelIndex],transform);
        spawnedMap.transform.localScale *= scaling;

        spawnedMap.OnFinish += SpawnedMap_OnFinish;
    }

    private void SpawnedMap_OnFinish()
    {
        spawnedMap.OnFinish -= SpawnedMap_OnFinish;
        levelIndex++;

        if (levelIndex >= levels.Count)
            levelIndex = 0;

        Destroy(spawnedMap.gameObject);
        SpawnLevel();
    }
}
