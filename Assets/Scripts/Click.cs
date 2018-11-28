using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        Physics.IgnoreCollision(GetComponent<Collider>(), collision.collider);
    }
    private void OnMouseDown()
    {
        transform.parent.SendMessage("OnMouseDown");
    }
}
