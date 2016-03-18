using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(AS_UNIInput))]
[System.Serializable]
public class UNIInputInspector : Editor {
	
	protected GUIContent m_myContent = new GUIContent("text","tooltip");
	/// the list of hand trackers we can choose
	protected string[] m_pointTrackerStrings = null;
	protected int arraySize = -1;
	/// the list of gestures we can choose
    protected string[] m_gestureStrings = null;
	
	protected void InitGestureStrings()
	{
		UNIInput inp = target as UNIInput;
		if(inp.m_gestureManager == null || inp.m_gestureManager.m_gestures == null)
		{
			if(m_gestureStrings == null || m_gestureStrings.Length != 1)
			{
				m_gestureStrings = new string[1];
				m_gestureStrings[0] = "No gesture";
				return;
			}
			else
			{
				//we already have the gesture strings
				return;
			}
		}
		if(m_gestureStrings != null && m_gestureStrings.Length == inp.m_gestureManager.m_gestures.Length)
			return;//it is already initialized properly
		m_gestureStrings = new string[inp.m_gestureManager.m_gestures.Length + 1];
		for(int i=0; i<inp.m_gestureManager.m_gestures.Length ; i++)
			m_gestureStrings[i] = inp.m_gestureManager.m_gestures[i].GetGestureType();
		m_gestureStrings[inp.m_gestureManager.m_gestures.Length] = "None";
	}
	
	protected void InitPointTrackerStrings()
	{
		UNIInput inp = target as UNIInput;
		if(inp.m_pointsTrackingManager== null || inp.m_pointsTrackingManager.m_trackers == null)
		{
			if(m_pointTrackerStrings == null || m_pointTrackerStrings.Length != 1)
			{
				m_pointTrackerStrings = new string[1];
				m_pointTrackerStrings[0] = "No point trackers";
				return;
			}
			else
			{
				//we already have the tracker strings
				return;
			}
		}
		
		if(m_pointTrackerStrings != null && m_pointTrackerStrings.Length == inp.m_pointsTrackingManager.m_trackers.Length)
			return;//it is already initialized properly.
		m_pointTrackerStrings = new string[inp.m_pointsTrackingManager.m_trackers.Length + 1];
		for(int i=0; i<inp.m_pointsTrackingManager.m_trackers.Length; i++)
			m_pointTrackerStrings[i] = inp.m_pointsTrackingManager.m_trackers[i].GetTrackerType();
		m_pointTrackerStrings[inp.m_pointsTrackingManager.m_trackers.Length]="None";
	}
	
	protected void fixTrackerIndex(UNIAxis axis)
	{
		if(axis.m_sourceTrackerIndex < m_pointTrackerStrings.Length && m_pointTrackerStrings[axis.m_sourceTrackerIndex].CompareTo(axis.m_sourceTrackerString)==0)
			return;
		for(int i = 0; i< m_pointTrackerStrings.Length; i++)
		{
			if(m_pointTrackerStrings[i].CompareTo(axis.m_sourceTrackerString) == 0)
			{
				axis.m_sourceTrackerIndex = i;
				return;//we found a match
			}
		}
		axis.m_sourceTrackerIndex = m_pointTrackerStrings.Length-1;
		axis.m_sourceTrackerString = m_pointTrackerStrings[axis.m_sourceTrackerIndex];
	}
	
	protected void fixGestureIndex(UNIAxis axis)
	{
		if(axis.m_gestureIndex < m_gestureStrings.Length && m_gestureStrings[axis.m_gestureIndex].CompareTo(axis.m_gestureString)==0)
			return;//we match
		if(axis.m_type != UNIAxis.UNIInputTypes.Gesture)
		{
			axis.m_gestureIndex = m_gestureStrings.Length -1;
			axis.m_gestureString = m_gestureStrings[axis.m_gestureIndex];
			return;
		}
		
		//we now know we are a gesture type so lets look for a match
		for(int i = 0; i<m_gestureStrings.Length; i++)
		{
			if(m_gestureStrings[i].CompareTo(axis.m_gestureString)==0)
			{
				axis.m_gestureIndex = i;
				return; //we found a match
			}
		}
		
		axis.m_gestureIndex = m_gestureStrings.Length - 1;
		axis.m_type = UNIAxis.UNIInputTypes.HandMovementFromStartingPoint;
		axis.m_gestureString = m_gestureStrings[axis.m_gestureIndex];
	}
	
	public override void OnInspectorGUI ()
	{
		//EditorGUIUtility.LookLikeInspector();
		//base.OnInspectorGUI ();
		
		if(EditorApplication.isPlaying)
			return;
		InitPointTrackerStrings();
		InitGestureStrings();
		EditorGUIUtility.LookLikeInspector();
		UNIInput inp = target as UNIInput;
		EditorGUI.indentLevel = 0;
		m_myContent.text = "Hand trackers manager";
		m_myContent.tooltip = "The hands tracker manager which holds hand tracking source we have";
		inp.m_pointsTrackingManager = EditorGUILayout.ObjectField(m_myContent, inp.m_pointsTrackingManager, typeof(UNIPointTrackerManager), true) as UNIPointTrackerManager;
		m_myContent.text = "Gestures manager";
		m_myContent.tooltip = "The gestures manager which holds which gestures we can use";
		inp.m_gestureManager = EditorGUILayout.ObjectField(m_myContent, inp.m_gestureManager,typeof(UNIGestureManager), true) as UNIGestureManager;
		
		m_myContent.text = "Axes";
		m_myContent.tooltip = "All the axes to use for Natural Interactions";
		inp.m_foldAllAxes = EditorGUILayout.Foldout (inp.m_foldAllAxes,m_myContent);
		if(inp.m_foldAllAxes == false)
			return;
		EditorGUI.indentLevel += 2;
		if(arraySize <0)
			arraySize = inp.m_axisList.Count;
		m_myContent.text= "Size";
		m_myContent.tooltip = "The number of axes used";
		int numElements = EditorGUILayout.IntField(m_myContent,arraySize);
		arraySize = numElements;
		
		Event e = Event.current;
		if(e.keyCode == KeyCode.Return)
		{
			if(numElements == 0)
			{
				numElements = inp.m_axisList.Count;//we don't allow to make it into 0 to avoid "deleting" to changer something
				arraySize = numElements;
			}
			if(numElements != inp.m_axisList.Count)
			{
				if(numElements < inp.m_axisList.Count)
				{
					while(numElements < inp.m_axisList.Count)
					{
						inp.m_axisList.RemoveAt(inp.m_axisList.Count - 1);
					}
				}
				else
				{
					for(int i=inp.m_axisList.Count; i < numElements; i++)
					{
						UNIAxis newAxis = new UNIAxis();
						newAxis.m_gestureIndex = m_gestureStrings.Length - 1;
						inp.m_axisList.Add(newAxis);
					}
				}
				List<bool> newFoldout = new List<bool>();
				int size = inp.m_foldAxisElement.Count;
				if(size > inp.m_axisList.Count)
					size = inp.m_axisList.Count;
				for(int i=0; i<size; i++)
				{
					newFoldout.Add (inp.m_foldAxisElement[i]);
				}
				for(int i= size; i< inp.m_axisList.Count; i++)
				{
					newFoldout.Add (false);
				}
				inp.m_foldAxisElement = newFoldout;
			}
		}
		
		for(int i = 0; i < inp.m_foldAxisElement.Count; i++)
		{
			inp.m_foldAxisElement[i] = EditorGUILayout.Foldout(inp.m_foldAxisElement[i],inp.m_axisList[i].m_axisName);
			if(inp.m_foldAxisElement[i]== false)
				continue;
			EditorGUI.indentLevel +=2;
			m_myContent.text = "Name";
			m_myContent.tooltip = "Change the name of the current axis";
			inp.m_axisList[i].m_axisName = EditorGUILayout.TextField(m_myContent, inp.m_axisList[i].m_axisName);
			m_myContent.text = "NIInput axis";
			m_myContent.tooltip = "If checked, the axis is used only by UNIInput, if not, it is also used by the input manager";
			inp.m_axisList[i].m_UNIInputAxisOnly = EditorGUILayout.Toggle(m_myContent,inp.m_axisList[i].m_UNIInputAxisOnly);
			m_myContent.text = "Descripitve Name";
			m_myContent.tooltip = "Name presented to the user if relevant";
			inp.m_axisList[i].m_descriptiveName = EditorGUILayout.TextField(m_myContent,inp.m_axisList[i].m_descriptiveName);
			fixGestureIndex(inp.m_axisList[i]);
			inp.m_axisList[i].m_gestureIndex = EditorGUILayout.Popup("Gesture", inp.m_axisList[i].m_gestureIndex, m_gestureStrings);
			inp.m_axisList[i].m_gestureString = m_gestureStrings[inp.m_axisList[i].m_gestureIndex];
			if(inp.m_axisList[i].m_gestureIndex != m_gestureStrings.Length -1)
				inp.m_axisList[i].m_type = UNIAxis.UNIInputTypes.Gesture;//to make sure we use either
			
			m_myContent.text = "Dead";
			m_myContent.tooltip = "Movement of less than this value(when chanage are used) will be ignored and considered 0";
			float deadZone = EditorGUILayout.FloatField(m_myContent,inp.m_axisList[i].m_deadZone);
			inp.m_axisList[i].m_deadZone = deadZone < UNIAxis.m_minDeadZone ? UNIAxis.m_minDeadZone : deadZone; ;
			
			m_myContent.text = "Sensitivity";
			m_myContent.tooltip = "A scaling factor";
			float sensititvity = EditorGUILayout.FloatField(m_myContent,inp.m_axisList[i].m_sensitivity);
			inp.m_axisList[i].m_sensitivity = sensititvity < UNIAxis.m_minSensitivity ? UNIAxis.m_minSensitivity : sensititvity;
			
			m_myContent.text = "Invert";
			m_myContent.tooltip = "Flip thed values between positive and negative";
			inp.m_axisList[i].m_invert = EditorGUILayout.Toggle(m_myContent,inp.m_axisList[i].m_invert);
			
			m_myContent.text = "Types";
			m_myContent.tooltip = "The type of movement to do";
			inp.m_axisList[i].m_type =(UNIAxis.UNIInputTypes)EditorGUILayout.EnumPopup(m_myContent,inp.m_axisList[i].m_type);
			if(inp.m_axisList[i].m_type != UNIAxis.UNIInputTypes.Gesture)
			{
				inp.m_axisList[i].m_gestureIndex = m_gestureStrings.Length - 1;//make it a "none" gesutre
			}
			
			m_myContent.text = "Normalization";
			m_myContent.tooltip = "Defines a transformation of the hand's axis to [-1,1] range (clipped)." +
				" -1 is anything smaller than the center minus m_maxMovement," +
				" +1 is anything larger than the center plus m_maxMovent" +
				" and center is defined based on the type (0 for HandMovement, focus point" +
				" for HandMovementFromFocusPoint etc.). If m_maxMovement is 0 or negative" +
				" then there will be to no transformation.\n" +
				" Note the actual value is still multiplied by the sensititvity," +
				" changed to 0 in the dead zone and can base inverted.";
			inp.m_axisList[i].m_maxMovement = EditorGUILayout.FloatField(m_myContent, inp.m_axisList[i].m_maxMovement);
			
			m_myContent.text = "Axis";
			m_myContent.tooltip = "Axis to use";
			inp.m_axisList[i].m_axisUsed = (UNIAxis.AxesList) EditorGUILayout.EnumPopup(m_myContent, inp.m_axisList[i].m_axisUsed);
			fixTrackerIndex(inp.m_axisList[i]);
			inp.m_axisList[i].m_sourceTrackerIndex = EditorGUILayout.Popup("Tracker to use",inp.m_axisList[i].m_sourceTrackerIndex, m_pointTrackerStrings);
			inp.m_axisList[i].m_sourceTrackerString = m_pointTrackerStrings[inp.m_axisList[i].m_sourceTrackerIndex];
 			
			EditorGUILayout.Space();
			EditorGUI.indentLevel -=2;
			
		}
		EditorGUI.indentLevel -= 2;
		EditorGUI.indentLevel = 0;
		EditorGUILayout.Space ();
		if(GUI.changed)
			EditorUtility.SetDirty(target);
	}
}
