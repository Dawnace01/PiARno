using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using Microsoft;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;

public class FingerDetection : MonoBehaviour
{
    public GameObject key;
    public Color keyOriginalColor = Color.white;
    public Color keyModifiedColor = Color.cyan;
    MixedRealityPose poseThumbR,poseIndexR,poseMiddleR,poseRingR,posePinkyR;
    MixedRealityPose poseThumbL, poseIndexL, poseMiddleL, poseRingL, posePinkyL;

    private void Start(){  }

    private void Update()
    {
        key.GetComponent<MeshRenderer>().material.color = keyOriginalColor;
        /****** Right hand ******/
        // Thumb
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, Handedness.Right, out poseThumbR))
        {
            if (isCollision(poseThumbR.Position))
            {
                Debug.Log("Pouce droit");
                key.GetComponent<MeshRenderer>().material.color = keyModifiedColor;
            }
        }
        // Index
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out poseIndexR))
        {
            if (isCollision(poseIndexR.Position))
            {
                Debug.Log("Index droit");
                key.GetComponent<MeshRenderer>().material.color = keyModifiedColor;
            }
        }
        // Middle
        else if (HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleTip, Handedness.Right, out poseMiddleR))
        {
            if (isCollision(poseMiddleR.Position))
            {
                Debug.Log("Majeur droit");
                key.GetComponent<MeshRenderer>().material.color = keyModifiedColor;
            }
        }
        // Ring
        else if (HandJointUtils.TryGetJointPose(TrackedHandJoint.RingTip, Handedness.Right, out poseRingR))
        {
            if (isCollision(poseRingR.Position))
            {
                Debug.Log("Annulaire droit");
                key.GetComponent<MeshRenderer>().material.color = keyModifiedColor;
            }
        }
        // Pinky
        else if (HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyTip, Handedness.Right, out posePinkyR))
        {
            if (isCollision(posePinkyR.Position))
            {
                Debug.Log("Auriculaire droit");
                key.GetComponent<MeshRenderer>().material.color = keyModifiedColor;
            }
        }

        /****** Left hand ******/
        // Thumb
        else if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, Handedness.Left, out poseThumbL))
        {
            if (isCollision(poseThumbL.Position))
            {
                Debug.Log("Pouce gauche");
                key.GetComponent<MeshRenderer>().material.color = keyModifiedColor;
            }
        }
        // Index
        else if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Left, out poseIndexL))
        {
            if (isCollision(poseIndexL.Position))
            {
                Debug.Log("Index gauche");
                key.GetComponent<MeshRenderer>().material.color = keyModifiedColor;
            }
        }
        // Middle
        else if (HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleTip, Handedness.Left, out poseMiddleL))
        {
            if (isCollision(poseMiddleL.Position))
            {
                Debug.Log("Majeur gauche");
                key.GetComponent<MeshRenderer>().material.color = keyModifiedColor;
            }
        }
        // Ring
        else if (HandJointUtils.TryGetJointPose(TrackedHandJoint.RingTip, Handedness.Left, out poseRingL))
        {
            if (isCollision(poseRingL.Position))
            {
                Debug.Log("Annulaire gauche");
                key.GetComponent<MeshRenderer>().material.color = keyModifiedColor;
            }
        }
        // Pinky
        else if (HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyTip, Handedness.Left, out posePinkyL))
        {
            if (isCollision(posePinkyL.Position))
            {
                Debug.Log("Auriculaire gauche");
                key.GetComponent<MeshRenderer>().material.color = keyModifiedColor;
            }
        }


    }

    private bool isCollision(Vector3 current)
    {
        Vector3 button = key.transform.position;

        float halfWidth = key.transform.localScale.x / 2,
            halfHeight = key.transform.localScale.y / 2,
            halfDeep = key.transform.localScale.z / 2;

        return (
                (current.x >= (button.x - halfWidth)) && (current.x <= (button.x + halfWidth))
                &&
                (current.y >= (button.y - halfHeight)) && (current.y <= (button.y + halfHeight))
                &&
                (current.z >= (button.z - halfDeep)) && (current.z <= (button.z + halfDeep))
            );
    }
}
