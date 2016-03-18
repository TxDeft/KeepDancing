using UnityEngine;
using System.Collections;
using OpenNI;

public class AS_UNISkeletonTracker  : UNISkeletonTracker {
	

	//mono-behavior fixedUpdate
	//this is used to update the position for the previous frame.
	public override void FixedUpdate()
	{
		
		UNISelectedPlayer player = m_playerManager.GetPlayer(m_playerNum);
		if(player == null || player.Valid == false || player.Tracking == false)
			return;//no player to work 
		
		SkeletonJointPosition skeletJointPos;
		if(player.GetSkeletonJointPosition(m_jointToUse,out skeletJointPos))
		{
			m_lastFrameCurPoint.UpdatePoint(skeletJointPos.Position,skeletJointPos.Confidence);
		}
		if(m_startingPoseReferenceType == StartingPosReferenceType.StaticModifierToOtherJoint ||
			m_startingPoseReferenceType == StartingPosReferenceType.ScaledModifierToOtherJoint)
		{
			if(player.GetSkeletonJointPosition(m_referenceJoint,out skeletJointPos))
			{
				m_lastFrameReferenceJoint.UpdatePoint(skeletJointPos.Position,skeletJointPos.Confidence);
			}
		}
		if(m_startingPoseReferenceType == StartingPosReferenceType.ScaledModifierToOtherJoint)
		{
			SkeletonJointPosition skeletJointPos2;
			if(player.GetSkeletonJointPosition(m_referenceJointScale1,out skeletJointPos) &&
				player.GetSkeletonJointPosition(m_referenceJointScale2,out skeletJointPos2))
			{
				m_lastFrameScaleJoint1.UpdatePoint(skeletJointPos.Position,skeletJointPos.Confidence);
				m_lastFrameScaleJoint2.UpdatePoint(skeletJointPos2.Position,skeletJointPos2.Confidence);
			}
		}
	}
	
}
