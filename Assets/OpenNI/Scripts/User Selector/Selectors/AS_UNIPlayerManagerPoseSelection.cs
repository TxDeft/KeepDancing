using UnityEngine;
using System.Collections;
using OpenNI;
using System;

public class AS_UNIPlayerManagerPoseSelection : UNIPlayerManagerPoseSelection
{	
		
	/// true if the mapper is valid, false otherwise.
    public override bool Valid
    {
        get { return m_valid && base.Valid; }
    }
	
	
	protected override void InternalInit()
    {
		base.InternalInit();
		
        m_valid = false;
        if (base.Valid == false)
            return;
        if(m_settingManager.UserGenerator.IsPoseSupported(m_PoseToSelect) == false)
        {
            m_settingManager.Log("Selection pose "+m_PoseToSelect+" not supported by pose detection capability!", UNIEventLogger.Categories.Initialization, UNIEventLogger.Sources.UserSelector, UNIEventLogger.VerboseLevel.Errors);
            return;
        }
        if (m_PoseToUnselect != null && m_PoseToUnselect.CompareTo("")!=0 && m_settingManager.UserGenerator.IsPoseSupported(m_PoseToUnselect) == false)
        {
            m_settingManager.Log("Unselection pose " + m_PoseToUnselect + " not supported by pose detection capability!", UNIEventLogger.Categories.Initialization, UNIEventLogger.Sources.UserSelector, UNIEventLogger.VerboseLevel.Errors);
            return;
        }
        m_settingManager.UserGenerator.UserNode.PoseDetectionCapability.PoseDetected += new EventHandler<PoseDetectedEventArgs>(PoseDetectedCallback);
        m_settingManager.UserGenerator.UserNode.PoseDetectionCapability.OutOfPose += new EventHandler<OutOfPoseEventArgs>(OutOfPoseDetectedCallback);
        m_valid = true;
    }
	
	/// @summary pose detection callback
    /// @param sender who called the callback
    /// @param e the arguments of the event.
    private void PoseDetectedCallback(object sender, PoseDetectedEventArgs e)
    {
        m_settingManager.Log("found pose " + e.Pose + " for user=" + e.ID, UNIEventLogger.Categories.Callbacks, UNIEventLogger.Sources.Skeleton, UNIEventLogger.VerboseLevel.Verbose);

        if (e.Pose.CompareTo(m_PoseToSelect)!=0 && e.Pose.CompareTo(m_PoseToUnselect)!=0)
            return; // irrelevant pose

        UNIPlayerPoseCandidateObject poseUser = GetUserFromUserID(e.ID) as UNIPlayerPoseCandidateObject;
        if (poseUser == null)
            return; // irrelevant user
        if(e.Pose.CompareTo(m_PoseToSelect)==0)
        {
            DetectSelectionPoseForUser(poseUser);
        }
        if (e.Pose.CompareTo(m_PoseToUnselect)==0) // we do NOT put an else because the select and unselect might be the same pose
        {
            DetectUnselectionPoseForUser(poseUser);
        }
    }


    /// @summary out of pose detection callback    
    /// @param sender who called the callback
    /// @param e the arguments of the event.
    private void OutOfPoseDetectedCallback(object sender, OutOfPoseEventArgs e)
    {
        m_settingManager.Log("found out of pose " + e.Pose + " for user=" + e.ID, UNIEventLogger.Categories.Callbacks, UNIEventLogger.Sources.Skeleton, UNIEventLogger.VerboseLevel.Verbose);
        if (e.Pose.CompareTo(m_PoseToSelect)!=0 && e.Pose.CompareTo(m_PoseToUnselect)!=0)
            return; // irrelevant

        UNIPlayerPoseCandidateObject poseUser = GetUserFromUserID(e.ID) as UNIPlayerPoseCandidateObject;
        if (poseUser == null)
            return; // irrelevant user
 
        if (e.Pose.CompareTo(m_PoseToSelect)==0)
        {
            OutOfPoseSelectionPoseForUser(poseUser);
        }
        if (e.Pose.CompareTo(m_PoseToUnselect)==0) // we do NOT put an else because the select and unselect might be the same pose
        {
            OutOfPoseUnselectionPoseForUser(poseUser);
        }
    }
}
