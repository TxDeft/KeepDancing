using UnityEngine;
using System.Collections;

public class UNISteadySkeletonGestureFactory : UNIGestureFactory {

	/// this is the time between the first detection of a steady gesture and the
    /// time it is considered to have "clicked". 
    public float m_timeToClick;
    /// this is the time after the "click" event when we reset the steady (i.e. as if we got
    /// a not steady event). 
    public float m_timeToReset;


    /// this is the time (in seconds) we check over to see a steady/not steady result.
    public float m_steadyTestTime=0.5f;

    /// This holds the maximum threshold to be considered steady. 
    public float m_steadyStdSqrThreshold=2;
    /// This holds the minimum threshold to be considered not steady.
    public float m_unsteadyStdSqrThreshold=8;

    /// this holds the maximum we allow to move from the first steady (in mm) before it is considered
    /// unsteady
    public float m_maxMoveFromFirstSteady=40;


    /// returns a unique name for the gesture type.
    /// @return the unique name.
    public override string GetGestureType()
    {
        return "SteadySkeletonGesture";
    }

    /// this creates the correct object implementation of the tracker
    /// @return the tracker object. 
    protected override UNIGestureTracker GetNewTrackerObject()
    {
        UNISteadySkeletonHandDetector gestureTracker=new UNISteadySkeletonHandDetector(m_timeToClick, m_timeToReset,m_steadyTestTime);
        gestureTracker.m_steadyStdSqrThreshold=m_steadyStdSqrThreshold; 
					   
        gestureTracker.m_unsteadyStdSqrThreshold=m_unsteadyStdSqrThreshold;
        gestureTracker.m_maxMoveFromFirstSteady = m_maxMoveFromFirstSteady;
        return gestureTracker;
    }
}
