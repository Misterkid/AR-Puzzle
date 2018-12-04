using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public enum TriggerType
    {
        NONE,
        DEATH,
        FINISH,
        JUMP
    }
    [SerializeField]
    private TriggerType triggerType;

    public TriggerType GetTriggerType { get { return triggerType; } }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
