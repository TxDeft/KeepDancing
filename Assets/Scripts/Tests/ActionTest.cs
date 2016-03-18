//**********************************************************************************************************
//
//文件名(File Name)：                              ActionTest.cs
//
//功能描述(Description)：                          动作匹配    泰斯特
//
//作者(Author)：                                   白野
//
//日期(Create Date)：                              2014.07.25
//
//修改记录(Revision History)：
//                                  			  暂无
//
//**********************************************************************************************************
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionTest : MonoBehaviour, IObserver {
	void Start () {
		SingletonActionMatchManager.GetInstance().AddObserver (this);
		}
	void IObserver.ObserverUpdate(ISubject subject,System.Object arg){

		print ((arg as MatchResult).Score);			//test
	}
}