using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

namespace ARPuzzle
{
    public class ArController : MonoBehaviour
    {

        List<DetectedPlane> detectedPlanes = new List<DetectedPlane>();

        private List<DetectedPlane> floorPlanes = new List<DetectedPlane>();
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            DetectFloor();
            //Session.GetTrackables<DetectedPlane>(detectedPlanes);
        }

        private void DetectObjects()
        {
            Debug.Log("ToDo");
        }

        private void DetectFloor()
        {
            Session.GetTrackables<DetectedPlane>(detectedPlanes);
            for (int i = 0; i < detectedPlanes.Count; i++)
            {
                Debug.Log(detectedPlanes[i].TrackingState);
                Debug.Log(detectedPlanes[i].CenterPose);
            }
        }
    }
}