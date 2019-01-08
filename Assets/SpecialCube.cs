using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCube : MonoBehaviour
{

    [SerializeField]
    private AudioClip appearAudioClip;

    [SerializeField]
    private AudioClip hitAudioClip;


    // Use this for initialization
    void Start ()
    {
        if (appearAudioClip != null)
            AudioSource.PlayClipAtPoint(appearAudioClip, transform.position);

        GameObject gameObject = new GameObject();


    }
	
	// Update is called once per frame
	void Update ()
    {

        /*
        if(transform.position.y < 0)
        {
            Destroy(this.gameObject);
        }
        */
	}

    private void OnMouseDown()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hitAudioClip != null)
            AudioSource.PlayClipAtPoint(hitAudioClip, transform.position);
    }
}
