using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefabs;

    private Queue<GameObject> prefabQueue = new Queue<GameObject>();


	// Use this for initialization
	void Start ()
    {
		for(int i = 0; i < prefabs.Length; i++)
        {
            prefabQueue.Enqueue(prefabs[i]);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnPrefabSwitchClick()
    {
        GameObject selectedPrefab = prefabQueue.Dequeue();
        FindObjectOfType<ObjectTrackable>().objectToSpawn = selectedPrefab;
        prefabQueue.Enqueue(selectedPrefab);
    }
}
