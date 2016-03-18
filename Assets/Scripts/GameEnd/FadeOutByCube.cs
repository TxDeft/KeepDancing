//**********************************************************************************************************
//
//文件名(File Name)：                              FadeOutByCube.cs
//
//功能描述(Description)：                          让光淡出
//
//作者(Author)：                                   connie
//
//日期(Create Date)：                              2014.07.31
//
//修改记录(Revision History)：                     暂无
//
//**********************************************************************************************************

using UnityEngine;
using System.Collections;

public class FadeOutByCube : MonoBehaviour {
	public GameObject cube;
	private float time=0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if(time > 0.9f){
			cube.collider.enabled=true ;
		}
	
	}
}
