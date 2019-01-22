using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Actions actions;
    public delegate void OnFinishEvent();
    public event OnFinishEvent OnFinish;

    public delegate void OnDeathEvent();
    public event OnDeathEvent OnDeath;
    //private new Rigidbody rigidbody;
    // Use this for initialization
    private Vector3 startPosition;
    private Quaternion startRotation;

    [SerializeField]
    private float speed = 2;

    [SerializeField]
    private Transform rayCastTransform;
    private bool isWalking = false;

    [SerializeField]
    private AudioClip deathAudioClip;

    [SerializeField]
    private AudioClip oofAudioClip;


    [SerializeField]
    private AudioClip yayAudioClip;

    private bool isDead = false;

    [SerializeField]
    private float raycastDistance = 0.30f;

    void Start()
    {
        //rigidbody = GetComponent<Rigidbody>();
        startPosition = transform.localPosition;
        startRotation = transform.localRotation;
        actions = GetComponent<Actions>();
        actions.Stay();
    }

    public void Reset()
    {
        transform.localPosition = startPosition;
        transform.localRotation = startRotation;
        actions.Stay();
        isWalking = false;
        GetComponent<Rigidbody>().useGravity = true;
        isDead = false;
        this.gameObject.SetActive(true);
    }
    public void StartWalking()
    {
        isWalking = true;
        actions.Walk();
    }
    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {
            transform.position = transform.position + (transform.forward * ((speed * transform.lossyScale.x) * Time.deltaTime));
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(rayCastTransform.position, rayCastTransform.TransformDirection(Vector3.forward), out hit, raycastDistance * transform.lossyScale.x))
            {
                Trigger trigger = hit.collider.GetComponent<Trigger>();
                if (trigger == null)
                {
                    if (oofAudioClip != null)
                        AudioSource.PlayClipAtPoint(oofAudioClip, transform.position);

                    actions.Damage();
                    actions.Walk();
                    Vector3 reflect = Vector3.Reflect(transform.forward, hit.normal);
                    //transform.LookAt(reflect);
                    transform.rotation = Quaternion.LookRotation(reflect);
                }
            }
        }

        /*
        if(transform.position.y < 0.05f && !isDead)
        {
            isDead = true;
            GetComponent<Rigidbody>().useGravity = false;
            isWalking = false;
            StartCoroutine(Death());
        }*/

    }

    private void OnTriggerEnter(Collider other)
    {

        if (isDead)
            return;

        Trigger trigger = other.GetComponent<Trigger>();
        if (trigger != null)
        {
            switch (trigger.GetTriggerType)
            {
                case Trigger.TriggerType.DEATH:
                    isDead = true;
                    GetComponent<Rigidbody>().useGravity = false;
                    isWalking = false;
                    StartCoroutine(Death());
                    break;

                case Trigger.TriggerType.FINISH:
                    GetComponent<Rigidbody>().useGravity = false;
                    isWalking = false;

                    StartCoroutine(Victory());
                    break;

                default:

                    break;
            }

        }
    }

    private IEnumerator Victory()
    {
        actions.Jump();
        if (yayAudioClip != null)
            AudioSource.PlayClipAtPoint(yayAudioClip, transform.position);

        yield return new WaitForSeconds(3.5f);
        if (OnFinish != null)
            OnFinish();
    }

    private IEnumerator Death()
    {
        actions.Death();

        if (deathAudioClip != null)
            AudioSource.PlayClipAtPoint(deathAudioClip, transform.position);

        yield return new WaitForSeconds(3.5f);
        this.gameObject.SetActive(false);
        if (OnDeath != null)
            OnDeath();
    }

}
