using UnityEngine;
using UnityEditor;
using System.Collections;
using OpenNI;

[CustomEditor(typeof(AS_UNISkeletonTracker))]
public class UNISkeletonTrackerInspector : Editor {
	
	private string m_lastDescription;
	private GUIStyle m_style = null;
	private GUIContent m_content = null;
	
	public void OnEnable()
	{
		AS_UNISkeletonTracker tracker = target as AS_UNISkeletonTracker;
		if(tracker == null)
		{
			m_lastDescription = "Error";
		}
		else
		{
			m_lastDescription  = tracker.GetTrackerType();
		}
		m_style = new GUIStyle();
		m_style.wordWrap = true;
		m_style.fontStyle = FontStyle.Normal;
		m_content = new GUIContent();
	}
	
	public override void OnInspectorGUI ()
	{
		EditorGUI.indentLevel = 0;
		EditorGUIUtility.LookLikeInspector();
		AS_UNISkeletonTracker tracker = target as AS_UNISkeletonTracker;
		
		GUILayout.Label("Tracker description:");
		GUILayout.BeginHorizontal();
		GUILayout.Space(20);
		GUILayout.Label(m_lastDescription,m_style);
		GUILayout.EndHorizontal();
		
		if(m_lastDescription.CompareTo(tracker.GetTrackerType())!=0)
		{
			EditorGUILayout.Space();
			Color tmpColor = m_style.normal.textColor;
			m_style.normal.textColor = Color.red;
			m_content.text = "Tracker description changed! Please relink all references";
			m_content.tooltip = "When objects reference the tracker, they use the description to choose between the various options. " +
				"Whe the description changes, the linking is lost. You need to relink all reference unless you return the description to the previous defintion by changing the player and joint.";
			GUILayout.BeginHorizontal();
			GUILayout.Space(20);
			GUILayout.Label(m_content,m_style);
			GUILayout.EndHorizontal();
			m_style.normal.textColor = tmpColor;
		}
		
		EditorGUILayout.Space();
		tracker.m_settingManager = EditorGUILayout.ObjectField("Context",tracker.m_settingManager,typeof(OpenNISettingsManager),true) as OpenNISettingsManager;
		tracker.m_playerManager = EditorGUILayout.ObjectField("Player manager",tracker.m_playerManager,typeof(UNIPlayerManager),true) as UNIPlayerManager;
		EditorGUILayout.LabelField("Who to track","");
		EditorGUI.indentLevel += 2;
		tracker.m_playerNum = EditorGUILayout.IntField("Player Num",tracker.m_playerNum);
		if(tracker.m_playerNum < 0)
			tracker.m_playerNum = 0;
		tracker.m_jointToUse = (SkeletonJoint)EditorGUILayout.EnumPopup("Joint to track",tracker.m_jointToUse);
		EditorGUI.indentLevel -= 2;

		tracker.m_startingPoseReferenceType = (UNISkeletonTracker.StartingPosReferenceType)EditorGUILayout.EnumPopup("StartPos type",tracker.m_startingPoseReferenceType);
		EditorGUI.indentLevel +=2;
		switch(tracker.m_startingPoseReferenceType)
		{
		case UNISkeletonTracker.StartingPosReferenceType.SetPointReference:
			tracker.m_startPosModifier = EditorGUILayout.Vector3Field("StartPos",tracker.m_startPosModifier);
			break;
		case UNISkeletonTracker.StartingPosReferenceType.TrackedJointReference:
			EditorGUILayout.LabelField("StartPos=ref of",""+ tracker.m_jointToUse);
			break;
		case UNISkeletonTracker.StartingPosReferenceType.StaticModifierToOtherJoint:
			tracker.m_referenceJoint = (SkeletonJoint)EditorGUILayout.EnumPopup("Cur Position of",tracker.m_referenceJoint);
			tracker.m_startPosModifier = EditorGUILayout.Vector3Field("Modified by",tracker.m_startPosModifier);
			break;
		case UNISkeletonTracker.StartingPosReferenceType.ScaledModifierToOtherJoint:
			tracker.m_referenceJoint = (SkeletonJoint)EditorGUILayout.EnumPopup("Cur Position of",tracker.m_referenceJoint);
			tracker.m_startPosModifier = EditorGUILayout.Vector3Field("Modified by",tracker.m_startPosModifier);
			EditorGUILayout.LabelField("scaled by destance","between");
			EditorGUI.indentLevel +=2;
			tracker.m_referenceJointScale1 = (SkeletonJoint)EditorGUILayout.EnumPopup("joint1",tracker.m_referenceJointScale1);
			tracker.m_referenceJointScale2 = (SkeletonJoint)EditorGUILayout.EnumPopup("joint2",tracker.m_referenceJointScale2);
			EditorGUI.indentLevel -=2;
			break;
		}
		EditorGUI.indentLevel -=2;
		EditorGUILayout.Space();
		if(GUI.changed)
			EditorUtility.SetDirty(target);
	}

}
