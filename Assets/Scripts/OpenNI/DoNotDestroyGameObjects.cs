//**********************************************************************************************************
//
//文件名(File Name)：                              DoNotDestroyGameObject.cs
//
//功能描述(Description)：                          在切换场景的时候，不销毁这个GameObject。该脚本用于OpenNI组件，
//												  因为OpenNI组件在切换场景的时候不能被销毁。
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
/// <summary>
/// Don't destroy this gameobject
/// </summary>
public class DoNotDestroyGameObjects : MonoBehaviour {
	void Awake() {

		DontDestroyOnLoad(gameObject);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}                                               
}
