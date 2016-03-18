//**********************************************************************************************************
//
//文件名(File Name)：                              RotateFootLight.cs
//
//功能描述(Description)：                          旋转脚底灯光
//
//作者(Author)：                                   汪成峰
//
//日期(Create Date)：                              2014.08.03
//
//修改记录(Revision History)：
//                                  			  暂无
//
//**********************************************************************************************************

using UnityEngine;
using System.Collections;

public class RotateFootLight : MonoBehaviour {
	//旋转速度的上界和下界
	public float RotateSpeedUpperBound = 65.0f;
	public float RotateSpeedLowerBound = 20.0f;
	//速度变化速率
	public float AcceleratedSpeed = 5.0f;
	//当前速度
	private float mCurrentSpeed;
	// Use this for initialization
	void Start () {
		mCurrentSpeed = RotateSpeedLowerBound;
	}
	
	// Update is called once per frame
	void Update () {
		mCurrentSpeed += AcceleratedSpeed * Time.deltaTime;
		transform.Rotate(Vector3.forward, mCurrentSpeed * Time.deltaTime);
		if((mCurrentSpeed > RotateSpeedUpperBound && AcceleratedSpeed > 0.0f) || (mCurrentSpeed < RotateSpeedLowerBound && AcceleratedSpeed < 0.0f)){
			AcceleratedSpeed *= -1.0f;
		}
	}
}
