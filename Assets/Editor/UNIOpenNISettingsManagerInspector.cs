using UnityEditor;
using UnityEngine;
using System.Collections;
using OpenNI;

[CustomEditor(typeof(AS_SettingsManager))]
public class UNIOpenNISettingsManagerInspector : Editor {

	protected bool m_userAndSkeletonControlFoldout = false;
	
	protected GUIContent m_myContent = new GUIContent("string lable","tooltip");
	
	public override void OnInspectorGUI ()
	{
		//base.OnInspectorGUI ();
		
		EditorGUI.indentLevel =0;
		EditorGUIUtility.LookLikeInspector();
		if(EditorApplication.isPlaying)
		{
			//the running status so we can only get some feedback
			RunInspector();
		}
		else
		{
			//the not running status so we can initialize stuff
			InitInspector();
		}
		EditorGUI.indentLevel = 0;
		
		//this is part of making sure we update during running. Add your functions to this delegate to get an update callback at approximately 100 times per second. 
		if(m_initialized == false)
		{
			EditorApplication.update += Update;
			m_initialized = true;
		}
	}
	
	private bool m_initialized  = false;
	
	protected void RunInspector()
	{
		OpenNISettingsManager OpenNISettings = target as OpenNISettingsManager;
		
		//basic test. If the object is invalid ,nothing else matters.
		if(OpenNISettings.Valid == false)
		{
			EditorGUILayout.LabelField("OpenNI was not initialized and therefore everything is invalid!");
			return;
		}
		
		//show the mirror capability
		OpenNISettings.Mirror = EditorGUILayout.Toggle("Mirror behavior",OpenNISettings.Mirror);
		
		//show image generator status
		string str = "Not used";
		if(OpenNISettings.m_useImageGenerator)
		{
			str = OpenNISettings.ImageValid ? "Valid":"Invalid";
		}
		EditorGUILayout.LabelField("Image Generator:",str);
		
		EditorGUILayout.Space();
		
		//show user and skeleton control status
		if(OpenNISettings.m_useSkeleton)
		{
			str = OpenNISettings.UserSkeletonValid ? "user and skeleton control is Valid":"user and skeleton control is Invalid";
			m_userAndSkeletonControlFoldout = EditorGUILayout.Foldout(m_userAndSkeletonControlFoldout,str);
			if(m_userAndSkeletonControlFoldout && OpenNISettings.UserSkeletonValid)
			{
				EditorGUI.indentLevel +=2;
				int[] users = OpenNISettings.UserGenerator.GetUserIds();
				EditorGUILayout.LabelField("Identified "+users.Length+" users","");
				for(int i= 0;i<users.Length ;i++)
				{
					int userID = users[i];
					Vector3 center = OpenNISettings.UserGenerator.GetUserCenterOfMass(users[i]);
					EditorGUILayout.LabelField("User:",""+(i+1));
					EditorGUI.indentLevel +=2;
					EditorGUILayout.LabelField("User id:",""+userID);
					string state="Not yet calibrated";
					if(OpenNISettings.UserGenerator.IsTracking(userID))
					{
						state = "Tracking";
					}
					else if(OpenNISettings.UserGenerator.IsCalibrated(userID))
					{
						state = "Calibrated";
					}
					else if(OpenNISettings.UserGenerator.IsCalibrating(userID))
					{
						state = "Calibrating";
					}
					EditorGUILayout.LabelField("state:",""+state);
					EditorGUILayout.LabelField("center of mass:",""+center);
					EditorGUI.indentLevel -=2;
				}
				EditorGUILayout.Space();
				//OpenNISettings.m_smoothingFactor = EditorGUILayout.FloatField("Smoothing factor:",OpenNISettings.m_smoothingFactor);
				OpenNISettings.SmoothFactor = EditorGUILayout.FloatField("Smoothing factor:",OpenNISettings.SmoothFactor);
				EditorGUI.indentLevel -=2;
			}
		}
		else
		{
			EditorGUILayout.LabelField("User and skeleton control:","Not used");
		}
		EditorGUILayout.Space();
		
		//if(GUI.changed)
			//EditorUtility.SetDirty(target);
	}
	
	protected void InitInspector()
	{
		
		
		OpenNISettingsManager openNISettings = target as OpenNISettingsManager;
		
		openNISettings.m_initMirroring = EditorGUILayout.Toggle("Mirror behavior",openNISettings.m_initMirroring);
		
		openNISettings.m_useImageGenerator = EditorGUILayout.Toggle("Use image generator?",openNISettings.m_useImageGenerator);
		openNISettings.m_useSkeleton = EditorGUILayout.Toggle("Use skeleton generator?",openNISettings.m_useSkeleton);
		
		if(openNISettings.m_useSkeleton && openNISettings.m_useSkeleton)
		{
			EditorGUI.indentLevel +=2;
			openNISettings.m_smoothingFactor = EditorGUILayout.FloatField("Smoothing factor:",openNISettings.m_smoothingFactor);
			EditorGUI.indentLevel -=2;
		}
		//initialize the logger and query objects
		openNISettings.m_logger = EditorGUILayout.ObjectField("Logger object",openNISettings.m_logger,typeof(UNIEventLogger),true) as UNIEventLogger;
		openNISettings.m_query = EditorGUILayout.ObjectField("Query object",openNISettings.m_query,typeof(UNIQuery),true) as UNIQuery;
		
		m_myContent.text = "XML file";
		m_myContent.tooltip = "Initialize from XML file.";
		openNISettings.m_XMLFileName = EditorGUILayout.TextField(m_myContent,openNISettings.m_XMLFileName);
		
		m_myContent.text = "Playback filename";
		m_myContent.tooltip = "If this is not empty,it will hold the filename of a recording. NOTE: if XML is defing,this is ignored.";
		//m_myContent.image = openNISettings.m_texture;
		openNISettings.m_recordingFileName = EditorGUILayout.TextField(m_myContent,openNISettings.m_recordingFileName);
		
		m_myContent.text = "Reset floor normal";
		m_myContent.tooltip = "If floor normal is updated in the game, it remains between game.If a new normal should be calculated,reset it between games.Note: this is only relevant while playing again and again in the editor as the normal will automationally be reset whenever the program is started again.";
		if(GUILayout.Button(m_myContent))
		{
			//resetFloorNormal
			UNIConvertCoordinates.ResetFloorNormal();
		}

		if(GUI.changed)
			EditorUtility.SetDirty(target);
		
	}
	
	/// Update this instance. used in order to make suer we update during running even if no movement occurs as long as the object is chose.
	void Update()
	{
		//to make sure this updates when we are not in focus too..
		if(EditorApplication.isPlaying)
			Repaint();
	}
}
