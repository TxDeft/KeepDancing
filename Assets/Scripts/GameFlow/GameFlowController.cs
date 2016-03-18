//**********************************************************************************************************
//
//文件名(File Name)：                              GameFlowController.cs
//
//功能描述(Description)：                          游戏流程控制，开始同步音乐、动画等，结束跳转场景
//
//作者(Author)：                                   汪成峰
//
//日期(Create Date)：                              2014.07.13
//
//修改记录(Revision History)：
//                                  			  暂无
//
//**********************************************************************************************************
using UnityEngine;
using System.Collections;

/// <summary>
/// 游戏流程控制
/// </summary>
public class GameFlowController : MonoBehaviour {
	void Awake () {
		//为了调试，先给游戏模式和歌曲名赋值
		//TODO: 后期去掉，必须从上一关卡获取
		DataBetweenScene.Single = true;
		if(DataBetweenScene.SongName == null){
			DataBetweenScene.SongName = "zuixuanminzufeng";
		}
	}

	// Use this for initialization
	void Start () {
		//TODO: 根据配置文件，选择场景和角色的prefab，目前只有一个角色，一个场景（除了角色在那边Instantiate以外，其余的都在需要时相应模块Instantiate）


		//TODO: 开启节拍控制模块，播放音乐，开始动画
		SingletonBeatManager.GetInstance ().Begin ();
		SingletonLoadGamePlayResources.GetInstance().ArrayCharacters[0].animation.Play(DataBetweenScene.SongName);
		audio.clip = SingletonLoadGamePlayResources.GetInstance().CurrentSong;
		audio.Play();
	}

	// Update is called once per frame
	void Update () {
		//判断游戏结束，计算平均分，跳转场景
		if(SingletonBeatManager.GetInstance().IsGameOver){
			DataBetweenScene.AverageScore = DataBetweenScene.Score / SingletonActionMatchManager.GetInstance().ActionMatchCount;
			Application.LoadLevel("GameOver");
		}
	}
}
