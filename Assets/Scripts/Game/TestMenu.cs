using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMenu : MonoBehaviour
{
    private LevelManager levelManager;
	// Use this for initialization
	void Start ()
    {
        levelManager = FindObjectOfType<LevelManager>();

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnPrefabSwitchClick()
    {
        Character character = FindObjectOfType<Character>();
        if(character != null)
        {
            character.StartWalking();
        }
    }

    public void Enlarge()
    {
        levelManager = FindObjectOfType<LevelManager>();
        levelManager.ApplyScaling(0.001f);
    }

    public void Shrink()
    {
        levelManager = FindObjectOfType<LevelManager>();
        levelManager.ApplyScaling(-0.001f);
    }
}
