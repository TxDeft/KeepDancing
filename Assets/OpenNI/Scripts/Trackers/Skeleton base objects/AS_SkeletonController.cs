using UnityEngine;
//using System.Collections;
using System;
using OpenNI;

public class AS_SkeletonController : UNISkeletonController {

	public int m_playerNumber;
	public UNIPlayerManager m_playerManager;
	
	
	public Transform[] m_jointTransforms;
	
	public bool m_updateJointPositions = false;
	public bool m_updateRootPosition = false;
	public bool m_updateOrientation = false;
	
	public float m_rotationDampening = 15.0f;
	public float m_scale = 0.001f;
	public float m_speed = 1.0f;

	
	// Use this for initialization
	void Start () {
		
        jointTransforms = m_jointTransforms;

		updateJointPositions = m_updateJointPositions;
		updateRootPosition = m_updateRootPosition;
		updateOrientation = m_updateOrientation;
		
		rotationDampening = m_rotationDampening;
		scale = m_scale;
		speed = m_speed;
		
		if(m_playerManager == null)
			m_playerManager = FindObjectOfType(typeof(UNIPlayerManager)) as UNIPlayerManager;
		if(m_playerManager == null)
			throw new System.Exception("Must have a plyer manager to control the skeleton!");
		int jointCount = Enum.GetNames(typeof(SkeletonJoint)).Length+1;//enum starts at 1
		m_jointsInitialRotations = new Quaternion[jointCount];
		//save all initial rotation
		//NOTE: Assumes skeleton mode is in 'T' pose since all rotations are relative to that pose
		foreach(SkeletonJoint joints in Enum.GetValues(typeof(SkeletonJoint)))
		{
			if((int)joints >= m_jointTransforms.Length)
				continue;
			
			if(m_jointTransforms[(int)joints])
			{
				m_jointsInitialRotations[(int)joints]=Quaternion.Inverse(transform.rotation)*m_jointTransforms[(int)joints].rotation;
			}
		}
		m_originalRootPosition = transform.localPosition;
		//start out in calibration pose
		RotateToCalibrationPose();
	}
	
	// Update is called once per frame
	void Update () {
		if(m_playerManager == null || m_playerManager.Valid == false)
			return;
		
		UNISelectedPlayer player = m_playerManager.GetPlayer(m_playerNumber);
		if(player == null || player.Valid == false || player.Tracking == false)
		{
			RotateToCalibrationPose(); //don't have anything to worke with
			return;
		}
		Vector3 skelPos= Vector3.zero;
		SkeletonJointTransformation skelTrans;
		if(player.GetReferenceSkeletonJointTransform(SkeletonJoint.Torso,out skelTrans))
		{
			if(skelTrans.Position.Confidence < 0.5f)
				player.RecalcReferenceJoints(); //we NEED the torso to be good.
			if(skelTrans.Position.Confidence >= 0.5f)
				skelPos = UNIConvertCoordinates.ConvertPos(skelTrans.Position.Position);
		}
		UpdateSkeleton(player,skelPos);
	}

}
