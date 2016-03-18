using UnityEngine;
using System.Collections;

public class AS_UNIInput : UNIInput {

	/// initialization
	void Start () 
    {
       InteriorInit();
	}

	public override void InteriorInit ()
	{
		if (m_pointsTrackingManager == null)
            throw new System.Exception("Must define a hand tracking manager! - create a new game object and attach AS_UNIInputHandsManager script to it. Add some hand trackers and drag to the input");
        if (m_gestureManager == null)
            throw new System.Exception("Must define a gesture tracking manager! - create a new game object and attach AS_NIGestureManager script to it. Add some gesture factories and drag to the input");
        foreach (UNIAxis axis in m_axisList)
        {
            if (m_pointsTrackingManager.m_trackers.Length <= axis.m_sourceTrackerIndex)
                throw new System.Exception("1011: Axis " + axis.m_axisName + " has an illegal point tracker!");
            axis.m_sourceTracker = m_pointsTrackingManager.GetTracker(axis.m_sourceTrackerString);
            if (axis.m_sourceTracker == null)
                throw new System.Exception("Axis does not have a hand to follow!!!");

            if (axis.m_gestureIndex < m_gestureManager.m_gestures.Length)
            {
                axis.m_sourceGesture = m_gestureManager.m_gestures[axis.m_gestureIndex].GetGestureTracker(axis.m_sourceTracker);
                if (axis.m_sourceGesture == null)
                    throw new System.Exception("Gesture type does not match hand tracker type");
            }
            else
                axis.m_sourceGesture = null;
        }
	}
	
	/// makes sure we release everything on quit
    public void OnApplicationQuit()
    {
        Release();
    }

    /// makes sure we release everything on destroy
    public void OnDestroy()
    {
        Release();
    }
	
	
}
