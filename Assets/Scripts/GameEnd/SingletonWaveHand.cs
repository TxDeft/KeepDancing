//**********************************************************************************************************
//
//文件名(File Name)：                              SingletonWaveHand.cs
//
//功能描述(Description)：                          挥手检测算法
//
//作者(Author)：                                   汪成峰\connie
//
//日期(Create Date)：                              2014.07.30
//
//修改记录(Revision History)：
//           R1:
//			      修改作者：                        汪成峰
//                修改日期：                        2014.08.03
//                修改理由：						   OpenNI修改为非镜像模式，左右进入有所变化
//
//**********************************************************************************************************

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SingletonWaveHand : MonoBehaviour, ISubject {
	private List<IObserver> mListObservers;
	//singleton
	private static SingletonWaveHand mInstance;
	public static SingletonWaveHand GetInstance(){
		return mInstance;
	}

	public float WaveThreshold = 1.2f;
	
	//Fixed timesetp is 0.02 and I want to get the data in 0.5 second.
	private const int _frameNum = 25;
	private float[] _arrayDeltaLeftX, _arrayDeltaLeftY, _arrayDeltaRightX, _arrayDeltaRightY, _arrayDeltaTorse;
	private float lastLeftHandX, lastLeftHandY, lastRightHandX, lastRightHandY, lastTorseX;
	
	private GameObject _torse, _leftHand, _rightHand;
	//Indicate which element in _arrayDeltaLeft etc. is updating.
	private int _updateIndex = 0;



	// Use this for initialization
	void Start () {
		mInstance = this;
		mListObservers = new List<IObserver>();
		_leftHand = GameObject.FindWithTag("Player1 Left Hand");
		if(_leftHand == null)
			Debug.Log("Can't find left hand gameobject");
		_rightHand = GameObject.FindWithTag("Player1 Right Hand");
		if(_rightHand == null)
			Debug.Log("Can't find right hand gameobject");
		_torse = GameObject.FindWithTag("Player1 Torso");
		if(_torse == null)
			Debug.Log("Can't find torso gameobject");
		
		//Fixed timesetp is 0.02 and I want to get the data in 0.5 second.
		_arrayDeltaLeftX = new float[_frameNum];
		_arrayDeltaLeftY = new float[_frameNum];
		_arrayDeltaRightX = new float[_frameNum];
		_arrayDeltaRightY = new float[_frameNum];
		_arrayDeltaTorse = new float[_frameNum];
		ClearAllDeltaData();
	}
	
	// Update is called once per frame
	void Update () {
		float leftDeltaSumX, leftDeltaSumY, rightDeltaSumX, rightDeltaSumY, torseLeftDeltaSum, torseRightDeltaSum;
		leftDeltaSumX = FreshAndCalDeltaData(_arrayDeltaLeftX, true, _leftHand.transform.position.x, lastLeftHandX);
		leftDeltaSumY = FreshAndCalDeltaData(_arrayDeltaLeftY, true, _leftHand.transform.position.y, lastLeftHandY);
		rightDeltaSumX = FreshAndCalDeltaData(_arrayDeltaRightX, true, _rightHand.transform.position.x, lastRightHandX);
		//print (rightDeltaSumX);
		rightDeltaSumY = FreshAndCalDeltaData(_arrayDeltaRightY, true, _rightHand.transform.position.y, lastRightHandY);
		torseLeftDeltaSum = FreshAndCalDeltaData(_arrayDeltaTorse, true  , _torse.transform.position.x, lastTorseX);
		torseRightDeltaSum = FreshAndCalDeltaData(_arrayDeltaTorse, true , _torse.transform.position.x, lastTorseX);
		//print (torseRightDeltaSum);
		//Wave left hand out.
		if(leftDeltaSumX - torseLeftDeltaSum > WaveThreshold && Mathf.Abs(leftDeltaSumX )- Mathf.Abs(leftDeltaSumY )> WaveThreshold){
			//Debug.Log("Wave left hand out.");
			Debug.Log("Wave right hand out.");
			ClearAllDeltaData();
			//NotifyObserver(EnumWaveHand.WaveLeftHandOut);
			NotifyObserver(EnumWaveHand.WaveRightHandOut);
			//SendMessage("OnWaveLeftHandOut", SendMessageOptions.DontRequireReceiver);
			//ChangeSelectedLevel(false);
		}
		//Wave right hand out.
		if(rightDeltaSumX - torseRightDeltaSum > WaveThreshold && Mathf.Abs(rightDeltaSumX) - Mathf.Abs(rightDeltaSumY) > WaveThreshold){
			//Debug.Log("Wave right hand out.");
			Debug.Log("Wave left hand in.");
			ClearAllDeltaData();
			NotifyObserver(EnumWaveHand.WaveLeftHandIn);
			//NotifyObserver(EnumWaveHand.WaveRightHandOut);
			//SendMessage("OnWaveRightHandOut", SendMessageOptions.DontRequireReceiver);
			//ChangeSelectedLevel(true);
		}

		//Wave left hand in.
		if(torseLeftDeltaSum -leftDeltaSumX> WaveThreshold && Mathf.Abs( leftDeltaSumX )- Mathf.Abs(leftDeltaSumY )> WaveThreshold){
			//Debug.Log("Wave left hand in.");
			Debug.Log("Wave right hand in.");
			ClearAllDeltaData();
			NotifyObserver(EnumWaveHand.WaveRightHandIn);
			//NotifyObserver(EnumWaveHand.WaveLeftHandIn);
			//SendMessage("OnWaveLeftHandIn", SendMessageOptions.DontRequireReceiver);
			//ChangeSelectedLevel(true);
		}

		//Wave right hand in.
		if( torseRightDeltaSum-rightDeltaSumX > WaveThreshold && Mathf.Abs(rightDeltaSumX) - Mathf.Abs(rightDeltaSumY) > WaveThreshold){
			//Debug.Log("Wave right hand in.");
			Debug.Log("Wave left hand out.");
			ClearAllDeltaData();
			NotifyObserver(EnumWaveHand.WaveLeftHandOut);
			//NotifyObserver(EnumWaveHand.WaveRightHandIn);
			//SendMessage("OnWaveRightHandIn", SendMessageOptions.DontRequireReceiver);
			//ChangeSelectedLevel(false);
		}

		//Save and update
		lastLeftHandX = _leftHand.transform.position.x;
		lastLeftHandY = _leftHand.transform.position.y;
		lastRightHandX = _rightHand.transform.position.x;
		lastRightHandY = _rightHand.transform.position.y;
		lastTorseX = _torse.transform.position.x;
		
		_updateIndex++;
	
	}

	//添加观察者
	public void AddObserver(IObserver observer) {
		mListObservers.Add (observer);
	}
	
	//删除观察者
	public void DeleteObserver(IObserver observer) {
		mListObservers.Remove (observer);
	}
	
	//遍历通知观察者
	public void NotifyObserver(System.Object arg) {
		foreach (IObserver o in mListObservers) {
			o.ObserverUpdate(this, arg);
		}
	}
	/// <summary>
	/// Calculate the sum of delta*** in 0.5s.
	/// </summary>
	/// <returns>Sum of delta*** in 0.5s</returns>
	/// <param name="arrayDeltaData">Delta data array</param>
	/// <param name="isBigger">Update data when current data is larger than last data(if it is false, update when smaller)</param>
	/// <param name="newData">Delta data of this frame</param>
	/// <param name="newData">Delta data of last frame</param>
	float FreshAndCalDeltaData(float[] arrayDeltaData, bool isBigger, float newData, float lastData){
		int dir = 1;
		float curDeltaData;
		if(!isBigger){
			dir = -1;
		}
		//update
		curDeltaData = (newData - lastData) * dir;
		//if(curDeltaData > 0){
			arrayDeltaData[_updateIndex % _frameNum] = curDeltaData;
		//}
		//else{
			//arrayDeltaData[_updateIndex % _frameNum] = 0;
		//}
		//accumulate
		float sum = 0.0f;
		for(int i = 0; i < _frameNum; i++){
			sum += arrayDeltaData[i];
		}

		return sum;
	}
	
	void ClearAllDeltaData(){
		for(int i = 0; i < _frameNum; i++){
			_arrayDeltaLeftX[i] = 0.0f;
			_arrayDeltaRightX[i] = 0.0f;
			_arrayDeltaRightY[i] = 0.0f;
			_arrayDeltaTorse[i] = 0.0f;
		}
		lastLeftHandX = lastRightHandX = lastRightHandY = lastTorseX = 0.0f;
	}
}

/// <summary>
/// 挥手信息
/// </summary>
public enum EnumWaveHand{
	WaveLeftHandOut,		//左手外挥
	WaveRightHandOut,		//右手外挥
	WaveLeftHandIn,		    //左手馁挥
	WaveRightHandIn		    //右手内挥
};