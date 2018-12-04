using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class GroundPlaneTrackable : DefaultTrackableEventHandler
{

    [SerializeField]
    private GameObject gameObjectToHide;

    protected override void Start()
    {
        base.Start();
    }

    public override void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        base.OnTrackableStateChanged(previousStatus, newStatus);
    }

    protected override void OnTrackingFound()
    {
        gameObjectToHide.gameObject.SetActive(true);
        //base.OnTrackingFound();
    }

    protected override void OnTrackingLost()
    {
        gameObjectToHide.gameObject.SetActive(false);
        //base.OnTrackingLost();
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    public void OnContentPlaced()
    {
        FindObjectOfType<AnchorInputListenerBehaviour>().enabled = false;
    }
}
