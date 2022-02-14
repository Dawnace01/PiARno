using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using Microsoft;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;

using PianoUtilities;

public class FingerDetection : MonoBehaviour
{ 
    public GameObject key;
    MixedRealityPose poseThumbR, poseIndexR, poseMiddleR, poseRingR, posePinkyR;
    MixedRealityPose poseThumbL, poseIndexL, poseMiddleL, poseRingL, posePinkyL;

    private void Start()
    {
        //key = this.transform.parent.gameObject;
        Debug.LogWarning(key.name);
    }

    private void Update()
    {
        setKeyStatus(false, Hand.NONE, Fingering.NONE);
        /****** Right hand ******/
        // Thumb
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, Handedness.Right, out poseThumbR))
        {
            if (isCollision(poseThumbR.Position))
            {
                setKeyStatus(true, Hand.RIGHT, Fingering.ONE);
            }
        }
        // Index
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out poseIndexR))
        {
            if (isCollision(poseIndexR.Position))
            {
                setKeyStatus(true, Hand.RIGHT, Fingering.TWO);
            }
        }
        // Middle
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleTip, Handedness.Right, out poseMiddleR))
        {
            if (isCollision(poseMiddleR.Position))
            {
                setKeyStatus(true, Hand.RIGHT, Fingering.THREE);
            }
        }
        // Ring
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.RingTip, Handedness.Right, out poseRingR))
        {
            if (isCollision(poseRingR.Position))
            {
                setKeyStatus(true, Hand.RIGHT, Fingering.FOUR);
            }
        }
        // Pinky
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyTip, Handedness.Right, out posePinkyR))
        {
            if (isCollision(posePinkyR.Position))
            {
                setKeyStatus(true, Hand.RIGHT, Fingering.FIVE);
            }
        }

        /****** Left hand ******/
        // Thumb
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, Handedness.Left, out poseThumbL))
        {
            if (isCollision(poseThumbL.Position))
            {
                setKeyStatus(true, Hand.LEFT, Fingering.ONE);
            }
        }
        // Index
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Left, out poseIndexL))
        {
            if (isCollision(poseIndexL.Position))
            {
                setKeyStatus(true, Hand.LEFT, Fingering.TWO);
            }
        }
        // Middle
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleTip, Handedness.Left, out poseMiddleL))
        {
            if (isCollision(poseMiddleL.Position))
            {
                setKeyStatus(true, Hand.LEFT, Fingering.THREE);
            }
        }
        // Ring
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.RingTip, Handedness.Left, out poseRingL))
        {
            if (isCollision(poseRingL.Position))
            {
                setKeyStatus(true, Hand.LEFT, Fingering.FOUR);
            }
        }
        // Pinky
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyTip, Handedness.Left, out posePinkyL))
        {
            if (isCollision(posePinkyL.Position))
            {
                setKeyStatus(true, Hand.LEFT, Fingering.FIVE);
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

    private void setKeyStatus(bool isPressed, Hand currentHand, Fingering currentFingering)
    {        
        key.GetComponent<KeyStates>().isKeyPressed = isPressed;
        // ajout du déclencheur de l'action
        key.GetComponent<KeyStates>().keyCurrentFinger = currentFingering;
        key.GetComponent<KeyStates>().keyCurrentHand = currentHand;
    }
}
