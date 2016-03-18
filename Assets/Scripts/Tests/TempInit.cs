using UnityEngine;
using System.Collections;

public class TempInit : MonoBehaviour {
	void Awake () {
		//为了调试，先给游戏模式和歌曲名赋值
		//TODO: 后期去掉，必须从上一关卡获取
		DataBetweenScene.Single = true;
		DataBetweenScene.SongName = "zuixuanminzufeng";
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
