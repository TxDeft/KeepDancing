using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(UNIUserPoseGestureFactory))]
public class UNIUserPoseGestureInspector : Editor {
	
	public override void OnInspectorGUI ()
	{
		EditorGUI.indentLevel = 0;
		EditorGUIUtility.LookLikeInspector();
		UNIUserPoseGestureFactory detector = target as UNIUserPoseGestureFactory;
		string[] legalPoses = UNIUserAndSkeleton.GetLegalPoses();
		if(legalPoses != null)
		{
			int selectedIndex;
			if(detector.m_poseName == null)
			{selectedIndex = 0;}
			else for(selectedIndex = 0; selectedIndex < legalPoses.Length; selectedIndex++)
			{
				if(detector.m_poseName.CompareTo(legalPoses[selectedIndex])==0)
					break;
			}
			if(selectedIndex >= legalPoses.Length)
			{
				selectedIndex = 0;
			}
			selectedIndex = EditorGUILayout.Popup("Pose to detect",selectedIndex,legalPoses);
			detector.m_poseName = legalPoses[selectedIndex];
		}
		else
		{
			EditorGUILayout.LabelField("Pose to detect",detector.m_poseName);
		}
		
		detector.m_timeToHoldPose = EditorGUILayout.FloatField("Time to hold pose",detector.m_timeToHoldPose);
		if(detector.m_timeToHoldPose <0)
			detector.m_timeToHoldPose = 0;
		
		detector.m_Context = EditorGUILayout.ObjectField("Context",detector.m_Context,typeof(OpenNISettingsManager),true) as OpenNISettingsManager;
		
		if(EditorApplication.isPlaying == false)
		{
			if(GUILayout.Button("Update legal Poses"))
			{
				OpenNISettingsManager.InspectorReloadAnInstance();
			}
		}
		
		if(GUI.changed)
			EditorUtility.SetDirty(target);
	}
}
