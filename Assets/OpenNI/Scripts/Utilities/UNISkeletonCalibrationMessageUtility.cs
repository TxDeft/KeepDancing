using UnityEngine;
using System.Collections;

public class UNISkeletonCalibrationMessageUtility : MonoBehaviour {
	
	public UNIPlayerManager m_playerManager;
	public string m_actionToSelect;
	public bool m_AllPlayersMessage;
	public Texture2D m_Image;
	
	protected Rect m_basePos;
	protected int m_actionToSelectRectWidth;
	
	// Use this for initialization
	void Start () {
	
		if(m_playerManager == null)
		{
			m_playerManager = FindObjectOfType(typeof(UNIPlayerManager)) as UNIPlayerManager;
		}
		
		m_basePos = new Rect(0,Screen.height/2,150,30);
		if(m_actionToSelect == null)
		{
			m_actionToSelect = "";
		}
		m_actionToSelectRectWidth = m_actionToSelect.Length * 7;
	}
	
	// Update is called once per frame
	void OnGUI () {
	
		Rect curPos = m_basePos;
		
		if(m_playerManager == null || m_playerManager.Valid == false)
			return; //no player manager
		int numTracking  = m_playerManager.GetNumberOfTrackingPlayers();
		
		if(m_AllPlayersMessage && numTracking>=m_playerManager.m_maxNumberOfPlayers)
			return; //all player are tracking,noting to do here
		if(!m_AllPlayersMessage && numTracking >0)
			return;//at least one player is tracking and we don't want to show the message to the rest 
		int numUnselected = 0;
		for(int i=0;i<m_playerManager.m_maxNumberOfPlayers;i++)
		{
			UNISelectedPlayer player = m_playerManager.GetPlayer(i);
			if(player == null || player.Valid == false)
			{
				GUI.Box (curPos,"Player "+i+" is unSelected.");
				numUnselected++;
			}
			else if(player.Tracking == false)
			{
				GUI.Box (curPos,"Player "+i+" is calibrating.");
			}
			else
				continue;
			curPos.y += 35;
		}
		if(numUnselected == 0)
			return;
		if(m_actionToSelect.CompareTo("")!=0)
		{
			curPos.width = m_actionToSelectRectWidth;
			GUI.Box (curPos,m_actionToSelect);
			curPos.y += 35;
		}
		if(m_Image != null)
		{
			curPos.width = 128;
			curPos.height = 128;
			GUI.Box (curPos,m_Image);
		}
	}
}
