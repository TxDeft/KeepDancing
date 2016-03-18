//**********************************************************************************************************
//
//文件名(File Name)：                              DelaySelfDestroy.cs
//
//功能描述(Description)：                          一段时间消失。由于坑爹的新版本粒子系统没有自动销毁，所以就先这么干了
//
//作者(Author)：                                   汪成峰
//
//日期(Create Date)：                              2014.08.01
//
//修改记录(Revision History)：
//                                  			  暂无
//
//**********************************************************************************************************
using UnityEngine;
using System.Collections;

/// <summary>
/// 一段时间后删除这个GameObject
/// </summary>
public class DelaySelfDestroy : MonoBehaviour {
	public float DelayTime = 5.0f;
	// Use this for initialization
	void Start () {
		StartCoroutine(Destroy());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Destroy(){
		yield return new WaitForSeconds(DelayTime);
		Destroy(gameObject);
	}

}
