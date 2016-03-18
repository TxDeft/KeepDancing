using UnityEngine;
using System.Collections;
using System;


public class AS_UNIGestureManager : UNIGestureManager {
			
	// Use this for initialization
	void Start () {
	
		if(m_gestures == null || m_gestures.Length == 0)
		{
			//throw new Exception("1001: Must define a gesture tracking manager! - create a new game object and attach AS_UNIGestureManager script to it.  ");
			Debug.Log("Not define a gesture tracking manager! -If you need create a new game object and attach AS_UNIGestureManager script to it.  ");
		}	
		
		base.CkeckGesture();
	}
}
