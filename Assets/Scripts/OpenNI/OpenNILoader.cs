//**********************************************************************************************************
//
//文件名(File Name)：                              OpenNILoader.cs
//
//功能描述(Description)：                          确保场景中有且仅有一个OpenNI组件。
//												  正确的逻辑应该是在第一个场景加载OpenNI组件，并且在之后的场景
//												  不销毁，但是这样一来调试不方便。使用该脚本可以确保不论是否从
//												  第一个场景中进入，都可以确保场景中有且仅有一个OpenNI组件）。
//
//作者(Author)：                                   汪成峰
//
//日期(Create Date)：                              2014.07.12
//
//修改记录(Revision History)：
//                                  			  暂无
//
//**********************************************************************************************************
using UnityEngine;
using System.Collections;

public class OpenNILoader : MonoBehaviour {
	//Prefab of OpenNI
	public GameObject openNIPrefab;

	// Use this for initialization
	void Awake () {
		//initialize OpenNI
		if (!(GameObject.FindWithTag("OpenNI")))
			Instantiate(openNIPrefab);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
