using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using OpenNI;


/// A class to define the inspector of UNIPlayerManager
/// 
/// This class is responsible for adding various inspector capabilities to the player manager
[CustomEditor(typeof(UNIPlayerManagerCOMSelection))]
public class UNIPlayerManagerCOMSelectionInspector : UNIPlayerMangerInspector 
{

    /// editor OnInspectorGUI to control the UNIEventLogger properties
    override public void OnInspectorGUI()
    {
        DrawPlayerManager();
        if (GUI.changed)
            EditorUtility.SetDirty(target);
    }

    protected override void DrawPlayerManager()
    {
        base.DrawPlayerManager();
        UNIPlayerManagerCOMSelection manager = target as UNIPlayerManagerCOMSelection;
        manager.m_maxAllowedDistance = EditorGUILayout.FloatField("Max distance", manager.m_maxAllowedDistance);
        if (manager.m_maxAllowedDistance < 0)
            manager.m_maxAllowedDistance = 0;
        manager.m_failurePenalty = EditorGUILayout.FloatField("Failure penalty", manager.m_failurePenalty);
        if (manager.m_failurePenalty < 0)
            manager.m_failurePenalty = 0;															 
        manager.m_minCOMChangeForFailure = EditorGUILayout.FloatField("Min change to retry", manager.m_minCOMChangeForFailure);
        if (manager.m_minCOMChangeForFailure < 0)
            manager.m_minCOMChangeForFailure = 0;
        manager.m_hysteresis = EditorGUILayout.FloatField("Hysteresis", manager.m_hysteresis);
        if (manager.m_hysteresis < 0)
            manager.m_hysteresis = 0;

    }
}
