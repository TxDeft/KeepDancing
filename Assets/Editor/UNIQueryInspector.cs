using UnityEditor;
using UnityEngine;
using System.Collections;
using OpenNI;

[CustomEditor(typeof(AS_UNIQuery))]
public class UNIQueryInspector : Editor {

	static protected bool[] m_queryFoldout;
	protected static int[] m_Version = new int[4];
	
	override public void OnInspectorGUI()
	{
		EditorGUI.indentLevel = 0;
		EditorGUIUtility.LookLikeInspector();
		UNIQuery query = target as UNIQuery;
		
		if(query.Valid == false)
		{
			query.ForceInit();
		}
		
		if(query.m_queryDescriptions != null)
		{ 
			if(m_queryFoldout == null || m_queryFoldout.Length != query.m_queryDescriptions.Length)
			{
				m_queryFoldout = new bool[query.m_queryDescriptions.Length];
				for(int i=0; i < m_queryFoldout.Length;i++)
				{
					m_queryFoldout[i]=false;
				}
			}
				
			GUILayout.Label ("Queries supported of "+query.m_queryDescriptions.Length +" nodes ");
			for(int i= 0; i<query.m_queryDescriptions.Length;i++)
			{
				m_queryFoldout[i] = EditorGUILayout.Foldout(m_queryFoldout[i],""+query.m_queryDescriptions[i].m_nodeType+" nodes query");
				if(m_queryFoldout[i])
				{
					EditorGUI.indentLevel +=2;
					QueryDescription desc=query.m_queryDescriptions[i];
					if(EditorApplication.isPlaying == false)
					{
						desc.m_nodeName = EditorGUILayout.TextField("Node name",desc.m_nodeName);
						desc.m_vendorName = EditorGUILayout.TextField("Vendor name",desc.m_vendorName);
						EditorGUILayout.LabelField("Min version:",desc.RequiresMinVersion()?"Limited":"no limitation");
						desc.GetMinVersionArr(ref m_Version);
						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.Space ();
						for(int j= 0; j<m_Version.Length;j++)
						{
							if(j!=0)
							{
								GUILayout.Label(".",GUILayout.MaxWidth(5));
							}
							m_Version[j]=EditorGUILayout.IntField(m_Version[j],GUILayout.MaxWidth(25));
						}
						EditorGUILayout.EndHorizontal();
						desc.SetMinVersion(m_Version);
						
						EditorGUILayout.LabelField("Max version:",desc.RequiresMaxVersion()?"Limted":"no limitation");
						desc.GetMaxVersionArr(ref m_Version);
						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.Space();
						for(int j=0;j<m_Version.Length;j++)
						{
							if(j!=0)
							{
								GUILayout.Label(".",GUILayout.MaxWidth(5));
							}
							m_Version[j] = EditorGUILayout.IntField(m_Version[j],GUILayout.MaxWidth(25));
						}
						EditorGUILayout.EndHorizontal();
						desc.SetMaxVersion (m_Version);
					}
					ProductionNodeDescription curNodeDesc;
					if(OpenNISettingsManager.GetProductionNodeInfomation(desc.m_nodeType,out curNodeDesc))
					{
						EditorGUILayout.LabelField("Last node lodaed was:","");
						EditorGUI.indentLevel +=2;
						EditorGUILayout.LabelField("Node name",curNodeDesc.Name);
						EditorGUILayout.LabelField("Vendor name",curNodeDesc.Vendor);
						EditorGUILayout.LabelField("Version",""+curNodeDesc.Version.Major+"."+curNodeDesc.Version.Minor+"."+curNodeDesc.Version.Maintenance+"."+curNodeDesc.Version.Build);
						EditorGUI.indentLevel -=2;
					}
					else 
					{
						GUILayout.Label ("Load node to see its info");
					}
					
					EditorGUI.indentLevel -=2;
					query.m_queryDescriptions[i] = desc;
				}
			}
			
		}
		if(GUI.changed)
				EditorUtility.SetDirty(target);
	}
}
