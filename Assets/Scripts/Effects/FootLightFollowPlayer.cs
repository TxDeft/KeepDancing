//**********************************************************************************************************
//
//文件名(File Name)：                              FootLightFollowPlayer.cs
//
//功能描述(Description)：                          脚底灯光跟随玩家
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

public class FootLightFollowPlayer : MonoBehaviour {
	public float Damping = 0.3f;
	//需要挂脚底灯光的主角
	private Transform mPlayerTransform;
	//当前位置
	private Vector3 mCurrentPosition;
	//目标位置
	private Vector3 mTargetPosition;
	// Use this for initialization
	void Start () {
		//第0号角色是主角
		mPlayerTransform = SingletonLoadGamePlayResources.GetInstance().MainPlayerTransform;
		//设置当前位置，使用主角的xz，自己的y
		mCurrentPosition = new Vector3(mPlayerTransform.position.x, transform.position.y, mPlayerTransform.position.z);
		mTargetPosition = new Vector3(mCurrentPosition.x, mCurrentPosition.y, mCurrentPosition.z);
	}
	
	// Update is called once per frame
	void Update () {
		mTargetPosition.x = mPlayerTransform.position.x;
		mTargetPosition.y = transform.position.y;
		mTargetPosition.z = mPlayerTransform.position.z;
		//print(mTargetPosition);
		transform.position = Vector3.Lerp(transform.position, mTargetPosition, Damping);
	}
}
