using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(AS_UNIPlayerManagerPoseSelection))]
public class UNIPlayerManagerPoseSelectionInspector : UNIPlayerMangerInspector {
	protected override void DrawPlayerManager ()
	{
		base.DrawPlayerManager ();
		AS_UNIPlayerManagerPoseSelection manager = target as AS_UNIPlayerManagerPoseSelection;
		
		string[] legalPose = UNIUserAndSkeleton.GetLegalPoses();
		if(legalPose != null)
		{
			int selectedIndex;
			if(manager.m_PoseToSelect == null)
			{
				selectedIndex = 0;
			}
			else for(selectedIndex = 0; selectedIndex < legalPose.Length; selectedIndex++)
			{
				if(manager.m_PoseToSelect.CompareTo(legalPose[selectedIndex])==0)
					break;
			}
			if(selectedIndex >= legalPose.Length)
				selectedIndex = 0;
			selectedIndex = EditorGUILayout.Popup("Pose to select",selectedIndex,legalPose);
			manager.m_PoseToSelect = legalPose[selectedIndex];
			
			bool useUnselectionPose = manager.m_PoseToUnselect != null && manager.m_PoseToUnselect.CompareTo("")!=0;
			useUnselectionPose = EditorGUILayout.Toggle("Use unselection pose",useUnselectionPose);
			if(useUnselectionPose == false)
			{
				manager.m_PoseToUnselect = "";
			}
			else
			{
				if(manager.m_PoseToUnselect == null)
				{
					selectedIndex = 0;
				}
				else for(selectedIndex =0;selectedIndex<legalPose.Length;selectedIndex++)
				{
					if(legalPose[selectedIndex] == manager.m_PoseToUnselect)
						break;
				}
				if(selectedIndex >= legalPose.Length)
				{
					selectedIndex = 0;
				}
				selectedIndex = EditorGUILayout.Popup("Pose to unselect",selectedIndex,legalPose);
				manager.m_PoseToUnselect =legalPose[selectedIndex];
			}
		}
		else
		{
			EditorGUILayout.LabelField("Pose to select",manager.m_PoseToSelect);
			EditorGUILayout.LabelField("pose to unselect",manager.m_PoseToUnselect);
		}
		
		manager.m_timeToSwitch = EditorGUILayout.FloatField("Time between switching",manager.m_timeToSwitch);
		if(manager.m_timeToSwitch<0)
			manager.m_timeToSwitch =0;
		
		if(EditorApplication.isPlaying == false)
		{
			if(GUILayout.Button("Update legal pose"))
			{
				OpenNISettingsManager.InspectorReloadAnInstance();
			}
		}
	}
}
