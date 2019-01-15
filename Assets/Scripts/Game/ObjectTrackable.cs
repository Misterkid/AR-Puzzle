using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ObjectTrackable : DefaultTrackableEventHandler
{
    [SerializeField]
    public GameObject objectToSpawn;

    private bool isFound = false;
    //private Map map;

    private LevelManager levelManager;

    private GameObject highLightObject;
    // Use this for initialization
    protected override void Start()
    {

        levelManager = Resources.FindObjectsOfTypeAll<LevelManager>()[0];//Find<LevelManager>()
        
        highLightObject = Instantiate(transform.GetChild(0).gameObject);
        Destroy(highLightObject.GetComponent<Click>());
        Destroy(highLightObject.GetComponent<Collider>());
        highLightObject.SetActive(false);

        base.Start();
    }
	
	// Update is called once per frame
	void Update ()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,Vector3.down,out hit,200))
        {
            Map map = hit.collider.GetComponent<Map>();
            if(map != null)
            {
                if(!highLightObject.activeSelf)
                    highLightObject.SetActive(true);

                highLightObject.transform.SetParent(map.transform);
                highLightObject.transform.localScale = GetComponentInChildren<Click>().transform.lossyScale;
                transform.position = hit.point;
            }
            else
            {
                highLightObject.SetActive(false);
            }

        }
	}

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();

        isFound = true;

    }
    public override void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, 
        TrackableBehaviour.Status newStatus)
    {

        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;

        Debug.Log(newStatus);
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED)// ||
            //newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
        // base.OnTrackableStateChanged(previousStatus, newStatus);

    }
    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        isFound = false;
        highLightObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        GameObject spawnedObject = Instantiate(objectToSpawn, null);
        Vector3 higherPos = transform.position;


        //spawnedObject.transform.localScale = spawnedObject.transform.localScale * levelManager.Scale;
        spawnedObject.transform.localScale = GetComponentInChildren<Click>().transform.lossyScale;
        higherPos.y += (spawnedObject.transform.localScale.y * 2);//levelManager.Scale;

        //transform.localScale;
        spawnedObject.transform.position = higherPos;
        spawnedObject.transform.rotation = transform.rotation;

        Map map = FindObjectOfType<Map>();
        if (map != null)
        {
            spawnedObject.transform.SetParent(map.transform);
        }
    }
}
