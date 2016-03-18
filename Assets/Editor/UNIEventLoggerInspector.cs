using UnityEditor;
using UnityEngine;
using System.Collections;


[CustomEditor(typeof(AS_Logger))]
public class UNIEventLoggerInspector : Editor {

	private bool Initialized = false;//Make sure we initialize the sources and categories list of the logger
	
	/// editor OnInspectorGUI to control the NIEventLogger properties
	override public void OnInspectorGUI () 
	{
		//base.OnInspectorGUI ();
		
		EditorGUI.indentLevel  = 0;
		EditorGUIUtility.LookLikeInspector();
		AS_Logger logger = target as AS_Logger;
		EditorGUILayout.LabelField("Categories to Show","");
		EditorGUI.indentLevel +=2;
		
		if(Initialized == false)
		{
			//this is aimed to make sure the categories and sources list will be updated when
			//the enums change. Therefore we use the static variable to make sure the test is done
			//only once and not every frame.
			if(logger.InitCategoriesList()||logger.InitSourcesList())
			{
				EditorUtility.SetDirty(target);
			}
			Initialized = true;
		}
		
		for(int i=0;i<logger.m_CategoriesToShow.Length;i++)
		{
			logger.m_CategoriesToShow[i] = EditorGUILayout.Toggle(""+(UNIEventLogger.Categories)i,logger.m_CategoriesToShow[i]);
		}
		
		EditorGUI.indentLevel -=2;
		EditorGUILayout.Space();
		
		EditorGUILayout.LabelField("Sources to show");
		EditorGUI.indentLevel +=2;
		for(int i = 0;i<logger.m_sourcesToShow.Length;i++)
		{
			logger.m_sourcesToShow[i] = EditorGUILayout.Toggle(""+(UNIEventLogger.Sources)i,logger.m_sourcesToShow[i]);
		}
		EditorGUI.indentLevel -=2;
		EditorGUILayout.Space();
		logger.m_minLevelToShow = (UNIEventLogger.VerboseLevel)EditorGUILayout.EnumPopup("Minimum log level",(System.Enum)logger.m_minLevelToShow);
		
		if(GUI.changed)
			EditorUtility.SetDirty(target);
		
	}
}
