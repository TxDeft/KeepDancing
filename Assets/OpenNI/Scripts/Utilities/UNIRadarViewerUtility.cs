using UnityEngine;
using System.Collections.Generic;

public class UNIRadarViewerUtility : MonoBehaviour
{
	public OpenNISettingsManager m_settingManager;
	public Rect m_placeToDraw;
	
	//public UNIMapViewerBaseUtility.ScreenSnap m_snap;
	public UNIMapViewerUtility.ScreenSnap m_snap;
	
	public Vector2 m_radarRealWorldDimensions = new Vector2(4000,4000);
	public Color m_uncalibratedColor = Color.red;
	public Color m_calibratingColor = Color.magenta;
	public Color m_calibratedColor = Color.yellow;
	public Color m_trackingColor = Color.green;
	
	private GUIStyle m_guiStyle;//Internal GUI style used to draw the box(sets the texture)
	private Texture2D m_texture;//Internal texture to draw a specific color
	
	//mono-behvaior initialization
	void Start()
	{
		m_guiStyle = new GUIStyle();
		m_texture = new Texture2D(1,1);
		if(m_settingManager == null)
			m_settingManager = FindObjectOfType(typeof(OpenNISettingsManager)) as OpenNISettingsManager;
	}
	
	void OnGUI()
	{
		Rect posToPut = m_placeToDraw;
		switch(m_snap)
		{
			case UNIMapViewerUtility.ScreenSnap.UppreRightCorner:
			{
				posToPut.x = Screen.width - m_placeToDraw.x - m_placeToDraw.width;
				break;
			}
			case UNIMapViewerUtility.ScreenSnap.LowerLeftCorner:
			{
				posToPut.y = Screen.height - m_placeToDraw.y - m_placeToDraw.height;
				break;
			}
			case UNIMapViewerUtility.ScreenSnap.LowerRightCorner:
			{
				posToPut.x = Screen.width - m_placeToDraw.x - m_placeToDraw.width;
				posToPut.y = Screen.height - m_placeToDraw.y - m_placeToDraw.height;
				break;
			}
		}
		
		GUI.BeginGroup(posToPut);
		GUI.Box (new Rect(0,0,m_placeToDraw.width ,m_placeToDraw.height),"Users RadarViewer");
		
		if(m_settingManager.Valid == false || m_settingManager.UserSkeletonValid == false)
		{
			GUI.EndGroup();
			return;
		}
		
		int[] users = m_settingManager.UserGenerator.GetUserIds();
		foreach(int userID in users)
		{
			Vector3 com = m_settingManager.UserGenerator.GetUserCenterOfMass(userID);
			Vector2 randarPosition = new Vector2(com.x/m_radarRealWorldDimensions.x,-com.z/m_radarRealWorldDimensions.y);
			
			randarPosition.x += 0.5f;
			
			randarPosition.x = Mathf.Clamp(randarPosition.x,0.0f,1.0f);
			randarPosition.y = Mathf.Clamp(randarPosition.y,0.0f,1.0f);
			
			if(!m_settingManager.Mirror)
			{
				randarPosition.x = 1.0f - randarPosition.x;
			}

			Color tempColor = m_uncalibratedColor;
			if(m_settingManager.UserGenerator.IsTracking(userID))  
				tempColor = m_trackingColor;
			else if(m_settingManager.UserGenerator.IsCalibrated(userID))
				tempColor = m_calibratedColor;
			else if(m_settingManager.UserGenerator.IsCalibrating(userID))
				tempColor = m_calibratingColor;
			
			m_texture.SetPixel (0,0,tempColor);
			m_texture.Apply();
			m_guiStyle.normal.background = m_texture;
			GUI.Box(new Rect(randarPosition.x*m_placeToDraw.width -10,randarPosition.y*m_placeToDraw.height-10,20,20),userID.ToString(),m_guiStyle);
		}
		GUI.EndGroup();
	}
}

