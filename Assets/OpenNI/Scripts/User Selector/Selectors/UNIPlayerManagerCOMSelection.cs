using UnityEngine;
using System.Collections;

public class UNIPlayerManagerCOMSelection : UNIPlayerManager
{
	public float m_maxAllowedDistance = 100000.0f;//defines a maximum distance, we can't have depth farther than this.
	public float m_failurePenalty = 10000.0f;//Defines the penalty (in mm) for users who failed to track and have not moved.
	public float m_minCOMChangeForFailure = 200.0f;//The minimum change in COM needed to retry a failure
	public float m_hysteresis = 100;//Holds the hysteresis used.
	
	protected override UNIPlayerCandidateObject GetNewUserObject (int userID)
	{
		return new UNIPlayerCOMCandidateObject(m_settingManager,userID);
	}
	
	public void Update()
	{
		foreach(UNIPlayerCandidateObject user in m_users)
		{
			UpdateUserPriority(user);
		}
		
		for(int i=0;i<m_users.Count ;i++)
		{
			UNIPlayerCOMCandidateObject bestUser = m_users[i] as UNIPlayerCOMCandidateObject;
			int bestIndex =i;
			for(int j = i+1;j<m_users.Count;j++)
			{
				UNIPlayerCOMCandidateObject curUser = m_users[j] as UNIPlayerCOMCandidateObject;
				if(curUser.m_priority > bestUser.m_priority)
				{
					bestUser = curUser;
					bestIndex = j;
				}
			}
			if(bestIndex !=i)
			{
				m_users[bestIndex] = m_users[i];
				m_users[i]= bestUser;
			}
		}
		int numUsersToPlayers = Mathf.Min (m_players.Count,m_users.Count);//the actual number of users which are players
		
		// first we will unselect all irrelevant players
		for(int i=numUsersToPlayers;i<m_users.Count;i++)
		{
			int playerNum = GetPlayerIdFromUser(m_users[i]);
			UnselectPlayer(playerNum);
		}
		//now we just have selected players so we need to select the good ones and put them in empty places
		
		for(int i=0;i<numUsersToPlayers;i++)
		{
			int playerNum = GetPlayerIdFromUser(m_users[i]);
			if(playerNum>=0)
				continue;//it is already selected
			
			for(int j = 0;j<m_players.Count ;j++)
			{
				if(m_players[j].Valid == false)
				{
					m_players[j].User = m_users[i];
					m_players[j].User.SelectUser(m_numRetries);
					break;
				}
			}
		}
	}
	
	public override string GetSelectionString ()
	{
		return "User selector based on closet user";
	}
	
	
	protected void UpdateUserPriority(UNIPlayerCandidateObject user)
	{
		UNIPlayerCOMCandidateObject userCOM = user as UNIPlayerCOMCandidateObject ;
		if(userCOM == null)
			return; //irrelevant
		
		Vector3 com = m_settingManager.UserGenerator.GetUserCenterOfMass(userCOM.OpenNIUserID);
		userCOM.m_priority = com.z;
		if(userCOM.m_priority >0)
		{
			userCOM.m_priority =-m_maxAllowedDistance ;//as far away as possible for an illegal value
		}
		if(user.PlayerStatus == UserStatus.Selected || user.PlayerStatus == UserStatus.Tracking)
		{
			userCOM.m_priority += m_hysteresis;
		}
		if(userCOM.PlayerStatus == UserStatus.Failure)
		{
			com -= userCOM.m_COMPosWhenFail;
			if(com.magnitude < m_minCOMChangeForFailure)
			{
				userCOM.m_priority -= m_failurePenalty;
			}
		}
	}
}

