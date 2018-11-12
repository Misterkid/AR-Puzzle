using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

namespace ARPuzzle
{
    /// <summary>
    /// Our AR controller, currently mostly from google example code.
    /// </summary>
    public class ArController : MonoBehaviour
    {
        [SerializeField]
        private Camera firstPersonCamera;

        [SerializeField]
        private GameObject characterPrefab;

        private List<DetectedPlane> detectedPlanes = new List<DetectedPlane>();

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            // Exit the app when the 'back' button is pressed.
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }

            // If the player has not touched the screen, we are done with this update.
            Touch touch;
            if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
            {
                return;
            }
            // Raycast against the location the player touched to search for planes.
            TrackableHit hit;
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
                TrackableHitFlags.FeaturePointWithSurfaceNormal;

            if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
            {
                // Use hit pose and camera pose to check if hittest is from the
                // back of the plane, if it is, no need to create the anchor.
                if ((hit.Trackable is DetectedPlane) &&
                    Vector3.Dot(firstPersonCamera.transform.position - hit.Pose.position,
                        hit.Pose.rotation * Vector3.up) < 0)
                {
                    Debug.Log("Hit at back of the current DetectedPlane");
                }
                else
                {
                    GameObject prefab = characterPrefab;
                    // Instantiate character model at the hit pose.
                    GameObject characterGameObject = Instantiate(prefab, hit.Pose.position, hit.Pose.rotation);

                    // Create an anchor to allow ARCore to track the hitpoint as understanding of the physical
                    // world evolves.
                    Anchor anchor = hit.Trackable.CreateAnchor(hit.Pose);

                    // Make model a child of the anchor.
                    characterGameObject.transform.parent = anchor.transform;
                }
            }
        }
    }
}