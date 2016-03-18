using UnityEngine;

/// @summary This class implements a factory which allows us to receive a user pose based gesture and 
/// assign it to a hand.
public class UNIUserPoseGestureFactory : UNIGestureFactory
{
    /// this is the time between the first detection of the pose gesture and the
    /// time it is considered to have "clicked". This is used to make timed poses
    /// gestures. A value of 0 (or smaller) ignores the timing element.
    public float m_timeToHoldPose;

    public string m_poseName; /// The name of the pose to track

    /// @summary The OpenNI settings manager context which holds the relevant OpenNI nodes.
    public OpenNISettingsManager m_Context;

    /// returns a unique name for the gesture type.
    /// @note this is what is used to identify the factory
    /// @return the unique name.
    public override string GetGestureType()
    {
        return m_poseName+" pose gesture";
    }

    /// this creates the correct object implementation of the tracker
    /// @return the tracker object. 
    protected override UNIGestureTracker GetNewTrackerObject()
    {
        if (m_Context == null)
        {
            m_Context = FindObjectOfType(typeof(OpenNISettingsManager)) as OpenNISettingsManager;
        }
        UNIUserPoseDetector gestureTracker = new UNIUserPoseDetector(m_timeToHoldPose,m_poseName,m_Context);
        return gestureTracker;
    }
}
