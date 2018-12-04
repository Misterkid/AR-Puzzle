using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public delegate void OnFinishEvent();
    public event OnFinishEvent OnFinish;

    public delegate void OnDeathEvent();
    public event OnDeathEvent OnDeath;
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

    public void Reset()
    {
        transform.localPosition = startPosition;
        transform.localRotation = startRotation;
    }
    // Update is called once per frame
    void Update ()
    {
        transform.position = transform.position + (transform.forward * ((2 * transform.lossyScale.x )* Time.deltaTime));
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1 * transform.lossyScale.x))
        {
            Trigger trigger = hit.collider.GetComponent<Trigger>();
            if(trigger == null)
            {
                Vector3 reflect = Vector3.Reflect(transform.forward, hit.normal);
                //transform.LookAt(reflect);
                transform.rotation = Quaternion.LookRotation(reflect);
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        Trigger trigger = other.GetComponent<Trigger>();
        if(trigger != null)
        {
            switch (trigger.GetTriggerType)
            {
                case Trigger.TriggerType.DEATH:
                    if (OnDeath != null)
                        OnDeath();
                    break;

                case Trigger.TriggerType.FINISH:
                    if (OnFinish != null)
                        OnFinish();
                    break;

                default:

                    break;
            }

        }
    }
}
