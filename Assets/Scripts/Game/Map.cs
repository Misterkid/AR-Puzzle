using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{

    public delegate void OnFinishEvent();
    public event OnFinishEvent OnFinish;

    public delegate void OnDeathEvent();
    public event OnDeathEvent OnDeath;

    [SerializeField]
    private Character character;
	// Use this for initialization
	void Start ()
    {
		if(character == null)
        {
            character = FindObjectOfType<Character>();
        }
        if(character != null)
        {
            character.OnDeath += Character_OnDeath;
            character.OnFinish += Character_OnFinish;
        }
	}

    private void Character_OnFinish()
    {
        if(OnFinish != null)
        {
            OnFinish();
        }
    }

    private void Character_OnDeath()
    {
        character.Reset();
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    public void OnDestroy()
    {
        character.OnDeath -= Character_OnDeath;
        character.OnFinish -= Character_OnFinish;
    }
}
