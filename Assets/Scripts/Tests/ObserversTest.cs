using System.Collections;
using System;
using UnityEngine;

public class ObserversTest : MonoBehaviour, IObserver{
	private EnumBeatType mCurrentBeatType;
	private ISubject mBeatManager;

	void Start(){
		mBeatManager = SingletonBeatManager.GetInstance();
		mBeatManager.AddObserver (this);
	}

	public void ObserverUpdate(ISubject subject,System.Object arg){
		mCurrentBeatType =(EnumBeatType) arg;
		mBeatManager = subject;
		print (mCurrentBeatType);
		//print (Time.frameCount);
	}
}
