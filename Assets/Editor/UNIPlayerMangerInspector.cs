using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(UNIPlayerManager))]
public class UNIPlayerMangerInspector : Editor
{
	public override void OnInspectorGUI ()
	{
		DrawPlayerManager();
		if(GUI.changed)
			EditorUtility.SetDirty(target);
		
	}
	
	protected virtual void DrawPlayerManager()
	{
		EditorGUIUtility.LookLikeInspector();
		UNIPlayerManager manager = target as UNIPlayerManager;
		GUILayout.Label(manager.GetSelectionString());
		EditorGUILayout.Space();
		manager.m_settingManager = EditorGUILayout.ObjectField("Context",manager.m_settingManager,typeof(OpenNISettingsManager),true) as OpenNISettingsManager;
		int maxPlayers = EditorGUILayout.IntField("Max allowed players",manager.m_maxNumberOfPlayers);
		if(maxPlayers >0 )
		{
			if(EditorApplication.isPlaying)
			{
				//is running so we need to change manually
				manager.ChangeNumberOfPlayers(maxPlayers);
			}
			else 
			{
				manager.m_maxNumberOfPlayers = maxPlayers;
			}
		}
		
		int numRetries = EditorGUILayout.IntField("Num Retries",manager.m_numRetries);
		if(numRetries>0)
			manager.m_numRetries = numRetries;
	}
}

