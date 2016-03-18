//**********************************************************************************************************
//
//文件名(File Name)：                             MotionJointQuaternionVector.cs
//
//功能描述(Description)：                          关节动作四元素和根骨向量封装类
//
//作者(Author)：                                   connie
//
//日期(Create Date)：                              2014.07.21
//
//修改记录(Revision History)：
//                                  			  暂无
//
//**********************************************************************************************************
using System;
using UnityEngine;



public class MotionJointQuaternionVector
{
	//根骨位置坐标
	private Vector3 mTorsoPosition;
	//各关节四元素
	private Quaternion mTorsoQuaternion;
	private Quaternion mLeftHipQuaternion;
	private Quaternion mRightHipQuaternion;
	private Quaternion mLeftShoulderQuaternion;
	private Quaternion mRightShoulderQuaternion;

	public MotionJointQuaternionVector (Vector3 torsoPosition,Quaternion torsoQuaternion,Quaternion leftHipQuaternion,Quaternion rightHipQuaternion,Quaternion leftShoulderQuaternion,Quaternion rightShoulderQuaternion)
	{
		mTorsoPosition=torsoPosition;
		mTorsoQuaternion = torsoQuaternion;
		mLeftHipQuaternion=leftHipQuaternion;
		mRightHipQuaternion=rightHipQuaternion;
		mLeftShoulderQuaternion=leftShoulderQuaternion;
		mRightShoulderQuaternion=rightShoulderQuaternion;

	}
	public Vector3 TorsoPosition{
		get {return mTorsoPosition;}
	}
	public Quaternion TorsoQuaternion{
		get {return mTorsoQuaternion;}
	}
	public Quaternion LeftHipQuaternion{
		get {return mLeftHipQuaternion;}
	}
	public Quaternion RightHipQuaternion{
		get {return mRightHipQuaternion;}
	}
	public Quaternion LeftShoulderQuaternion{
		get {return mLeftShoulderQuaternion;}
	}
	public Quaternion RightShoulderQuaternion{
		get {return mRightShoulderQuaternion;}
	}

}


