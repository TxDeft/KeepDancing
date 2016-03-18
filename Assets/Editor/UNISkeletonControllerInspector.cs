using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using OpenNI;

[CustomEditor(typeof(AS_SkeletonController))]
public class UNISkeletonControllerInspector : Editor {
	
	public override void OnInspectorGUI ()
	{
		EditorGUI.indentLevel = 0;
		EditorGUIUtility.LookLikeInspector();
		AS_SkeletonController controller = target as AS_SkeletonController;
		
		EditorGUILayout.LabelField("Controlling player","");
		EditorGUI.indentLevel += 2;
		controller.m_playerManager = EditorGUILayout.ObjectField("Player manager",controller.m_playerManager,typeof(UNIPlayerManager),true) as UNIPlayerManager;
		controller.m_playerNumber = EditorGUILayout.IntField("Player Number",controller.m_playerNumber);
		EditorGUI.indentLevel -= 2;
		EditorGUILayout.Space();
		
		EditorGUILayout.LabelField("Joints to control","");
		EditorGUI.indentLevel += 2;
		if(controller.m_jointTransforms == null)
		{
			controller.m_jointTransforms = new Transform[Enum.GetNames(typeof(SkeletonJoint)).Length+1];
			for(int i=0;i<controller.m_jointTransforms.Length;i++)
			{
				controller.m_jointTransforms[i] =null;
			}
		}
		else if(controller.m_jointTransforms.Length != Enum.GetNames(typeof(SkeletonJoint)).Length+1)
		{
			Transform[] newVal = new Transform[Enum.GetNames(typeof(SkeletonJoint)).Length+1];
			for(int i=0; i<newVal.Length; i++)
			{
				if(i<controller.m_jointTransforms.Length)
				{
					newVal[i] = controller.m_jointTransforms[i];
				}
				else
					newVal[i]=null;
			}
			controller.m_jointTransforms = newVal;
		}
		
		foreach(SkeletonJoint joint in Enum.GetValues(typeof(SkeletonJoint)))
		{
			controller.m_jointTransforms[(int)joint] = EditorGUILayout.ObjectField("NI_"+joint ,controller.m_jointTransforms[(int)joint], typeof(Transform),true) as Transform;
		}
		EditorGUI.indentLevel -= 2;
		EditorGUILayout.Space();
		
		EditorGUILayout.LabelField("What to update","");
		EditorGUI.indentLevel += 2;
		controller.m_updateRootPosition = EditorGUILayout.Toggle ("Update root Position",controller.m_updateRootPosition);
		controller.m_updateJointPositions = EditorGUILayout.Toggle ("Update Joint Position",controller.m_updateJointPositions);
		controller.m_updateOrientation = EditorGUILayout.Toggle ("Update Joint Orientation",controller.m_updateOrientation);
		EditorGUI.indentLevel -= 2;
		EditorGUILayout.Space();
		
		EditorGUILayout.LabelField("Scalling and Limitations","");
		EditorGUI.indentLevel += 2;
		controller.m_rotationDampening = EditorGUILayout.FloatField("Rotation Dampening",controller.m_rotationDampening);
		controller.m_scale = EditorGUILayout.FloatField("Scale",controller.m_scale);
		controller.m_speed = EditorGUILayout.FloatField("Torso speed scale",controller.m_speed);
		EditorGUI.indentLevel -= 2;
		EditorGUILayout.Space();

		if(GUI.changed)
			EditorUtility.SetDirty(target);
	}
}
