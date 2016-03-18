using UnityEngine;
//using System.Collections;
using System.Collections.Generic;
using OpenNI;

public class AS_UNIQuery : UNIQuery { 

	public override void Awake()
	{
		Debug.Log("###########this is AS_UNIQuery Awake begin");
		base.InternalAwake();
		Debug.Log("###########this is AS_UNIQuery Awake end");
	}
}
