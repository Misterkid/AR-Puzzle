using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private new Rigidbody rigidbody;
	// Use this for initialization
	void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(rigidbody != null)
        {
            rigidbody.AddForce((transform.forward * (2 * Time.deltaTime)));
        }
	}
}
