using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.Examples.Common;

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


        private List<AugmentedImage> augmentedImages = new List<AugmentedImage>();
        private bool createdMap = false;

        [SerializeField]
        private GameObject cubeGameObjectPrefab;

        private GameObject mapGameObject;

        [SerializeField]
        private Vector3 scaling = new Vector3(0.05f, 0.05f, 0.05f);
        void Start()
        {
            Physics.gravity = Vector3.one;
            //testMesh = testMeshFilter.mesh;
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


            Session.GetTrackables<AugmentedImage>(augmentedImages, TrackableQueryFilter.All);
            if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
            {
                if(createdMap)
                {
                    for (int i = 0; i < augmentedImages.Count; i++)
                    {
                        AugmentedImage image = augmentedImages[i];
                        //augmentedImages[i].CenterPose
                        //AugmentedImageVisualizer visualizer = null;
                        //m_Visualizers.TryGetValue(image.DatabaseIndex, out visualizer);
                        if (image.TrackingState == TrackingState.Tracking)
                        {
                            // Create an anchor to ensure that ARCore keeps tracking this augmented image.
                            Anchor anchor = image.CreateAnchor(image.CenterPose);
                            Debug.Log("Created one " + augmentedImages[i].Name + ":" + augmentedImages[i].DatabaseIndex);
                            Vector3 highPos = anchor.transform.position;
                            highPos.y += 1;

                            GameObject spawnedCube = Instantiate(cubeGameObjectPrefab, highPos,anchor.transform.rotation, anchor.transform);
                            Vector3 localScale = spawnedCube.transform.localScale;
                            spawnedCube.transform.SetParent(mapGameObject.transform);
                            spawnedCube.transform.localScale = localScale;
                            //spawnedCube.transform.position = augmentedImages[i].CenterPose.position;
                            //spawnedCube.transform.rotation = augmentedImages[i].CenterPose.rotation;


                            //visualizer = (AugmentedImageVisualizer)Instantiate(AugmentedImageVisualizerPrefab, anchor.transform);
                            //visualizer.Image = image;
                            //m_Visualizers.Add(image.DatabaseIndex, visualizer);
                        }
                        else if (image.TrackingState == TrackingState.Stopped)
                        {
                            Debug.Log("Remove");
                            //m_Visualizers.Remove(image.DatabaseIndex);
                            //GameObject.Destroy(visualizer.gameObject);
                        }
                        /*

                        */

                    }
                    /*
                    List<Vector3> points = new List<Vector3>();
                    for (int i = 0; i < Frame.PointCloud.PointCount; i++)
                    {
                        points.Add(Frame.PointCloud.GetPointAsStruct(i));
                    }
                    testMesh.vertices = points.ToArray();

                    int[] indices = new int[Frame.PointCloud.PointCount];
                    for (int i = 0; i < Frame.PointCloud.PointCount; i++)
                    {
                        indices[i] = i;
                    }
                    testMesh.SetIndices(indices, MeshTopology.Quads, 0);
                    */
                    //FindObjectOfType<PointcloudVisualizer>().scanPoints = true;
                    /*
                    Debug.Log("ToDo");
                    for (int i = 0; i < featurePoints.Count; i++)
                    {
                        Debug.Log(featurePoints[i].Pose.position + ":" + featurePoints[i].Pose.rotation);
                    }*/
                }
                else
                {
                    createdMap = true;
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
                        mapGameObject = Instantiate(prefab, hit.Pose.position, hit.Pose.rotation);

                        // Create an anchor to allow ARCore to track the hitpoint as understanding of the physical
                        // world evolves.
                        Anchor anchor = hit.Trackable.CreateAnchor(hit.Pose);

                        // Make model a child of the anchor.
                        mapGameObject.transform.parent = anchor.transform;
                        mapGameObject.transform.localScale = scaling;
                    }
                }
            }
        }
    }
}