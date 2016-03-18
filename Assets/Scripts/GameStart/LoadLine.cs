//**********************************************************************************************************
//
//文件名(File Name)：                              LoadLine.cs
//
//功能描述(Description)：                          载入
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

public class LoadLine : MonoBehaviour {

	//加载时的图片
	public Texture Progress;
	//载入中
	public Texture ZaiRuZhong;
	//异步对象
	private AsyncOperation async;
	private int mProgress;

	void Start(){

	}

	void Update(){
		if(SingletonStateController.CurrrentState == SingletonStateController.State_Type.ChooseFinish){
			//开始异步任务
			StartCoroutine (LoadScene());
			SingletonStateController.CurrrentState = SingletonStateController.State_Type.ChangeScene;
		}
		if(SingletonStateController.CurrrentState == SingletonStateController.State_Type.ChangeScene){
			mProgress = (int)(async.progress * 100);
			print (mProgress + "%");
		}

	}
	IEnumerator LoadScene(){
		print ("1111111111111111111111");
		//异步读取场景
		async = Application.LoadLevelAsync (1);

		//读取完毕后返回
		yield return async;
	}
	void OnGUI(){
		if (SingletonStateController.CurrrentState == SingletonStateController.State_Type.ChangeScene) {
			GUI.DrawTexture (new Rect (Screen.width / 2.2f, Screen.height / 1.1f,
			                           Screen.width / 10, Screen.height / 8), ZaiRuZhong);
			GUI.DrawTexture (new Rect (Screen.width / 26 - Progress.width * async.progress,
			                           Screen.height / 1.095f, Screen.width, Screen.height / 25), Progress);
		}
	}

}

