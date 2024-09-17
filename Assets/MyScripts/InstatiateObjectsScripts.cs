using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;
using UnityEngine.Rendering;
public class InstatiateObjectsScripts : MonoBehaviour
{
    public GameObject ObjectsToBePlaced;

    public ARPlaneManager planeManager;
    public ARRaycastManager raycastManager;
    private List<ARRaycastHit> aRRaycastHits = new List<ARRaycastHit>();

    private void Awake()
    {
        planeManager = GetComponent<ARPlaneManager>();
        raycastManager = GetComponent<ARRaycastManager>();
    }

    private void OnEnable()
    {
        Debug.Log("Object is Enabled");
        UnityEngine.InputSystem.EnhancedTouch.TouchSimulation.Enable();
        UnityEngine.InputSystem.EnhancedTouch.EnhancedTouchSupport.Enable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += FingerTouchDetected;
    }
    private void OnDisable()
    {
        Debug.Log("Object is Diasabled");
        UnityEngine.InputSystem.EnhancedTouch.TouchSimulation.Disable();
        UnityEngine.InputSystem.EnhancedTouch.EnhancedTouchSupport.Disable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= FingerTouchDetected;
    }

    void FingerTouchDetected(UnityEngine.InputSystem.EnhancedTouch.Finger fingerTouch)
    {
        if (fingerTouch.index != 0 )
        {
            return;
        }

        if (raycastManager.Raycast(fingerTouch.currentTouch.screenPosition,aRRaycastHits, TrackableType.PlaneWithinPolygon))
        {

            foreach(ARRaycastHit hit in aRRaycastHits)
            {
                Pose orientation = hit.pose;
                GameObject spawnedObject = Instantiate(ObjectsToBePlaced, orientation.position, orientation.rotation);
            }
        }
    }

}









