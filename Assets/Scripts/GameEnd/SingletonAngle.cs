//**********************************************************************************************************
//
//文件名(File Name)：                              SingletonAngle.cs
//
//功能描述(Description)：                          检测手臂的角度
//
//作者(Author)：                                   connie
//
//日期(Create Date)：                              2014.07.31
//
//修改记录(Revision History)：
//           R1:
//			      修改作者：                        汪成峰
//                修改日期：                        2014.08.03
//                修改理由：						   OpenNI修改为非镜像模式，左右手翻转
//
//**********************************************************************************************************

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SingletonAngle : MonoBehaviour {
	private float mLeftAngle,mRightAngle;
	private GameObject _leftShoulder,_rightShoulder;
	//singleton
	private static SingletonAngle mInstance;
	public static SingletonAngle GetInstance(){
		return mInstance;
	}

	public float LeftAngle {
		get { return mLeftAngle; }
	}
	public float RightAngle {
		get { return mRightAngle; }
	}

	// Use this for initialization
	void Start () {
		mInstance = this;
		_leftShoulder = GameObject.FindWithTag("Player1 Left Shoulder");
		if(_leftShoulder == null)
			Debug.Log("Can't find left Shoulder gameobject");
		_rightShoulder = GameObject.FindWithTag("Player1 Right Shoulder");
		if(_rightShoulder == null)
			Debug.Log("Can't find right Shoulder gameobject");
	
	}
	
	// Update is called once per frame
	void Update () {
		//左右手颠倒。   by 汪成峰
		if(_leftShoulder.transform .eulerAngles .z<180){
			mRightAngle=(_leftShoulder.transform .eulerAngles .z-90)*(-1.0f);
		}
		else{
			mRightAngle=(_leftShoulder.transform .eulerAngles .z-450)*(-1.0f);
		}
		if(_rightShoulder.transform.eulerAngles.z <180 ){
			mLeftAngle=_rightShoulder.transform.eulerAngles.z+90;
		}
		else{
			mLeftAngle=_rightShoulder.transform.eulerAngles.z-270;
		}
	}

}
