using UnityEngine;
using System.Collections;

public class AS_UNIPointTrackerManager : UNIPointTrackerManager {
	
	public void Awake()
	{
		if(m_context == null)
		{
			m_context = FindObjectOfType(typeof(OpenNISettingsManager)) as OpenNISettingsManager;
		}
		if(m_trackers.Length <= 0)
			return ;//there are no trackers
		m_references = new int[m_trackers.Length];
		for(int i=0; i<m_trackers.Length; i++)
			m_references[i]=0;
	}
	
	public void OnApplicationQuit()
	{
		ReleaseAll();
	}
	
	public void OnDestory()
	{
		ReleaseAll();
	}
	
}
