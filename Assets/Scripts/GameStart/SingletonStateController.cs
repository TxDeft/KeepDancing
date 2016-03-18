//**********************************************************************************************************
//
//文件名(File Name)：                              SingletonStateController.cs
//
//功能描述(Description)：                          状态控制
//
//作者(Author)：                                   白野
//
//日期(Create Date)：                              2014.08.5
//
//修改记录(Revision History)：
//                                  			  暂无
//
//**********************************************************************************************************

using UnityEngine;
using System.Collections;

public class SingletonStateController : MonoBehaviour {

	private static SingletonStateController mInstance;
	public static SingletonStateController GetInstance(){
		return mInstance;
	}

	public enum State_Type
	{
		ChooseMenu,
		ChooseFinish,
		ChangeScene,
	};

	public static State_Type CurrrentState;
	// Use this for initialization
	void Start () {
		CurrrentState = State_Type.ChooseMenu;
	}

}
