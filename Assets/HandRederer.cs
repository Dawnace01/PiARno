using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Microsoft;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;

public class HandRederer : MonoBehaviour
{
    public GameObject prefab;

    GameObject thumbObjectR, indexObjectR, middleObjectR, ringObjectR, pinkyObjectR;
    GameObject thumbObjectL, indexObjectL, middleObjectL, ringObjectL, pinkyObjectL;

    MixedRealityPose pose;

    // Start is called before the first frame update
    void Start()
    {
        thumbObjectR = Instantiate(prefab, this.transform);
        thumbObjectR.transform.name = "Sphere Thumb R";

        indexObjectR = Instantiate(prefab, this.transform);
        indexObjectR.transform.name = "Sphere Index R";

        middleObjectR = Instantiate(prefab, this.transform);
        middleObjectR.transform.name = "Sphere Middle R";

        ringObjectR = Instantiate(prefab, this.transform);
        ringObjectR.transform.name = "Sphere Ring R";

        pinkyObjectR = Instantiate(prefab, this.transform);
        pinkyObjectR.transform.name = "Sphere Pinky R";

        thumbObjectL = Instantiate(prefab, this.transform);
        thumbObjectL.transform.name = "Sphere Thumb L";

        indexObjectL = Instantiate(prefab, this.transform);
        indexObjectL.transform.name = "Sphere Index L";

        middleObjectL = Instantiate(prefab, this.transform);
        middleObjectL.transform.name = "Sphere Middle L";

        ringObjectL = Instantiate(prefab, this.transform);
        ringObjectL.transform.name = "Sphere Ring L";

        pinkyObjectL = Instantiate(prefab, this.transform);
        pinkyObjectL.transform.name = "Sphere Pinky L";
    }

    // Update is called once per frame
    void Update()
    {
        thumbObjectR.GetComponent<Renderer>().enabled = false;
        indexObjectR.GetComponent<Renderer>().enabled = false;
        middleObjectR.GetComponent<Renderer>().enabled = false;
        ringObjectR.GetComponent<Renderer>().enabled = false;
        pinkyObjectR.GetComponent<Renderer>().enabled = false;

        thumbObjectL.GetComponent<Renderer>().enabled = false;
        indexObjectL.GetComponent<Renderer>().enabled = false;
        middleObjectL.GetComponent<Renderer>().enabled = false;
        ringObjectL.GetComponent<Renderer>().enabled = false;
        pinkyObjectL.GetComponent<Renderer>().enabled = false;

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip,Handedness.Right,out pose))
        {
            thumbObjectR.GetComponent<Renderer>().enabled = true;
            thumbObjectR.transform.position = pose.Position;
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out pose))
        {
            indexObjectR.GetComponent<Renderer>().enabled = true;
            indexObjectR.transform.position = pose.Position;
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleTip, Handedness.Right, out pose))
        {
            middleObjectR.GetComponent<Renderer>().enabled = true;
            middleObjectR.transform.position = pose.Position;
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.RingTip, Handedness.Right, out pose))
        {
            ringObjectR.GetComponent<Renderer>().enabled = true;
            ringObjectR.transform.position = pose.Position;
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyTip, Handedness.Right, out pose))
        {
            pinkyObjectR.GetComponent<Renderer>().enabled = true;
            pinkyObjectR.transform.position = pose.Position;
        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, Handedness.Left, out pose))
        {
            thumbObjectL.GetComponent<Renderer>().enabled = true;
            thumbObjectL.transform.position = pose.Position;
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Left, out pose))
        {
            indexObjectL.GetComponent<Renderer>().enabled = true;
            indexObjectL.transform.position = pose.Position;
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleTip, Handedness.Left, out pose))
        {
            middleObjectL.GetComponent<Renderer>().enabled = true;
            middleObjectL.transform.position = pose.Position;
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.RingTip, Handedness.Left, out pose))
        {
            ringObjectL.GetComponent<Renderer>().enabled = true;
            ringObjectL.transform.position = pose.Position;
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyTip, Handedness.Left, out pose))
        {
            pinkyObjectL.GetComponent<Renderer>().enabled = true;
            pinkyObjectL.transform.position = pose.Position;
        }
    }
}
