using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //private new Rigidbody rigidbody;
    // Use this for initialization
    private Vector3 startPosition;
    private Quaternion startRotation;
	void Start ()
    {
        //rigidbody = GetComponent<Rigidbody>();
        startPosition = transform.localPosition;
        startRotation = transform.localRotation;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetMouseButtonDown(0))
        {
            transform.localPosition = startPosition;
            transform.localRotation = startRotation;
        }
        //if (rigidbody != null)
        {
            //rigidbody.AddForce((transform.forward * (2 * Time.deltaTime)));
            //transform.Translate(transform.forward * ((1*transform.lossyScale.x) * Time.deltaTime));

            transform.position = transform.position + (transform.forward * ((2 * transform.lossyScale.x )* Time.deltaTime));
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1 * transform.lossyScale.x))
            {
                Vector3 reflect = Vector3.Reflect(transform.forward, hit.normal);
                //transform.LookAt(reflect);
                transform.rotation = Quaternion.LookRotation(reflect);
            }
        }
	}
}
