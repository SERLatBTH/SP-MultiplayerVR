using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Normal.Realtime;

public class LaserPointer : MonoBehaviour {
    // Reference to Realtime to use to instantiate laser pointer.
    [SerializeField] private Realtime _realtime = null;

    // Which hand should this brush instance track?
    private enum Hand { LeftHand, RightHand };
    [SerializeField] private Hand _hand = Hand.RightHand;

    // Used to keep track of the current brush tip position and the actively drawing brush stroke
    private Vector3     _handPosition;
    private Quaternion  _handRotation;
    //Laser stuff
    public GameObject laserPre;
    private GameObject laserPointer;
    private Transform laserTransform;
    private Vector3 hitPoint;

    private void Start()
    {
        laserPointer = Instantiate(laserPre);
        // 2
        laserTransform = laserPointer.transform;
    }
    private void Update() {
        // If not connected to room, do nothing!
        if (!_realtime.connected)
            return;

        // Start by figuring out which hand we're tracking
        XRNode node    = _hand == Hand.LeftHand ? XRNode.LeftHand : XRNode.RightHand;
        string trigger = _hand == Hand.LeftHand ? "Left Trigger" : "Right Trigger";

        // Get the position & rotation of the hand
        bool handIsTracking = UpdatePose(node, ref _handPosition, ref _handRotation);

        // Figure out if the trigger is pressed or not
        bool triggerPressed = Input.GetAxisRaw(trigger) > 0.1f;

        // If we lose tracking, stop drawing
        if (!handIsTracking)
            triggerPressed = false;
        //If trigger is down, then draw pointer!
        if (triggerPressed)
        {
            //Create a raycst hit.
            RaycastHit hitPoint;

            // If we have a hit, then do draw lazer from hand to hitPoint.
            if (Physics.Raycast(_handPosition, (_handRotation*Vector3.forward), out hitPoint, 300))
            {
                this.hitPoint = hitPoint.point;
                ShowLaser(hitPoint);
            }
        }
        else //Laser does not hit so do not draw it!
        {
            laserPointer.SetActive(false);
        }
    }

    //Function to draw the laser pointer at a hitpoint.
    private void ShowLaser(RaycastHit hit)
    {
        laserPointer.SetActive(true);
        laserTransform.position = Vector3.Lerp(_handPosition, hitPoint, .5f);
        laserTransform.LookAt(hitPoint); //Aim it at the hitpoint
        laserTransform.localScale = new Vector3(laserTransform.localScale.x,
                                                laserTransform.localScale.y,
                                                hit.distance);
    }
    //// Utility

    // Given an XRNode, get the current position & rotation. If it's not tracking, don't modify the position & rotation.
    private static bool UpdatePose(XRNode node, ref Vector3 position, ref Quaternion rotation) {
        List<XRNodeState> nodeStates = new List<XRNodeState>();
        InputTracking.GetNodeStates(nodeStates);

        foreach (XRNodeState nodeState in nodeStates) {
            if (nodeState.nodeType == node) {
                Vector3    nodePosition;
                Quaternion nodeRotation;
                bool gotPosition = nodeState.TryGetPosition(out nodePosition);
                bool gotRotation = nodeState.TryGetRotation(out nodeRotation);

                if (gotPosition)
                    position = nodePosition;
                if (gotRotation)
                    rotation = nodeRotation;

                return gotPosition;
            }
        }

        return false;
    }
}
