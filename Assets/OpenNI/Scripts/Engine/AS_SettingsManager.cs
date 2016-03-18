using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using OpenNI;

public class AS_SettingsManager : OpenNISettingsManager 
{
	public override void Awake()
	{
		base.InternalStart();
	}
}
